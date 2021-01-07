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
    

    [SerializeField] private Transform volumeTrasnform;
    [SerializeField] private float clampZOffset = 7;
    void Awake()
    {
        cannon = FindObjectOfType<CannonController>();
        startPos = transform.position;
    }

    private void Start()
    {
        // Debug.Log(cannon.gameObject.name);
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

    public void ClampCamera()
    {
        float clampedY = Mathf.Clamp(transform.position.y, 7, 55);
        float clampedZ = Mathf.Clamp(transform.position.z, 0, volumeTrasnform.position.z + clampZOffset);
        Vector3 clampedPos = new Vector3(transform.position.x, clampedY, clampedZ);
        transform.position = clampedPos;
    }
}
