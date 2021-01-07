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
   
}
