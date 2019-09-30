using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultRecorder : MonoBehaviour
{
    string winner = "undefined";
    bool setted = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 2)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setWinner(string name)
    {
        if (setted == false)
        {
            winner = name;
        }
        setted = true;
    }
    public string getWinner()
    {
        return winner;
    }
    public bool IsSetted()
    {
        return setted;
    }
}
