using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "Winner is " + FindObjectOfType<resultRecorder>().getWinner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
