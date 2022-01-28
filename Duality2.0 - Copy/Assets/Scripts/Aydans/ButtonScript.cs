using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public void QuitGame(string QuitGame)
    {
        Debug.Log("does it even quit?");
        Application.Quit();
    }

}
