using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using System;


public class SpawnObstacles : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject tripPrefab;
    public GameObject slidePrefab;
    public GameObject pointsPrefab;
    public GameObject shieldPrefab;
    public GameObject player;
    public GameObject cameraObject;
    CCameraTransition cameraScript;

    System.Collections.Generic.List<CSpawnSpace> spawnSpaces;
    System.Random rand;
    Vector3 playerForwardOffset;
    
    // Use this for initialization
    void Start()
    {
        cameraScript = cameraObject.GetComponent<CCameraTransition>();
        playerForwardOffset = new Vector3(0f, 0f, 0f);   
        spawnSpaces = new System.Collections.Generic.List<CSpawnSpace>();
        rand = new System.Random();
        for (int i = 0; i <= 8; i++)
        {
            CSpawnSpace spawnSpace = new CSpawnSpace();
            spawnSpace.Start();
            spawnSpace.ValidSpawn = true;
            if (i == 0 || i == 3 || i == 6)
            {
                spawnSpace.LocationToSpawn = new Vector3(-2.5f, 0f, gameObject.transform.position.z);
            }
            if (i == 1 || i == 4 || i == 7)
            {
                spawnSpace.LocationToSpawn = new Vector3(0f, 0f, gameObject.transform.position.z);
            }
            if (i == 2 || i == 5 || i == 8)
            {
                spawnSpace.LocationToSpawn = new Vector3(2.5f, 0f, gameObject.transform.position.z);
            }
            spawnSpaces.Add(spawnSpace);
        }

    }
    public void InvalidateColumn(int columnToInvalidate)
    {
        if(columnToInvalidate == 1)
        {
            spawnSpaces[0].ValidSpawn = false;
            spawnSpaces[3].ValidSpawn = false;
            spawnSpaces[6].ValidSpawn = false;
        }
        if (columnToInvalidate == 2)
        {
            spawnSpaces[1].ValidSpawn = false;
            spawnSpaces[4].ValidSpawn = false;
            spawnSpaces[7].ValidSpawn = false;
        }
        if (columnToInvalidate == 3)
        {
            spawnSpaces[2].ValidSpawn = false;
            spawnSpaces[5].ValidSpawn = false;
            spawnSpaces[8].ValidSpawn = false;
        }
    }
    public GameObject choosePrefab()
    {
        int choose = rand.Next(1, 20);
        if (choose >0 && choose < 5)
        {
            return wallPrefab;
        }
        if (choose >5 && choose < 10)
        {
            return tripPrefab;
        }
        if (choose >10 && choose < 15)
        {
            return slidePrefab;
        }

        if(choose>15&&choose< 18)
        {
            return pointsPrefab;
        }
        if(choose>18&&choose<20)
        {
            return shieldPrefab;
        }


        return pointsPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if(!cameraScript._Initialize)
        {
            spawnSpaces[0].ValidSpawn = false;
            spawnSpaces[3].ValidSpawn = false;
            spawnSpaces[1].ValidSpawn = false;
            spawnSpaces[4].ValidSpawn = false;
            spawnSpaces[2].ValidSpawn = false;
            spawnSpaces[5].ValidSpawn = false;
            InvalidateColumn(1);
            InvalidateColumn(3);
        }
        if (cameraScript._Initialize)
        {
            foreach (CSpawnSpace spawnSpace in spawnSpaces)
            {
                spawnSpace.Update();
            }
            
            playerForwardOffset.z = gameObject.transform.position.z +100f;
            switch (rand.Next(1, 4))
            {
                case 1:
                    {
                        switch (rand.Next(1, 4))
                        {
                            case 1:
                                {
                                    if (spawnSpaces[0].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[0].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(1);
                                    }
                                    break;
                                }

                            case 2:
                                {
                                    if (spawnSpaces[3].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[3].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(1);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (spawnSpaces[6].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[6].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(1);
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (rand.Next(1, 4))
                        {
                            case 1:
                                {
                                    if (spawnSpaces[1].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[1].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(2);
                                    }
                                    break;
                                }

                            case 2:
                                {
                                    if (spawnSpaces[4].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[4].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(2);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (spawnSpaces[7].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[7].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(2);
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 3:
                    {
                        switch (rand.Next(1, 4))
                        {
                            case 1:
                                {
                                    if (spawnSpaces[2].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[2].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(3);
                                    }
                                    break;
                                }

                            case 2:
                                {
                                    if (spawnSpaces[5].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[5].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(3);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (spawnSpaces[8].ValidSpawn)
                                    {
                                        GameObject prefabToSpawn = choosePrefab();
                                        GameObject.Instantiate(prefabToSpawn);
                                        prefabToSpawn.transform.position = spawnSpaces[8].LocationToSpawn +playerForwardOffset;
                                        InvalidateColumn(3);
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
    }
}
