using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name; //name of enemy
    public Sprite sprite; //sprite of enemy
    public EnemyType enemyType; //type of enemy

    public int health; //health of enemy
    public int defence; //defence of enemy
    public int damage; //damage of enemy
}

public enum EnemyType { Slime, Skeleton, Spider }