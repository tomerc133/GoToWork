using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    [Header("Charging Shot")]
    [SerializeField] private float shootMultiply = 5;
    [SerializeField] private float slowDownDivider;
    private bool _chargeUp;
    public bool shotFired;
    [Header("Prefabs to insert")]
    [SerializeField] private GameObject cannonTip;
    [SerializeField] private GameObject[] charactersRb;

    [Header("UI")]
    [SerializeField] private Slider slider;
 
    
    [Tooltip("Check in game mode if values change")][Header("View only Values")]
    public GameObject bulletClone;
    public Rigidbody bulletCloneHips;
    
    [Header("ReadOnly")]
    [SerializeField]private float shootForce = 1;
    [SerializeField] private float hipsVelocity;
    
    
    void Start()
    {
        _chargeUp = true;
        
    }

    // Update is called once per frame

    void Update()
    {
        
        LookAtMouse();

        ShootPlayer();

        slider.value = shootForce;
        
        // view the velocity in inspector
        if(bulletCloneHips != null)
            hipsVelocity = bulletCloneHips.velocity.magnitude;

        Debug.DrawRay(cannonTip.transform.position, cannonTip.transform.up * 35, Color.green, 0.1f);
    }

    private void LookAtMouse()
    {
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
    }

    private void ShootPlayer()
    {
        if (!shotFired)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bulletClone = Instantiate(charactersRb[0], cannonTip.transform.position, cannonTip.transform.rotation);
                bulletCloneHips = bulletClone.transform.GetChild(0).GetComponent<Rigidbody>();
            }

            if (Input.GetMouseButton(0))
            {
                if (shootForce < 500 && _chargeUp)
                    shootForce += shootMultiply * Time.deltaTime;
                if (shootForce >= 500)
                    _chargeUp = false;
                if (!_chargeUp && shootForce > 1)
                    shootForce -= (shootMultiply * Time.deltaTime) / slowDownDivider;
                if (shootForce <= 1)
                    _chargeUp = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (Rigidbody bodyPart in bulletClone.GetComponentsInChildren<Rigidbody>())
                    bodyPart.AddForce(transform.forward * shootForce, ForceMode.Impulse);
                shootForce = 1;
                shotFired = true;
                InvokeRepeating("CheckCloneVelocity", 0.2f, 0.1f);
            }
        }
    }

    void CheckCloneVelocity()
    {
        if (bulletCloneHips.velocity.magnitude < 0.1)
        {
            shotFired = false;
            CancelInvoke("CheckCloneVelocity");
        }
    }
}