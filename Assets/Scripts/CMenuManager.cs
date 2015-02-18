using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CMenuManager : MonoBehaviour
{
    public CMenu CurrentMenu;

    public void Start()
    {
        ShowMenu(CurrentMenu);
    }

    // sets the desired menu screen
    public void ShowMenu(CMenu menu)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.IsOpen = false;
        }

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
    }
}