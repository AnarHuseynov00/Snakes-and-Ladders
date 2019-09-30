using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderPartSpawner : MonoBehaviour
{
    [SerializeField] GameObject ladderPart;
    [SerializeField] Vector3 beginning;
    [SerializeField] Vector3 end;
    // Start is called before the first frame update
    void Start()
    {
        beginning = transform.position;
        Invoke("drawCompLadder", 0.1f);
       //spawnLadders(calcLadderCount(new Vector3(12, 8, 0), new Vector3((float)14.1, (float)10.8, 0)), 
            //getSlope(new Vector3(12, 8, 0), new Vector3((float)14.1, (float)10.8, 0)));
    }
    private float getSlope(Vector3 one, Vector3 two)
    {
        return Mathf.Atan2(two.y - one.y, two.x - one.x) * Mathf.Rad2Deg - 90;
    }

    private int calcLadderCount(Vector3 beginning, Vector3 end)
    {
        float xValue = Mathf.Abs(beginning.x - end.x);
        float yValue = Mathf.Abs(beginning.y - end.y);
        float length = Mathf.Sqrt(Mathf.Pow(xValue, 2) + Mathf.Pow(yValue, 2));
        return Mathf.FloorToInt((length + (float)(0.001)) / (float)0.35);
    }
    public void setBeginning(Vector3 a)
    {
        beginning = a;
    }
    private void drawCompLadder()
    {
        spawnLadders(calcLadderCount(beginning, end), getSlope(beginning, end));
        transform.localScale = new Vector3(transform.localScale.x, calcYScaleInc(transform.position, end), transform.localScale.z);
        //Debug.Log(calcYScaleInc(transform.position, end));
    }
    public void setEnd(Vector3 a)
    {
        end = a;
    }
    public Vector3 getBeginning()
    {
        return beginning;
    }
    public Vector3 getEnd()
    {
        return end;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnLadders(int a, float slope)
    {
        GameObject newOne;
        for (int x = 0; x < a; x++)
        {
            Vector3 location = new Vector3(transform.position.x, transform.position.y + x * (float)(0.35) + (float)0.15, transform.position.z);
            newOne = Instantiate(ladderPart, location, transform.rotation);
            newOne.transform.parent = transform;
        }
        transform.Rotate(0, 0, slope);
    }
    private float calcYScaleInc(Vector3 beginning, Vector3 end)
    {
        float xValue = Mathf.Abs(beginning.x - end.x);
        float yValue = Mathf.Abs(beginning.y - end.y);
        float length = Mathf.Sqrt(Mathf.Pow(xValue, 2) + Mathf.Pow(yValue, 2));
        return length / (calcLadderCount(beginning, end) * (float)0.35);
    }
}
