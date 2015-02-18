using UnityEngine;
using System.Collections;

public class CCameraTransition : MonoBehaviour
{
    public GameObject _Player;
    //public GameObject enemy;

    Vector3 cameraGamePosition;
    Vector3 cameraStartPosition;
    Quaternion cameraStartRotation;

    // Intro and game start booleans
    public bool _StartIntro = false, 
                _GameStarted = false, 
                _Initialize = false;

    // Game start delay time variables
    int gameStartDelay = 0;
    public int _GameStartDelayTime = 120;

    // Use this for initialization
    void Start()
    {
        //initial camera rotation
        cameraStartRotation = Quaternion.Euler(new Vector3(15f, 270f, 0f));
        transform.rotation = cameraStartRotation;

        //initial position
        cameraStartPosition = new Vector3(12f, 8f, 0f);
        transform.position = cameraStartPosition;
        cameraGamePosition = new Vector3(_Player.transform.position.x, 6f, _Player.transform.position.z - 12f);
    }

    // Update is called once per frame
    void Update()
    {
        //if true camera will rotation into position 
        //THIS IS THE HOOK FOR STARTING THE GAME
        if (_StartIntro)
        {
            transform.position = Vector3.Lerp(transform.position, cameraGamePosition, 0.03f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(15f, 362f, transform.rotation.z), 0.03f);
            _GameStarted = true;
            gameStartDelay++;
            if (_GameStarted && gameStartDelay == _GameStartDelayTime)
            {
                _StartIntro = false;
                _Initialize = true;
            }
        }
        if(_Initialize)
        {
            transform.position = new Vector3(0f, 6f, _Player.transform.position.z - 12f);
        }
    }
}
