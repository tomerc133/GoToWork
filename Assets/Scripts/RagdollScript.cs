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
    private Rigidbody _rigidbody;

    private Scene _scene;
    private string sceneName;
    private float _velocity;
    private bool inTheRightFloor =false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        _camera = Camera.main.transform.GetComponent<CameraScript>();
        _scene = SceneManager.GetActiveScene();
        sceneName = _scene.name;
        _rigidbody = transform.parent.GetComponent<Rigidbody>();

    }
    
    // Update is called once per frame
    void Update()
    {
        _velocity =_rigidbody.velocity.magnitude;
        if (_velocity < 0.5f && inTheRightFloor)
        if (_velocity < 0.5f && inTheRightFloor)
        {
            GameManager.addScore();
            inTheRightFloor = false;
        }

        if (sceneName != "MainMenu")
        {
            _camera.ClampCamera();
            transform.parent.position = new Vector3(Mathf.Clamp(transform.parent.position .x, 0, 0), transform.parent.position.y,
                transform.parent.position.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(floorTag))
        {
            inTheRightFloor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(floorTag))
        {
            inTheRightFloor = false;
        }
    }
}