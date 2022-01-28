using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class GameQuit : MonoBehaviour
    {
        public void QuitGame(string QuitGame)
        {
            Application.Quit();
        Debug.Log("Quit works");
        }
    }

