using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams : MonoBehaviour
{
    int snakeCount = 7;
    int ladderCount = 5;
    int MAXNUMBEROFPLAYERS = 4;
    public float[] snakeHeadX = new float[7];
    public float[] snakeTailX = new float[7];
    public float[] snakeHeadY = new float[7];
    public float[] snakeTailY = new float[7];
    public float[] ladderHeadX = new float[5];
    public float[] ladderTailX = new float[5];
    public float[] ladderHeadY = new float[5];
    public float[] ladderTailY = new float[5];
    [SerializeField] GameObject snake;
    [SerializeField] GameObject ladder;
    [SerializeField] float mostLeftX;
    [SerializeField] float mostRigthX;
    [SerializeField] float mostTopY;
    [SerializeField] float mostBottomY;
    [SerializeField] GameObject player;
    ArrayList players = new ArrayList();
    GameObject currentPlayer;
    bool gameStarted = false;
    int currentPlayerIndex = 0;
    bool ThereIsPlayer = false;
    int playerCount;
    PlayerCounter counter;
    GameObject RankText;
    // Start is called before the first frame update
    void Start()
    {
        counter = FindObjectOfType<PlayerCounter>();
        playerCount = counter.getPlayerCount();
        MAXNUMBEROFPLAYERS = counter.getMaxNumOfP();
        generateRandomSnakesAndLadders();
        spawnAllLadders();
        spawnAllSnakes();
        spawnAllPlayers();
    }
    public float getMostLeftX()
    {
        return mostLeftX;
    }
    public float getMostBottomY()
    {
        return mostBottomY;
    }
    public int getSnakeCount()
    {
        return snakeCount;
    }
    public int getLadderCount()
    {
        return ladderCount;
    }

    // Update is called once per frame
    void Update()
    {
        check();
        setGameWinner();
    }
    public bool IsGameStarted()
    {
        return gameStarted;
    }
    public void setGameStarted(bool a)
    {
        gameStarted = a;
    }
    public GameObject getCurrentPlayer()
    {
        return currentPlayer;
    }
    private void spawnAllLadders()
    {
        for(int n = 0; n < ladderCount; n++)
        {
            GameObject newOne = Instantiate(ladder, new Vector3(ladderHeadX[n], ladderHeadY[n], 0), transform.rotation);
            ladderPartSpawner spawner = newOne.GetComponent<ladderPartSpawner>();
            spawner.setEnd(new Vector3(ladderTailX[n], ladderTailY[n], 0));
        }
    }
    public int getCurrentPlayerIndex()
    {
        return currentPlayerIndex;
    }
    private void spawnAllPlayers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            spawnPlayer();
            ((GameObject)players[i]).GetComponent<Movement>().setPlayerName(counter.getPlayerNameByIndex(i));
        }
        currentPlayer = (GameObject)players[currentPlayerIndex];
        for (int i = 0; i < playerCount; i++)
        {
            ((GameObject)players[i]).GetComponentInChildren<SpriteRenderer>().color = new Color((i == 2? 1: 0), (i == 1? 1: 0), (i == 0? 1: 0));
        }
        ThereIsPlayer = true;
    }
    public void spawnPlayer()
    {
        Vector3 pos = new Vector3(mostLeftX, mostBottomY, 0);
        GameObject newOne = Instantiate(player, pos, transform.rotation);
        newOne.transform.parent = transform;
        players.Add(newOne);
    }
    public bool IsTherePlayer()
    {
        return ThereIsPlayer;
    }
    public void nextPlayer()
    {
        int firstID = currentPlayer.GetComponent<Movement>().getID();
        currentPlayerIndex = (currentPlayerIndex + 1) % playerCount;
        currentPlayer = (GameObject)players[currentPlayerIndex];
        int counter = 0;
        while(currentPlayer.GetComponent<Movement>().getCurrentX() < 9.2001 && currentPlayer.GetComponent<Movement>().getCurrentY() > 12.1999 && counter < MAXNUMBEROFPLAYERS)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % playerCount;
            currentPlayer = (GameObject)players[currentPlayerIndex];
            counter++;
        }
        int secondID = currentPlayer.GetComponent<Movement>().getID();
        if (playerCount > 1)
        {
            if (firstID == secondID)
            {
                FindObjectOfType<SceenLoader>().loadNextScene();
            }
        }
    }
    private void spawnAllSnakes()
    {
        for (int n = 0; n < snakeCount; n++)
        {
            GameObject newOne = Instantiate(snake, new Vector3(snakeHeadX[n], snakeHeadY[n], 0), transform.rotation);
            snakePartSpawner spawner = newOne.GetComponent<snakePartSpawner>();
            newOne.GetComponent<snakePartSpawner>().snakePart.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0,1), Random.Range(0, 1), Random.Range(0, 1));
            spawner.setEnd(new Vector3(snakeTailX[n], snakeTailY[n], 0));
        }
    }
    private void generateRandomSnakesAndLadders()
    {
        int[] snakeCor = new int[snakeCount * 2];
        int[] ladderCor = new int[ladderCount * 2];
        for(int x = 0; x < snakeCor.Length; x = x + 2)
        {
            int val1 = 0;
            int val2 = 0;
            bool notValid1 = true;
            bool notValid2 = true;
            while(notValid1)
            {
                val1 = Mathf.FloorToInt(Random.Range(9, 90));
                if(val1 != 81)
                {
                    notValid1 = false;
                }
                if(!notValid1)
                {
                    for(int y = 0; y < snakeCor.Length; y++)
                    {
                        if(val1 == snakeCor[y])
                        {
                            notValid1 = true;
                        }
                    }
                }
            }
            snakeCor[x] = val1;
            while(notValid2)
            {
                val2 = Mathf.FloorToInt(Random.Range(1, 81));
                if(val2 < Mathf.FloorToInt(val1/9) * 9)
                {
                    notValid2 = false;
                }
                if(!notValid2)
                {
                    for(int y = 0; y < snakeCor.Length; y++)
                    {
                        if(val2 == snakeCor[y])
                        {
                            notValid2 = true;
                        }
                    }
                }
                snakeCor[x + 1] = val2;
            }
        }
        for(int x = 0; x < ladderCor.Length; x = x + 2)
        {
            int val1 = 0;
            int val2 = 0;
            bool notValid1 = true;
            bool notValid2 = true;
            while (notValid1)
            {
                val1 = Mathf.FloorToInt(Random.Range(0, 81));
                if (val1 != 0)
                {
                    notValid1 = false;
                }
                if (!notValid1)
                {
                    for (int y = 0; y < snakeCor.Length; y++)
                    {
                        if(val1 == snakeCor[y])
                        {
                            notValid1 = true;
                        }
                    }
                    for(int y = 0; y < ladderCor.Length; y++)
                    {
                        if(val1 == ladderCor[y])
                        {
                            notValid1 = true;
                        }
                    }
                }
            }
            ladderCor[x] = val1;
            while (notValid2)
            {
                val2 = Mathf.FloorToInt(Random.Range(9, 90));
                if (val2 >= Mathf.FloorToInt(val1 / 9) * 9 + 9 && val2 <= val1 + 40)
                {
                    notValid2 = false;
                }
                if (!notValid2)
                {
                    for (int y = 0; y < snakeCor.Length; y++)
                    {
                        if (val2 == snakeCor[y])
                        {
                            notValid2 = true;
                        }
                    }
                    for (int y = 0; y < ladderCor.Length; y++)
                    {
                        if (val2 == ladderCor[y])
                        {
                            notValid2 = true;
                        }
                    }
                }
                ladderCor[x + 1] = val2;
            }
        }
        for (int x = 0; x < snakeCor.Length; x = x + 2)
        {
            Debug.Log(x / 2);
            snakeHeadX[x / 2] = (float)(mostLeftX + (snakeCor[x] - (Mathf.FloorToInt(snakeCor[x] / 9) * 9)) * 0.7);
            snakeHeadY[x / 2] = (float)(mostBottomY + Mathf.FloorToInt(snakeCor[x] / 9) * 0.7);
            snakeTailX[x / 2] = (float)(mostLeftX + (snakeCor[x + 1] - (Mathf.FloorToInt(snakeCor[x + 1] / 9) * 9)) * 0.7);
            snakeTailY[x / 2] = (float)(mostBottomY + Mathf.FloorToInt(snakeCor[x + 1] / 9) * 0.7);
        }
        for (int x = 0; x < ladderCor.Length; x = x + 2)
        {
            ladderHeadX[x / 2] = (float)(mostLeftX + (ladderCor[x] - (Mathf.FloorToInt(ladderCor[x] / 9) * 9)) * 0.7);
            ladderHeadY[x / 2] = (float)(mostBottomY + Mathf.FloorToInt(ladderCor[x] / 9) * 0.7);
            ladderTailX[x / 2] = (float)(mostLeftX + (ladderCor[x + 1] - (Mathf.FloorToInt(ladderCor[x + 1] / 9) * 9)) * 0.7);
            ladderTailY[x / 2] = (float)(mostBottomY + Mathf.FloorToInt(ladderCor[x + 1] / 9) * 0.7);
        }

    }
    private void check()
    {
        int count = 0;
        foreach(object player in players)
        {
            if(((GameObject)player).GetComponent<Movement>().getCurrentX() < 9.2001 && 
                ((GameObject)player).GetComponent<Movement>().getCurrentY() > 12.1999)
            {
                count ++;
            }
        }
        if (playerCount == 1)
        {
            if (count == playerCount)
            {
                FindObjectOfType<SceenLoader>().loadNextScene();
            }
        }
        else
        {
            if(count == playerCount - 1)
            {
                FindObjectOfType<SceenLoader>().loadNextScene();
            }
        }
    }
    public int getPlayerC()
    {
        return playerCount;
    }
    public GameObject getplayerByIndex(int a)
    {
        return (GameObject)players[a];
    }
    private void setGameWinner()
    {
        if(currentPlayer.GetComponent<Movement>().getCurrentX() < 9.3 && currentPlayer.GetComponent<Movement>().getCurrentY() > 12.1)
        {
            FindObjectOfType<resultRecorder>().setWinner(currentPlayer.GetComponent<Movement>().getPlayerName());
        }
    }
}