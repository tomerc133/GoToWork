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
    private bool detachPlayer;

// Start is called before the first frame update
    void Start()
    {
        detachPlayer = true;
        launched = false;
        _flyToGameObject = transform.GetChild(0).position;
        _flyTo = new Vector3(_flyToGameObject.x, _flyToGameObject.y, _flyToGameObject.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _flyTo) < 20)
        {
            foreach (Rigidbody bodyPart in player.GetComponentsInChildren<Rigidbody>())
            {
                bodyPart.AddForce(transform.forward , ForceMode.Impulse);
            }

            detachPlayer = true;
        }


        if (Vector3.Distance(transform.position, _flyTo) < 1)
        {
            launched = false;
            launchNow = false;
        }

        if (!detachPlayer)
        {
            playerFoot.transform.position = transform.position;
            player.transform.position = transform.GetChild(1).position;
        }

        if (launched)
        {
            if (launchNow)
            {
                transform.position = Vector3.Lerp(transform.position, _flyTo, 7 * Time.deltaTime);
            }
            else
            {
                StartCoroutine(launchSetUp());
            }
        }
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
        }
    }

    IEnumerator launchSetUp()
    {
        yield return new WaitForSeconds(0.5f);
        launchNow = true;
    }
}