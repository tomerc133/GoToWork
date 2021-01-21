using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    [SerializeField] private GameObject planCamera;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject cannonTip;
    [SerializeField] private GameObject[] charactersRb;
    private GameObject bulletClone;
    private bool theGameHasStarted = false;
    private AudioManager _audioManager;

    private void Awake()
    {
        bulletClone = Instantiate(charactersRb[0], cannonTip.transform.position, cannonTip.transform.rotation);
        _audioManager = FindObjectOfType<AudioManager>();

    }


    public void startGame()
    {
        theGameHasStarted = true;
        _audioManager.PlayBoomSound();
        StartCoroutine(startGameEnumerator());
        _audioManager.ResetSounds();

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (theGameHasStarted)
        {
            foreach (Rigidbody bodyPart in bulletClone.GetComponentsInChildren<Rigidbody>())
                bodyPart.AddForce(cannon.transform.forward * 12, ForceMode.Impulse);
        }
    }

    IEnumerator startGameEnumerator()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}