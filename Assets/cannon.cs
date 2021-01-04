using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonTip;
    [SerializeField] private GameObject bullet;
    private float shootForce = 1;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(shootForce);
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
        if (Input.GetMouseButton(0))
        {
            if(shootForce<500)
                shootForce += 1;
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            GameObject bulletClone= Instantiate(bullet, cannonTip.transform.position, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward*shootForce,ForceMode.Impulse);
            shootForce = 1;
        }
        
    }
}
