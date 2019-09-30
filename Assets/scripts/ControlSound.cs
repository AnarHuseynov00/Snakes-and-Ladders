using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Control()
    {
        if (GetComponent<AudioSource>().volume > 0.5)
        {
            GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            GetComponent<AudioSource>().volume = 1;
        }
    }
}
