using UnityEngine;
using System.Collections;

public class CStartGame : MonoBehaviour
{
    public void StartGame()
    {
        Application.LoadLevel("gameScene");
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
