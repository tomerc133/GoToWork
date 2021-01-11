using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private bool launched = false; 
    private bool launchNow = false;
    private Vector3 _flyTo;
    private Vector3 _flyToGameObject;
    private GameObject playerFoot;
    private GameObject player;

// Start is called before the first frame update
    void Start()
    {
        launched = false;
        _flyToGameObject = transform.GetChild(0).position; 
        _flyTo = new Vector3(_flyToGameObject.x,_flyToGameObject.y,_flyToGameObject.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (launched)
        {
            playerFoot.transform.position = transform.position;
            player.transform.position = transform.GetChild(1).position;
            if(launchNow)
            {
                transform.position = Vector3.Lerp(transform.position,_flyTo , 3f * Time.deltaTime); 
            }
            else
            {
                StartCoroutine(launchSetUp());
            }
           
        }
            
        if (Vector3.Distance(transform.position, _flyTo) < 10)
        {
            launched = false;
            launchNow = false;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
        if (other.CompareTag("playerFoot"))
        {
            launched = true;
            playerFoot = other.gameObject;
        }
    }

    IEnumerator launchSetUp()
    {
        yield return new WaitForSeconds(1);
        launchNow = true;
        
    }
}