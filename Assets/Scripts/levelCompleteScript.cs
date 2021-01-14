using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelCompleteScript : MonoBehaviour
{
    public void restatLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLvl()
    {
        if (SceneManager.sceneCountInBuildSettings < SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }
}