using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{    
    public bool detachPlayer;
    [SerializeField] private bool launched = false;
    [SerializeField] private float rocketSpeed;
    private Rigidbody _rb;
    private bool launchNow = false;
    private Vector3 _flyTo;
    private Vector3 _flyToGameObject;
    private GameObject playerFoot;
    private GameObject player;
    
    private bool footIsAttached;
    private Transform rocket;
    // timing
    private float forceTimer;
    public bool playerDetached;
    // explosion
    [Header("Explosion")] 
    public bool selfDestroy;
    [SerializeField] private float explosionForce;
    [SerializeField] float radius;
    [SerializeField] private float addForceForTime;
    private Vector3 explosionPos;
    
   
    private float explosionTimer;
    

// Start is called before the first frame update
    void Start()
    {
        rocket = transform.GetChild(0).transform;
        detachPlayer = true;
        launched = false;
        _flyToGameObject = transform.GetChild(1).position;
        _flyTo = new Vector3(_flyToGameObject.x, _flyToGameObject.y, _flyToGameObject.z);
        _rb = transform.GetChild(0).GetComponent<Rigidbody>();
        footIsAttached = false;
        playerDetached = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Vector3.Distance(rocket.position, _flyTo) < 1)
        {
            launched = false;
            launchNow = false;
        }

        if (!detachPlayer && footIsAttached)
        {
            playerFoot.transform.position = rocket.position;
            player.transform.position = rocket.GetChild(0).position;
        }

        if (launched)
        {
            if (launchNow)
            {
                _rb.AddForce(transform.forward * rocketSpeed, ForceMode.Force);
                foreach (Rigidbody bodyPart in player.GetComponentsInChildren<Rigidbody>())
                {
                    bodyPart.useGravity = false;
                }
            }
            else
            {
                StartCoroutine(launchSetUp());
            }
        }

        if (playerDetached)
        {
            forceTimer += Time.deltaTime;
            if (forceTimer < 1.5f && player != null)
            {
                foreach (Rigidbody bodyPart in player.GetComponentsInChildren<Rigidbody>())
                {
                    bodyPart.useGravity = true;
                    bodyPart.AddForce(Vector3.forward*1.5f, ForceMode.Impulse);
                    bodyPart.AddForce(Vector3.up*2, ForceMode.Impulse);
                }
            }
        }
        if (selfDestroy)
        {
            explosionPos = _rb.transform.position;
         
            if (explosionTimer < addForceForTime)
            {
                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
                foreach (Collider hit in colliders)
                {
                    if (hit.attachedRigidbody)
                        hit.attachedRigidbody.AddExplosionForce(15, explosionPos, radius, 5, ForceMode.Force);
                }
            }
            launchNow = false;
            _rb.isKinematic = true;
        }
    
    }

    private void Update()
    {
        if(selfDestroy)
            explosionTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            detachPlayer = false;
        }

        if (other.CompareTag("playerFoot"))
        {
            launched = true;
            playerFoot = other.gameObject;
            footIsAttached = true;
        }
    }

    IEnumerator launchSetUp()
    {
        yield return new WaitForSeconds(0.5f);
        launchNow = true;
    }
    
}