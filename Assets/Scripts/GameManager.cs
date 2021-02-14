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

    private int _score;
    [SerializeField] private GameObject lvlComp;

    [SerializeField] private int MaxScore;

    // Start is called before the first frame update
    void Start()
    {
        _buildingScript = FindObjectOfType<BuildingScript>();
        _ragdollScript = FindObjectOfType<RagdollScript>();
        _cameraScript = FindObjectOfType<CameraScript>();
        _cannonController = FindObjectOfType<CannonController>();
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void LevelComplete()
    {
        StartCoroutine(levelCompEnumerator());
    }

    IEnumerator levelCompEnumerator()
    {
        yield return new WaitForSeconds(7);
        lvlComp.SetActive(true);
        if (_score == MaxScore)
        {
            lvlComp.GetComponent<Animator>().SetTrigger("3stars");
        }

        if (_score == MaxScore - 1)
        {
            lvlComp.GetComponent<Animator>().SetTrigger("2stars");
        }

        if (_score <= MaxScore - 2)
        {
            lvlComp.GetComponent<Animator>().SetTrigger("1stars");
        }
    }


    public void AddScore(int amount)
    {
        _score += amount;
        Debug.Log(_score);
    }
}