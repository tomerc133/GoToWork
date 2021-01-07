using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RagdollScript : MonoBehaviour
{
    [Tooltip("INSERT TAG IN HERE U BABY, RUSH B NOOB TEAM")] [SerializeField]
    private string floorTag;

    private bool sloMo = false;

    public bool scaleTime;
    private GameManager GameManager;
    private CameraScript _camera;

    private Scene _scene;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        _camera = Camera.main.transform.GetComponent<CameraScript>();
        _scene = SceneManager.GetActiveScene();
        sceneName = _scene.name;

    }
    
    // Update is called once per frame
    void Update()
    {
        // if (sloMo)
        // {
        //     float currentTime = Time.time;
        //     if (Time.time - currentTime > 1)
        //     {
        //         Time.timeScale = 1;
        //     }
        // }
        if(sceneName != "MainMenu")
            _camera.ClampCamera();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(floorTag))
        {
           GameManager.addScore();
        }
    }
}