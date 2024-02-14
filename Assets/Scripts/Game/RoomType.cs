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

    private void Start()
    {
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
}

public enum RoomTypeList { Empty, Chest, Combat }; //list of rooms