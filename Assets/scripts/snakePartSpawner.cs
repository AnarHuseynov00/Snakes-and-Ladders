using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakePartSpawner : MonoBehaviour
{
    [SerializeField] public GameObject snakePart;
    [SerializeField] GameObject snakeHead;
    [SerializeField] GameObject snakeTail;
    [SerializeField] Vector3 beginning;
    [SerializeField] Vector3 end;
    // Start is called before the first frame update
    void Start()
    {
        //snakePart.GetComponent<SpriteRenderer>().color = new Color(0, 0.4f, 0.4f);
        beginning = transform.position;
        Invoke("drawCompSnake", 0.1f);
        //drawCompSnake();
        //spawnSnakes(calcSnakeCount(beginning, end),
       //     getSlope(beginning, end));
    }
    private float getSlope(Vector3 one, Vector3 two)
    {
        return Mathf.Atan2(two.y - one.y, two.x - one.x) * Mathf.Rad2Deg - 90;
    }

    private int calcSnakeCount(Vector3 beginning, Vector3 end)
    {
        float xValue = Mathf.Abs(beginning.x - end.x);
        float yValue = Mathf.Abs(beginning.y - end.y);
        float length = Mathf.Sqrt(Mathf.Pow(xValue, 2) + Mathf.Pow(yValue, 2));
        if(Mathf.FloorToInt(length / (float)0.7) == 0)
        {
            return 1;
        }
        else
        {
            return Mathf.FloorToInt((length + (float)0.001) / (float)0.7);
        }
    }
    private float calcYScaleInc(Vector3 beginning, Vector3 end)
    {
        float xValue = Mathf.Abs(beginning.x - end.x);
        float yValue = Mathf.Abs(beginning.y - end.y);
        float length = Mathf.Sqrt(Mathf.Pow(xValue, 2) + Mathf.Pow(yValue, 2));
        //Debug.Log(length);
        return length / (calcSnakeCount(beginning, end) * (float)0.7);

    }
    /*public void setBeginning(Vector3 a)
    {
        beginning = a;
    }*/
    private void drawCompSnake()
    {
        spawnSnakes(calcSnakeCount(transform.position, end), getSlope(transform.position, end));
        transform.localScale = new Vector3(transform.localScale.x, calcYScaleInc(transform.position, end) - (float)0.02, transform.localScale.z);
    }
    public void setEnd(Vector3 a)
    {
        end = a;
    }
    /*public Vector3 getBeginning()
    {
        return beginning;
    }*/
    public Vector3 getEnd()
    {
        return end;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void spawnSnakes(int a, float slope)
    {
        GameObject newOne;
        if (end.x > beginning.x)
        {
            Vector3 locationHead = new Vector3(transform.position.x - (float)0.09, transform.position.y - (float)0.24, transform.position.z);
            newOne = Instantiate(snakeHead, locationHead, transform.rotation);
            newOne.transform.localScale = new Vector3((float)0.62, (float)-0.5);
            newOne.transform.parent = transform;
        }
        else
        {
            Vector3 locationHead = new Vector3(transform.position.x - (float)0.09, transform.position.y - (float)0.24, transform.position.z);
            newOne = Instantiate(snakeHead, locationHead, transform.rotation);
            newOne.transform.localScale = new Vector3((float)-0.62, (float)-0.5);
            newOne.transform.parent = transform;
        }
        for (int x = 0; x < a; x++)
        {
            Vector3 location = new Vector3(transform.position.x, transform.position.y + x * (float)(0.7) + (float)0.35, transform.position.z);
            newOne = Instantiate(snakePart, location, transform.rotation);
            newOne.transform.parent = transform;
        }
        Vector3 locationTail = new Vector3(transform.position.x - (float)0.094, transform.position.y + a * (float)(0.7) + (float)0.08, transform.position.z);
        newOne = Instantiate(snakeTail, locationTail, transform.rotation);
        newOne.transform.localScale = new Vector3((float)-0.64, (float)-0.499);
        newOne.transform.parent = transform;
        transform.Rotate(0, 0, slope);
    }
}
