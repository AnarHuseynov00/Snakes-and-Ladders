using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceenLoader : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] int timeToWait = 4;
    [SerializeField] int ecsPressed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }
    public void StartGame()
    {
        if(FindObjectOfType<PlayerCounter>().getPlayerCount() > 0 && FindObjectOfType<PlayerCounter>().getNotFilled() == false)
        {
            loadNextScene();
        }
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        loadNextScene();
    }
    public void loadNextScene()
    {
        SceneManager.LoadScene((currentSceneIndex + 1) % 4);
        if(currentSceneIndex == 2)
        {
            FindObjectOfType<PlayerCounter>().Eliminate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        Ecs();
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void ReStart()
    {
        SceneManager.LoadScene(1);
        Destroy(FindObjectOfType<resultRecorder>().gameObject);
    }
    public void Ecs()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && ecsPressed == 0)
            {
                ecsPressed = ecsPressed + 1;
                StartCoroutine(CancelECS());
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && ecsPressed == 1)
            {
                Destroy(FindObjectOfType<PlayerCounter>().gameObject);
                ReStart();
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && ecsPressed == 0)
            {
                ecsPressed = ecsPressed + 1;
                StartCoroutine(CancelECS());
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && ecsPressed == 1)
            {
                quitGame();
            }
        }
    }
    IEnumerator CancelECS()
    {
        yield return new WaitForSeconds(1f);
        ecsPressed = ecsPressed - 1;
    }
}
