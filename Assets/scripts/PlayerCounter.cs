using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour
{
    int playerCount = 0;
    bool done = false;
    const int MaxNumOfPlayers = 4;
    int currentSceneIndex;
    [SerializeField] AudioSource sound;
    [SerializeField] GameObject[] inputFields = new GameObject[4];
    ArrayList playerNames = new ArrayList();
    [SerializeField] GameObject addPlayer;
    [SerializeField] GameObject InputFieldsHolder;
    bool notFilled = true;
    // Start is called before the first frame update
    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        sound = GetComponent<AudioSource>();
        for(int i = 0; i < MaxNumOfPlayers; i++)
        {
            inputFields[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        setNames();
    }
    public int getPlayerCount()
    {
        return playerCount;
    }
    public void increment()
    {
        if (playerCount < MaxNumOfPlayers)
        {
            playerCount++;
            sound.Play(0);
            inputFields[playerCount - 1].SetActive(true);
            inputFields[playerCount - 1].GetComponent<InputField>().placeholder.GetComponent<Text>().text = "add name";
            playerNames.Add("player " + playerCount);
        }
    }
    public int getMaxNumOfP()
    {
        return MaxNumOfPlayers;
    }
    public void decrement()
    {
        if(playerCount > 0)
        {
            playerCount--;
            sound.Play(0);
            inputFields[playerCount].SetActive(false);
            playerNames.Remove(playerCount);
        }
    }
    private void setNames()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (inputFields[i].GetComponent<InputField>().textComponent.GetComponent<Text>().text.Trim() != "")
                {
                    playerNames[i] = inputFields[i].GetComponent<InputField>().textComponent.GetComponent<Text>().text;
                    notFilled = false;
                }
                else
                {
                    //playerNames[i] = inputFields[i].GetComponent<InputField>().placeholder.GetComponent<Text>().text;
                    notFilled = true;
                }
            }
        }
    }
    public bool getNotFilled()
    {
        return notFilled;
    }
    public void Eliminate()
    {
        Destroy(gameObject);
    }
    public string getPlayerNameByIndex(int i)
    {
        if (i < playerCount && i >= 0)
        {
            return (string)playerNames[i];
        }
        else
        {
            return "undefined_Name";
        }
    }
}