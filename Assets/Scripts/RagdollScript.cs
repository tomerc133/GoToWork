using System;
using System.Collections;
using UnityEngine;


public class RagdollScript : MonoBehaviour
{
    [Tooltip("INSERT TAG IN HERE U BABY, RUSH B NOOB TEAM")]
    [SerializeField] private string floorTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(floorTag))
            Debug.Log("good job you soofbich im in!");
    }
}
