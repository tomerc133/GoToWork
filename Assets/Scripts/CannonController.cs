using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    [Header("Charging Shot")] 
    [SerializeField] private float shootMultiply = 5;

    [SerializeField] private float maxShootForce = 500f;

    [SerializeField] private float slowDownDivider;
    private bool _chargeUp;
    private int characterIndex = 0;
    public bool shotFired;

    [Header("Prefabs to insert")] [SerializeField]
    private GameObject cannonTip;
    [SerializeField] private GameObject cannonFire;
    
    [SerializeField] private GameObject[] charactersRb;

    [Header("UI")]
    [SerializeField] private Slider slider;
   


    [Tooltip("Check in game mode if values change")] [Header("View only Values")]
    public GameObject bulletClone;

    public Rigidbody bulletCloneHips;

    [Header("ReadOnly")] [SerializeField] private float shootForce = 1;
    [SerializeField] private float hipsVelocity;
    private int numOfShoots=0;


    //refrences
    private AudioManager _audioManager;
    private Transform _realCannonTip;
    
    void Start()
    {
        _realCannonTip = transform.GetChild(3).transform;
        _audioManager = FindObjectOfType<AudioManager>();
        if (_audioManager == null)
            Debug.Log("No Audio Manager Found");
        
        _chargeUp = true;
        slider.maxValue = maxShootForce;
        
    }

    // Update is called once per frame

    void Update()
    {
        LookAtMouse();

        ShootPlayer();

        slider.value = shootForce;
        

        // view the velocity in inspector
        if (bulletCloneHips != null)
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
        if (!shotFired && !allTheCharactersWereShot())
        {
            if (Input.GetMouseButtonDown(0))
            {
                bulletClone = Instantiate(charactersRb[characterIndex], cannonTip.transform.position,
                    cannonTip.transform.rotation);
                bulletCloneHips = bulletClone.transform.GetChild(0).GetComponent<Rigidbody>();
                
            }

            if (Input.GetMouseButton(0))
            {
                bulletCloneHips.isKinematic = true;
                bulletClone.transform.rotation = cannonTip.transform.rotation;
                bulletClone.transform.position = cannonTip.transform.position;
                if (shootForce < maxShootForce && _chargeUp)
                {
                    shootForce += shootMultiply * Time.deltaTime;
                    _audioManager.PlayChargingUp();
                }
                   
                if (shootForce >= maxShootForce)
                    _chargeUp = false;
                if (!_chargeUp && shootForce > 1)
                {
                    _audioManager.PlayChargingDown();
                    shootForce -= (shootMultiply * Time.deltaTime) / slowDownDivider;
                }

                if (shootForce <= 1)
                {
                    _chargeUp = true;
                    _audioManager.ResetSounds();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                bulletCloneHips.isKinematic = false;
                foreach (Rigidbody bodyPart in bulletClone.GetComponentsInChildren<Rigidbody>())
                    bodyPart.AddForce(transform.forward * shootForce, ForceMode.Impulse);
                GameObject partcileClone = Instantiate(cannonFire, _realCannonTip.position, transform.rotation);
                Destroy(partcileClone, 2.5f);
                shootForce = 1;
                shotFired = true;
                numOfShoots++;
                InvokeRepeating("CheckCloneVelocity", 0.2f, 0.1f);
                _audioManager.PlayBoomSound();
                _audioManager.ResetSounds();
                _audioManager.stopChargingSound = true;
                
            }
        }
    }

    void CheckCloneVelocity()
    {
        if (bulletCloneHips.velocity.magnitude < 0.5)
        {
            shotFired = false;
            if (charactersRb.Length-1  > characterIndex)
            {
                characterIndex++;
                
            }
            CancelInvoke("CheckCloneVelocity");
        }
    }

    public bool allTheCharactersWereShot()
    {
        return charactersRb.Length == numOfShoots;
    }
}