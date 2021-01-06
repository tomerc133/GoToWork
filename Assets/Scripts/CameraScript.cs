using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    private CannonController cannon;
    private Vector3 startPos;
    [SerializeField] Vector3 offset;
    [SerializeField] private float cameraSpeed;
    private Transform bulletCloneHips;
    void Awake()
    {
        cannon = FindObjectOfType<CannonController>();
        startPos = transform.position;
    }

    private void Start()
    {
        Debug.Log(cannon.gameObject.name);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (cannon.shotFired)
        {
            bulletCloneHips = cannon.bulletClone.transform.GetChild(0);
            transform.position = Vector3.Lerp(transform.position,
                bulletCloneHips.position + offset, 0.01f * cameraSpeed);
            // store last vector
        }
        
        if(!cannon.shotFired)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 0.01f * cameraSpeed * 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.B))
            cannon.shotFired = false;
    }

    void Update()
    {

    }
}
