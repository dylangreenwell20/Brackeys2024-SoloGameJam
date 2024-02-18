using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    //choose from one of the three room types - empty, chest, combat - and set a variable for both doors
    //both doors can be the same room type

    public RoomTypeList leftDoor; //left door type
    public RoomTypeList rightDoor; //right door type

    public Enemy slime; //reference to slime
    public Enemy skeleton; //reference to skeleton

    private PlayerStats playerStats; //reference to player stats

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>(); //get player stats script
        GenerateRooms(); //generate rooms
    }

    public void GenerateRooms()
    {
        for(int i=0; i < 2; i++)
        {
            RoomTypeList randomRoom = (RoomTypeList)UnityEngine.Random.Range(0, RoomTypeList.GetValues(typeof(RoomTypeList)).Length); //pick a random room from the list
            Debug.Log(randomRoom); //for testing

            if(i == 0) //left door
            {
                leftDoor = randomRoom; //left door assigned random room value
            }
            else //right door
            {
                rightDoor = randomRoom; //right door assigned random room value
            }
        }
    }

    public Enemy CombatGenerator()
    {
        //pick how many enemies to spawn in the room and what the type of enemy is
        //higher the depth, more likely to see harder enemies

        //depth 0-3 - 20% skeleton, 80% slime
        //depth 4-8 - 50% skeleton, 50% slime

        //RETURN THIS METHOD AS A ENEMY TYPE SO I CAN JUST RETURN SCRIPTABLE OBJECT ENEMIES INSTEAD OF USING RESOURCES SEARCH

        int depth = playerStats.depth;

        int random = UnityEngine.Random.Range(1, 10);

        if (depth <= 3)
        {
            if(random <= 5) //if number hits 20% chance for skeleton
            {
                return skeleton; //return skeleton
            }

            return slime; //return slime
        }
        else if (depth <= 8)
        {
            if(random <= 5)
            {
                return skeleton; //return skeleton
            }

            return slime; //return slime
        }
        else
        {
            return null; //error
        }
    }
}

public enum RoomTypeList { Empty, Chest, Combat }; //list of rooms