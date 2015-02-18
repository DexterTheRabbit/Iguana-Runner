using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using System;


public class CSpawnSpace
{

    bool validSpawn;
    Vector3 locationToSpawn;
    float timeBeforeValidSpawnSetting;
    float timeBeforeValidSpawn;

    System.Random rand;

    public bool ValidSpawn
    {
        get
        {
            return validSpawn;
        }
        set
        {
            validSpawn = value;
        }
    }

    public Vector3 LocationToSpawn
    {
        get
        { return locationToSpawn; }
        set
        {
            locationToSpawn = value;
        }
    }

    // Use this for initialization
    public void Start()
    {
        rand = new System.Random();
        locationToSpawn = new Vector3(0f, 0f, 0f);
        timeBeforeValidSpawn = timeBeforeValidSpawnSetting;
        timeBeforeValidSpawnSetting = rand.Next(0, 6) ;
    }

    // Update is called once per frame
    public void Update()
    {
        timeBeforeValidSpawnSetting = rand.Next(2, 5);
        if (!validSpawn)
        {
            DecrementTimer();
        }
       
    }
    public void DecrementTimer()
    {
        if (timeBeforeValidSpawn > 0)
        {
            timeBeforeValidSpawn = timeBeforeValidSpawn - Time.deltaTime;
        }
        else
        {
            validSpawn = true;
            timeBeforeValidSpawn = timeBeforeValidSpawnSetting;
        }
    }
}
