using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public MainManager MainManager { get { return FindObjectOfType<MainManager>(); } }
    
    public void ClickRestart()
    {
        MainManager.Restart();
    }

    public void ClickMenu()
    {
        MainManager.MainMenu();
    }
}
