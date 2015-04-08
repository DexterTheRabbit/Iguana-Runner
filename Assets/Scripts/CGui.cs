using UnityEngine;
using System.Collections;

public class CGui : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _mongoose;

    //private string versionNumber;

    //CPlayer _playerScript;
    CMongoose _mongooseScript;
    CCameraTransition _cameraFollowerScript;
    Rect titleRect, exitRect, startRect, versionRect, scoreRect, heatRect;

    // Use this for initialization
    void Start()
    {
        //versionNumber = "5.1.1";

        //_playerScript = _Player.GetComponent<CPlayer>();
        _cameraFollowerScript = GetComponent<CCameraTransition>();
        _mongooseScript = _mongoose.GetComponent<CMongoose>();
        heatRect = new Rect(Screen.width / 2 - 15f, 0f, 120f, 30f);
        scoreRect = new Rect(Screen.width / 2 + 50f, 0f, 120f, 30f);
        versionRect = new Rect(Screen.width * .02f, 0f, 100f, 30f);
        startRect = new Rect(Screen.width / 2 - 60, Screen.height / 2 - 30, 120, 60);
        exitRect = new Rect(Screen.width / 2 - 60, Screen.height / 2 + 30, 120, 60);
        titleRect = new Rect(Screen.width / 2 - 60, Screen.height / 2 - 120, 120, 60);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        


        if (_cameraFollowerScript._GameStarted)
        {
           // GUI.Label(heatRect, "Heat: " + (int)_playerScript._Temperature);
          //  GUI.Label(scoreRect, "Score: " + (int)_playerScript._Score);
           // GUI.Label(new Rect(20, 100, 120, 60), "Is player shielded?: " + _playerScript.ISSHIELDED);
        }
        //if (!_cameraFollowerScript._GameStarted)
        //{
        //    if (GUI.Button(startRect, "Start"))
        //    {
        //        _cameraFollowerScript._StartIntro = true;
        //    }
        //    if(GUI.Button(exitRect, "Exit"))
        //    {
        //        Application.Quit();
        //    }
        //    GUI.Label(titleRect, "Iguana Runner");
        //}

        //GUI.Label(versionRect, "Version " + versionNumber);


    }
}
