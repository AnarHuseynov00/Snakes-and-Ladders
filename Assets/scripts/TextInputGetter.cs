using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInputGetter : MonoBehaviour
{
    PlayerCounter counter;

    private void Start()
    {
        counter = FindObjectOfType<PlayerCounter>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Current number of players is " + counter.getPlayerCount().ToString();
    }
}
