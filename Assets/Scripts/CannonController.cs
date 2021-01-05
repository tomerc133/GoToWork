using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject cannonTip;
    [SerializeField] private GameObject[] charactersRb;
    [SerializeField] private float shootMultiply = 5;
    [SerializeField] private float slowDownDivider;
    [SerializeField] private Slider slider;
    private bool _chargeUp;
    [Header("ReadOnly")]
    [SerializeField]private float shootForce = 1;

    private GameObject bulletClone;
    
    void Start()
    {
        _chargeUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
        
        if(Input.GetMouseButtonDown(0))
            bulletClone = Instantiate(charactersRb[0], cannonTip.transform.position, cannonTip.transform.rotation);
{        if (Input.GetMouseButton(0))
        {
            if(shootForce<500 && _chargeUp)
                shootForce +=  shootMultiply * Time.deltaTime;
            if (shootForce >= 500)
                _chargeUp = false;
            if (!_chargeUp && shootForce > 1)
                shootForce -= (shootMultiply * Time.deltaTime) / slowDownDivider;
            if (shootForce <= 1)
                _chargeUp = true;
            
        }}
        if (Input.GetMouseButtonUp(0))
        {
            foreach(Rigidbody bodyPart in bulletClone.GetComponentsInChildren<Rigidbody>())
                bodyPart.AddForce(transform.forward*shootForce,ForceMode.Impulse);
            shootForce = 1;
        }
        slider.value = shootForce;



        Debug.DrawRay(cannonTip.transform.position, cannonTip.transform.up * 35, Color.green, 0.1f);

    
    }
}
