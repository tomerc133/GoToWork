
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    private AudioSource _as;
    
    [SerializeField] private AudioClip[] boomSounds;
    [SerializeField] private AudioClip chargingUp;
    [SerializeField] private AudioClip chargingDown;
    private bool _isCupDone,_isCDownDone,_isBoomSoundDone = false;
    public bool stopChargingSound = false;
    
    void Start()
    {
        _as = GetComponent<AudioSource>();
    }


    public void PlayBoomSound()
    {
        if (!_isBoomSoundDone)
        {
            _as.Stop();
            int rand = Random.Range(0, boomSounds.Length);
            _as.PlayOneShot(boomSounds[rand], 0.5f);
            _isBoomSoundDone = true;
        }
        
    }

    public void PlayChargingUp()
    {
        if (!_isCupDone)
        {
            _as.Stop();
            _as.PlayOneShot(chargingUp, 1f);
            _isCupDone = true;
        }
        
    }
    
    public void PlayChargingDown()
    {
        if (!_isCDownDone)
        {
            _as.Stop();
            _as.PlayOneShot(chargingDown, 1f);
            _isCDownDone = true;
        }

    

    }

    public void ResetSounds()
    {
        _isCupDone = false;
        _isCDownDone = false;
        _isBoomSoundDone = false;
    }

    
}
