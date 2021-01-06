using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CannonController _cannonController;

    private CameraScript _cameraScript;

    private RagdollScript _ragdollScript;

    private BuildingScript _buildingScript;
    // Start is called before the first frame update
    void Start()
    {
        _buildingScript = FindObjectOfType<BuildingScript>();
        _ragdollScript = FindObjectOfType<RagdollScript>();
        _cameraScript = FindObjectOfType<CameraScript>();
        _cannonController = FindObjectOfType<CannonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }
}
