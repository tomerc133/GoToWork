using System;
using System.Collections;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField] private GameObject glassFrac;
    [SerializeField] private GameObject glassFilled;
    private GameObject volumeChild;
    [SerializeField] int volumeIndex;
    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        volumeChild = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // do not touch , somehow it works
        if (other.CompareTag("Player") && glassFilled != null)
        {
            i = 1;
            if (i <= 1)
            {
                Instantiate(glassFrac, glassFilled.transform.position, glassFilled.transform.rotation);
                if(glassFilled != null)
                    Destroy(glassFilled);
            }
        }
        if (glassFilled == null)
            Debug.Log("Glass filled does not exist");
    }
    private void OnTriggerExit(Collider other)
    {
        i = 0;
    }
}