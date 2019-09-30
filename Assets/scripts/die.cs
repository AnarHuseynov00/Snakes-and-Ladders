using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    int dieValue;
    BoxCollider2D coll;
    Animator animator;
    bool IsRolling = false;
    GameParams coreObject;
    AudioSource rollSound;
    //this bool is used in order to not skip first player in first die rolling
    bool firstPlayerActive = true;
    // Start is called before the first frame update
    void Start()
    {
        coreObject = FindObjectOfType<GameParams>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("DieValue", 6);
        coll.enabled = false;
        rollSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coreObject.IsTherePlayer())
        {
            controlButtonActiveness();
        }
    }
    private void rollDies()
    {
        dieValue = Mathf.FloorToInt(Random.Range(1, 7));
        animator.SetInteger("DieValue", 0);
        IsRolling = true;
        StartCoroutine(WaitForRolling());
        rollSound.Play(0);
    }
    IEnumerator WaitForRolling()
    {
        yield return new WaitForSeconds(1f);
        if(dieValue == 1)
        {
            animator.SetInteger("DieValue", 1);
            IsRolling = false;
        }
        else if (dieValue == 2)
        {
            animator.SetInteger("DieValue", 2);
            IsRolling = false;
        }
        else if (dieValue == 3)
        {
            animator.SetInteger("DieValue", 3);
            IsRolling = false;
        }
        else if (dieValue == 4)
        {
            animator.SetInteger("DieValue", 4);
            IsRolling = false;
        }
        else if (dieValue == 5)
        {
            animator.SetInteger("DieValue", 5);
            IsRolling = false;
        }
        else if (dieValue == 6)
        {
            animator.SetInteger("DieValue", 6);
            IsRolling = false;
        }
    }
    private void controlButtonActiveness()
    {
        Movement movement = coreObject.getCurrentPlayer().GetComponent<Movement>();
        if(Mathf.Abs(movement.getCurrentX() - movement.getTargetX()) < 0.001 && 
            Mathf.Abs(movement.getCurrentY() - movement.getTargetY()) < 0.001)
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
    }
    public bool Rolling()
    {
        return IsRolling;
    }
    public int getDieValue()
    {
        return dieValue;
    }
    private void OnMouseDown()
    {
        coreObject.setGameStarted(true);
        if(firstPlayerActive == false)
        {
            coreObject.nextPlayer();
        }
        firstPlayerActive = false;
        rollDies();
        if(coreObject.getCurrentPlayer().GetComponent<Movement>().getDragVal() != 0)
        {
            coreObject.getCurrentPlayer().GetComponent<Movement>().drag(5 -
                coreObject.getCurrentPlayer().GetComponent<Movement>().getDragVal());
            coreObject.getCurrentPlayer().GetComponent<Movement>().setDragVal(0);
        }
        coreObject.getCurrentPlayer().GetComponent<Movement>().setTargetByDieValue();
    }
}
