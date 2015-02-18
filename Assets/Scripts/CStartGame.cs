using UnityEngine;
using System.Collections;

public class CStartGame : MonoBehaviour
{
    public void StartGame()
    {
        Application.LoadLevel("SceneTest");
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
