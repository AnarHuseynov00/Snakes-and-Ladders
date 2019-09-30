using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
{
    GameParams coreObject;

    // Start is called before the first frame update
    void Start()
    {
        coreObject = FindObjectOfType<GameParams>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (coreObject.IsGameStarted() == false)
        {
            //coreObject.spawnPlayer();
        }
    }
}
