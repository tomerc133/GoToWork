using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class RagdollScript : MonoBehaviour
{
    [Tooltip("INSERT TAG IN HERE U BABY, RUSH B NOOB TEAM")] [SerializeField]
    private string floorTag;

    private bool sloMo = false;

    public bool scaleTime;
    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (sloMo)
        // {
        //     float currentTime = Time.time;
        //     if (Time.time - currentTime > 1)
        //     {
        //         Time.timeScale = 1;
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(floorTag))
        {
           GameManager.addScore();
        }
    }
}