using UnityEngine;
using System.Collections;

public class CPlayer : MonoBehaviour//PLAYER HAS TO START AT Y POSITION 0
{
    internal enum moveStates { jumping, running, sliding, stopped }//Handles the current method of movement.
    internal moveStates currentState; //current movement state.

    Vector3 maxRight, maxLeft, forward, centerRunVector;
    GameObject mongoose, _camera, prefabPickup;
    public CMongoose mongooseScript;
    CCameraTransition cameraScript;

    float touchStartX,
        touchStartY,
        touchStopX,
        touchStopY,
        numberToSin,
        lerpSpeed,
        collideTimer,
        timeToWait,
        heightMultiplier;
    /*int powerUpHeatIncrease,
        powerUpHeatDecrease;*/
    bool canCollide, delayWait, magnetized;



    public int _MoveState;
    public float _Speed, _Temperature, _Score, _BankedScore;
    public bool ISSHIELDED;

    // Use this for initialization
    void Start()
    {
        mongoose = GameObject.FindGameObjectWithTag("Mongoose");
        mongooseScript = mongoose.GetComponent<CMongoose>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraScript = _camera.GetComponent<CCameraTransition>();
        prefabPickup = (GameObject)Resources.Load("Pickup");

        forward = new Vector3(transform.position.x, transform.position.y, _Speed * Time.deltaTime);
        _Temperature = 50;
        currentState = moveStates.running;
        lerpSpeed = 0.08f;
        _MoveState = 1;
        _Score = 0;
        ISSHIELDED = false;
        touchStartX = 0;
        touchStartY = 0;
        touchStopX = 0;
        touchStopY = 0;
        maxRight = new Vector3(2.5f, 0f, transform.position.z);
        maxLeft = new Vector3(-2.5f, 0f, transform.position.z);
        centerRunVector = new Vector3(0f, 0f, transform.position.z);
        canCollide = true;
        heightMultiplier = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //DelayWait(.5f, currentState);
        if (cameraScript._GameStarted)
        {
            input();
            Movement();
            // move state boundary
            if (_MoveState <= 0)
            {
                _MoveState = 0;
            }
            if (_MoveState >= 2)
            {
                _MoveState = 2;
            }
            if(mongooseScript.currentState == CMongoose.followStates.eating)
            {
                Application.LoadLevel("SceneTest");
            }
            
            if (canCollide==false)
            {
                collideTimer += Time.deltaTime;
                if (collideTimer >= 5f)
                {
                    canCollide = true;
                    collideTimer = 0;
                }
            }
            if(heightMultiplier >2f)
            {
                heightMultiplier -= 0.002f;
            }

            //TODO: Check if the current scene is not factory or shipyard.
            //_Score += Time.deltaTime;
            //_Temperature -= Time.deltaTime;
            

        }
    }

    void Movement()
    {
        maxRight.z = transform.position.z;
        maxLeft.z = transform.position.z;
        centerRunVector.z = transform.position.z;
        if (currentState != moveStates.stopped && cameraScript._Initialize)
            transform.position += forward;
        switch (_MoveState)
        {
            case 0:
                transform.position = Vector3.Lerp(transform.position, maxLeft, lerpSpeed);
                break;
            case 1:
                transform.position = Vector3.Lerp(transform.position, centerRunVector, lerpSpeed);
                break;
            case 2:
                transform.position = Vector3.Lerp(transform.position, maxRight, lerpSpeed);
                break;
        }
        if (currentState == moveStates.jumping)
        {
            numberToSin += 0.05f;
            transform.position = new Vector3(transform.position.x, Mathf.Sin(numberToSin) * heightMultiplier, transform.position.z);

            if (numberToSin >= 2.14f)
            {
                numberToSin = 0;
                currentState = moveStates.running;
            }
        }
        if (currentState == moveStates.sliding)
        {
            numberToSin += 0.05f;
            transform.position = new Vector3(transform.position.x, -Mathf.Sin(numberToSin), transform.position.z);

            if (numberToSin >= 3.14f)
            {
                numberToSin = 0;
                currentState = moveStates.running;
            }

        }
    }

    // player input for movement and jumping
    void input()
    {
        //KEYBOARD CONTROL CODE DEBUG ONLY, WILL BE REMOVED ON FINAL BUILD
        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > maxLeft.x && currentState != moveStates.jumping)
        {
            _MoveState -= 1;
        }

