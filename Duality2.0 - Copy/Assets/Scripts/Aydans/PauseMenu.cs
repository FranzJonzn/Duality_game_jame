using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject MenuPaused; 
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                MenuPaused.SetActive(true);
            }

            else
            {
                Time.timeScale = 0;
                MenuPaused.SetActive(false);
            }

            Debug.Log("Esc is working");
        }
    }
}
