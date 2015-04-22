using UnityEngine;
using System.Collections;
using System;

public class CPlayer : MonoBehaviour//PLAYER HAS TO START AT Y POSITION 0
{
    internal enum moveStates { jumping, running, sliding, stopped }//Handles the current method of movement.
    internal moveStates currentState; //current movement state.

    Vector3 maxRight, maxLeft, forward, centerRunVector;
    GameObject mongoose, _camera;
    public CMongoose mongooseScript;
    CCameraTransition cameraScript;

    private Animator anim;							// a reference to the animator on the character
    private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
    private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2
    private GameObject iguanaModel;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int runState = Animator.StringToHash("Base Layer.Running");
    static int jumpState = Animator.StringToHash("Base Layer.Jumping");
    static int jukeRightState = Animator.StringToHash("Base Layer.JukeRight");
    static int jukeLeftState = Animator.StringToHash("Base Layer.JukeLeft");

    float touchStartX,
        touchStartY,
        touchStopX,
        touchStopY,
        numberToSin,
        lerpSpeed,
        collideTimer,
        timeToWait;
    /*int powerUpHeatIncrease,
        powerUpHeatDecrease;*/
    bool canCollide, delayWait;



    public int _MoveState;
    public float _Speed, _Temperature, _Score;
    public bool ISSHIELDED;

    // Use this for initialization
    void Start()
    {
        iguanaModel = GameObject.FindGameObjectWithTag("IguanaModel");
        mongoose = GameObject.FindGameObjectWithTag("Mongoose");
        mongooseScript = mongoose.GetComponent<CMongoose>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraScript = _camera.GetComponent<CCameraTransition>();
        anim = iguanaModel.GetComponent<Animator>();
        if (anim.layerCount == 2)
            anim.SetLayerWeight(1, 1);


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
    }

    // Update is called once per frame
    void Update()
    {
        DelayWait(.5f, currentState);
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
                Console.WriteLine("mongose eating");
                //Application.LoadLevel("SceneTest");
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
            //TODO: Check if the current scene is not factory or shipyard.
            _Score += Time.deltaTime;
            _Temperature -= Time.deltaTime;
            
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
            transform.position = new Vector3(transform.position.x, Mathf.Sin(numberToSin) * 2f, transform.position.z);

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
            anim.Play("JukeLeft");
        }
        if (Input.GetKeyDown(KeyCode.D) && transform.position.x < maxRight.x && currentState != moveStates.jumping)
        {
            _MoveState += 1;
            anim.Play("JukeRight");
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
                    anim.Play("JukeLeft");
                }
                if (touchStopX > touchStartX)
                {
                    _MoveState += 1;
                    anim.Play("JukeRight");
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
        if (objectCollidedWith.tag == "points")
        {
            objectCollidedWith.gameObject.transform.position = gameObject.transform.position - new Vector3(0f, 0f, -100f);
        }

        //if (objectCollidedWith.tag == "Cold")
        //{
        //    _temperature -= powerUpHeatDecrease;
        //}
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

            //if(objecCollidedWith.tag == "")

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