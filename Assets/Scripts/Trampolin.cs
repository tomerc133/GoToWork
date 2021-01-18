using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trampolin : MonoBehaviour
{
    [SerializeField] private float power = 300; 
    [SerializeField] private bool launched;
    private Vector3 _currentLoc;
    private bool goDown = false;
    [SerializeField] bool inversed = false;
    private Vector3 launchMovement = new Vector3(0, 5, 0);
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _currentLoc =new Vector3(transform.position.x,transform.position.y,transform.position.z);
        launched = false;
        if (inversed)
            launchMovement *= -1;
    }

    void Update()
    {
        if (launched)
        {
            goDown = false;
            transform.position = Vector3.Lerp(transform.position,  _currentLoc + launchMovement, 5f*Time.deltaTime);
        }
 
        if (Vector3.Distance(transform.position, (_currentLoc + launchMovement))<1)
        {
            goDown = true;
            launched = false;
        }

        if (goDown)
        {
            transform.position = Vector3.Lerp(transform.position ,_currentLoc, 7f*Time.deltaTime);  
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            foreach (Rigidbody bodyPart in other.gameObject.GetComponentsInChildren<Rigidbody>())
                bodyPart.AddForce(transform.up * power, ForceMode.Impulse);
            _audioManager.PlayTrampolin();
            launched = true;
        }
    }
    
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             foreach (Rigidbody bodyPart in other.GetComponentsInChildren<Rigidbody>())
                 bodyPart.AddForce(transform.up * power, ForceMode.Impulse);
             _audioManager.PlayTrampolin();
             launched = true;
         }
     }
    
}