        if (Input.GetKeyDown(KeyCode.D) && transform.position.x < maxRight.x && currentState != moveStates.jumping)
        {
            _MoveState += 1;
        }
        if (Input.GetKeyDown(KeyCode.W) && currentState != moveStates.jumping)
        {
            currentState = moveStates.jumping;
        }
        if (Input.GetKeyDown(KeyCode.S) && currentState != moveStates.jumping && currentState != moveStates.sliding)
        {
            currentState = moveStates.sliding;
        }


        //Begin touch control code 


        if (Input.GetMouseButtonDown(0))
        {
            touchStartX = Input.mousePosition.x;
            touchStartY = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchStopX = Input.mousePosition.x;
            touchStopY = Input.mousePosition.y;

            int changeInTouchX;
            int changeInTouchY;

            changeInTouchX = Mathf.Abs((int)touchStartX - (int)touchStopX);
            changeInTouchY = Mathf.Abs((int)touchStartY - (int)touchStopY);

            if (changeInTouchX > changeInTouchY && changeInTouchX > 40)
            {
                if (touchStartX > touchStopX)
                {
                    _MoveState -= 1;
                }
                if (touchStopX > touchStartX)
                {
                    _MoveState += 1;
                }
            }
            if (changeInTouchY > changeInTouchX && changeInTouchY > 40)
            {
                if (touchStartY < touchStopY)
                {
                    currentState = moveStates.jumping;
                }
                if (touchStopY < touchStartY)
                {
                    currentState = moveStates.sliding;
                }
            }

        }
    }

    // collision with hot and cold blocks
    void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.tag == "BabyIguana")
        {
            _Score = _Score + 1;
        }

        if (objectCollidedWith.tag == "Oasis")
        {
            _BankedScore = _Score;
            _Score = 0;
        }

        if(objectCollidedWith.tag=="Courier")
        {
            _BankedScore = _Score;
            _Score = 0;
        }

        if (objectCollidedWith.tag == "SuperJump")
        {
            heightMultiplier = 5f;
        }

        if(objectCollidedWith.tag == "Magnet")
        {
            magnetized = true;
        }

        if(objectCollidedWith.tag == "BreedingGround")
        {
            //TODO: Add code for breeding ground after clarification from designers
        }

        if(objectCollidedWith.tag == "ScorchedEarth")
        {
            //TODO: Add code for scorched earth after approval from greg that it's actually possible
        }


        if (canCollide)
        {
            //
            if (objectCollidedWith.tag == "MiddleObstacle")//trigger fall animation
            {
                
                if (currentState != moveStates.sliding)
                {
                    canCollide = false;
                    delayWait = true;
                    
                    if (mongooseScript.currentState == CMongoose.followStates.near)
                    {
                        mongooseScript.previousState = mongooseScript.currentState;
                        mongooseScript.currentState = CMongoose.followStates.eating;
                    }

                    if (mongooseScript.currentState == CMongoose.followStates.far)
                    {
                        mongooseScript.previousState = mongooseScript.currentState;
                        mongooseScript.currentState = CMongoose.followStates.near;
                    }
                }
            }
            if (objectCollidedWith.tag == "FloorObstacle") //trigger trip animation
            {
                if (currentState != moveStates.jumping)
                {


                    canCollide = false;
                    
                    delayWait = true;

                    if (mongooseScript.currentState == CMongoose.followStates.near)
                    {
                        mongooseScript.previousState = mongooseScript.currentState;
                        mongooseScript.currentState = CMongoose.followStates.eating;
                    }

                    if (mongooseScript.currentState == CMongoose.followStates.far)
                    {
                        mongooseScript.previousState = mongooseScript.currentState;
                        mongooseScript.currentState = CMongoose.followStates.near;
                    }
                }
            }
            if (objectCollidedWith.tag == "Wall")
            {
                //trigger wall hit/kill animation

                _Speed = 0;

                if (mongooseScript.currentState == CMongoose.followStates.far || mongooseScript.currentState == CMongoose.followStates.near)
                {
                    mongooseScript.previousState = mongooseScript.currentState;
                    mongooseScript.currentState = CMongoose.followStates.eating;
                }
            }
        }
        
    }
    void OnTriggerStay(Collider objectCollidedWith)
    {
    }

    void OnTriggerExit(Collider objectCollidedWith)
    {
    }

    void DelayWait(float time, moveStates currentStateDuringCollision)
    {
        if (delayWait)
        {
            

            if (timeToWait > 0)
            {
                timeToWait = timeToWait - Time.deltaTime;
                currentState = moveStates.stopped;
            }
            if(timeToWait <=0)
            {
                currentState = moveStates.running;
                delayWait = false;
                
            }
        }
        else { timeToWait = time; }
    }

}