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

     void Start()
    {
        _currentLoc =new Vector3(transform.position.x,transform.position.y,transform.position.z);
        launched = false;
    }

    void Update()
    {
        if (launched)
        {
            goDown = false;
            transform.position = Vector3.Lerp(transform.position,  _currentLoc + new Vector3(0, 5, 0), 5f*Time.deltaTime);
        }
           
       
        if (Vector3.Distance(transform.position, (_currentLoc + new Vector3(0, 5, 0)))<1)
        {
             goDown = true;
            launched = false;
        }

        if (goDown)
        {
            transform.position = Vector3.Lerp(transform.position ,_currentLoc, 7f*Time.deltaTime);  
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Rigidbody bodyPart in other.GetComponentsInChildren<Rigidbody>())
                bodyPart.AddForce(transform.up * power, ForceMode.Impulse);
            launched = true;
        }
    }
}
