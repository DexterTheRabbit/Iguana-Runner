using UnityEngine;
using System.Collections;

public class CMongoose : MonoBehaviour
{
    GameObject player;
    CPlayer playerScript;

    Vector3 maxRight, maxLeft, farFollow, nearFollow, eatFollow, centerRunPoint;
    float lerpSpeed, lerpLateralSpeed, timeSinceLastHit;

    public enum followStates { far, near, eating }//Handles how far away from the player the mongoose follows.
    public followStates currentState;
    public followStates previousState;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//Finds the player in the current scene.
        playerScript = player.GetComponent<CPlayer>();//Gets the player script from the given player object.
        currentState = followStates.far;//The default follow state is far.

        maxRight = new Vector3(2.5f, 0f, transform.position.z);
        maxLeft = new Vector3(-2.5f, 0f, transform.position.z);
        centerRunPoint = new Vector3(0f, 0f, transform.position.z);
        lerpSpeed = 0.3f;
        lerpLateralSpeed = 0.001f;

        farFollow = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 8);
        nearFollow = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 3);
        eatFollow = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        farFollow.x = player.transform.position.x;
        farFollow.z = player.transform.position.z - 15;

        nearFollow.x = player.transform.position.x;
        nearFollow.z = player.transform.position.z - 3;

        eatFollow.x = player.transform.position.x;
        eatFollow.z = player.transform.position.z;

        #region when both states are the same
        if (currentState == followStates.far && currentState == previousState)
        {
            transform.position = farFollow;
        }
        else if (currentState == followStates.near && currentState == previousState)
        {
            transform.position = nearFollow;
        }
        else if (currentState == followStates.eating && currentState == previousState)
        {
            transform.position = eatFollow;
        }
        #endregion

        #region when the move states are different
        if (currentState == followStates.far && currentState != previousState)
        {
            transform.position = Vector3.Lerp(transform.position, farFollow, lerpSpeed);
            if (transform.position.z == player.transform.position.z - 5f)
            {
                previousState = currentState;
            }
        }
        else if (currentState == followStates.near && currentState != previousState)
        {
            transform.position = Vector3.Lerp(transform.position, nearFollow, lerpSpeed);
            if (transform.position.z == player.transform.position.z - 1f)
            {
                previousState = currentState;
            }
        }
        else if (currentState == followStates.eating && currentState != previousState)
        {
            transform.position = Vector3.Lerp(transform.position, eatFollow, lerpSpeed);
            if (transform.position.z == player.transform.position.z + 1f)
            {
                previousState = currentState;
            }
        }
        #endregion

        if (currentState == followStates.near)//If the mongoose is onscreen, count up to five seconds, then hide it.
        {
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit  >= 10f)
            {
                //Debug.Log("This got tripped");
                currentState = followStates.far;
                previousState = followStates.far;
                timeSinceLastHit  = 0f;
            }
        }

        maxLeft.z = transform.position.z;
        maxRight.z = transform.position.z;
        centerRunPoint.z = transform.position.z;

        switch (playerScript._MoveState)
        {
            case 0:
                transform.position = Vector3.Lerp(transform.position, maxLeft, lerpLateralSpeed);
                break;
            case 1:
                transform.position = Vector3.Lerp(transform.position, centerRunPoint, lerpLateralSpeed);
                break;
            case 2:
                transform.position = Vector3.Lerp(transform.position, maxRight, lerpLateralSpeed);
                break;
        }
    }
}