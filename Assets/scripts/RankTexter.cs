using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankTexter : MonoBehaviour
{
    // Start is called before the first frame update
    GameParams coreObj;
    int count;
    [SerializeField] GameObject textComp;
    ArrayList texts = new ArrayList();
    //GameObject currentPlayerText;
    void Start()
    {
        coreObj = FindObjectOfType<GameParams>();
        count = FindObjectOfType<PlayerCounter>().getPlayerCount();
        spawnTexts();
    }

    // Update is called once per frame
    void Update()
    {
        HighlightCurrentPlayer();
    }
    private void spawnTexts()
    {
        for(int i = 0; i < count; i++)
        {
            GameObject newOne = Instantiate(textComp, new Vector3(transform.position.x + (float)0.4, transform.position.y - i * (float)0.5, transform.position.z), transform.rotation);  ;
            newOne.transform.localScale = new Vector3((float)0.0125, (float)0.0125);
            newOne.GetComponent<Text>().color = new Color((i == 2 ? 1 : 0), (i == 1 ? 1 : 0), (i == 0 ? 1 : 0));
            newOne.transform.parent = transform;
            newOne.GetComponent<Text>().text = coreObj.getplayerByIndex(i).GetComponent<Movement>().getPlayerName();
            texts.Add(newOne);
        }
    }
    private void HighlightCurrentPlayer()
    {
        for (int i = 0; i < count; i++)
        {
            ((GameObject)texts[i]).GetComponent<Text>().fontStyle = FontStyle.Normal;
        }
        ((GameObject)texts[coreObj.getCurrentPlayerIndex()]).GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
    }
}
