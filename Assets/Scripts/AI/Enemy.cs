using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Soldier enemySoldier;
    public bool hasDetectedPlayer = false;
    public string type = "Shooter";
}

