using UnityEngine;
using System.Collections;

public class CBillboardDestroy : MonoBehaviour 
{
    //void OnEnable()
    //{
    //    Invoke("Destroy", 2f);
    //}

    //void Destroy()
    //{
    //    gameObject.SetActive(false);
    //}

    //void OnDisable()
    //{
    //    CancelInvoke();
    //}
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {

        if(transform.position.z < player.transform.position.z - 30)
        {
            gameObject.SetActive(false);
        }
    }
}
