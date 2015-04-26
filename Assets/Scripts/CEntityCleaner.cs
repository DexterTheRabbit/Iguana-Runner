using UnityEngine;
using System.Collections;

public class CEntityCleaner : MonoBehaviour
{
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < player.transform.position.z - 10)
        {
            //GameObject.Destroy(gameObject);
			gameObject.SetActive(false);
        }
    }
}
