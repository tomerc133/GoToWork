using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    private Rocket _rocketParent;
    
    // Start is called before the first frame update
    void Start()
    {
        _rocketParent = GetComponentInParent<Rocket>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DetachPlayer"))
        {
            _rocketParent.detachPlayer = true;
            _rocketParent.playerDetached = true;
        }

        if (other.CompareTag("BackWallCollider"))
        {
            
            _rocketParent.selfDestroy = true;
            
        }
            

    }
}
