using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUImenuStuff : MonoBehaviour
{
    float lastTimeScale = 0;
    bool paused = false;

    public GameObject pauseMenue;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (paused)
            {
                UnPaseGame();
                pauseMenue.SetActive(false);
            }
            else
            {
                PauseGame();
                pauseMenue.SetActive(true);
            }
        }
    }



    public void PauseGame()
    {
        if (!paused)
        {
            lastTimeScale = Time.timeScale;
            paused = true;

        }

        Time.timeScale = 0f;
    }
    public void UnPaseGame()
    {
        if (paused)
        {
            Time.timeScale = lastTimeScale;
            paused = false;

        }
    }

    //============================================
    public void QuitGame()
    {
        Application.Quit();

    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
