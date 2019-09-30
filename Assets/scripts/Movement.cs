using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float currentX;
    [SerializeField] float currentY;
    [SerializeField] float targetX;
    [SerializeField] float targetY;
    [SerializeField] float mostLeftX;
    [SerializeField] float mostRightX;
    [SerializeField] int ID;
    Animator animator;
    die CurrentDie;
    [SerializeField] int moveDirection = 1;
    GameParams coreGame;
    [SerializeField] int moveType = 1;
    int dragVal = 0;
    float R;
    float G;
    float B;
    [SerializeField] string playerName;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (ID == coreGame.getCurrentPlayer().GetComponent<Movement>().getID())
        {
            totalControl();
        }
    }
    private void Start()
    {
        coreGame = FindObjectOfType<GameParams>();
        currentX = coreGame.getMostLeftX();
        currentY = coreGame.getMostBottomY();
        animator = GetComponent<Animator>();
        CurrentDie = FindObjectOfType<die>();
        ID = Mathf.FloorToInt(Random.Range(1, 10000));
        R = Random.Range(1, 256);
        G = Random.Range(1, 256);
        B = Random.Range(1, 256);
        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    public string getPlayerName()
    {
        return playerName;
    }
    public void setPlayerName(string name)
    {
        playerName = name;
    }
    public float getCurrentX()
    {
        return currentX;
    }
    public void totalControl()
    {
        if (CurrentDie.Rolling() == false)
        {
            controlMove();
        }
    }
    public float getCurrentY()
    {
        return currentY;
    }
    public float getTargetX()
    {
        return targetX;
    }
    public float getTargetY()
    {
        return targetY; ;
    }
    public void SetSpeed(float speed)
    {
        MoveSpeed = speed;
    }
    private void moveToRight()
    {
        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        transform.Translate(Vector2.right * Time.deltaTime * MoveSpeed);
    }
    private void moveToLeft()
    {
        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        transform.Translate(Vector2.left * Time.deltaTime * MoveSpeed);
    }
    private void moveToUp()
    {
        transform.Translate(Vector2.up * Time.deltaTime * MoveSpeed);
    }
    private void moveToDown()
    {
        transform.Translate(Vector2.down * Time.deltaTime * MoveSpeed);
    }
    private int findDirection()
    {
        float posY = currentY;
        if(posY > 12.199)
        {
            return -1;
        }
        else if(posY >= 11.499)
        {
            return 1;
        }
        else if (posY >= 10.799)
        {
            return -1;
        }
        else if (posY >= 10.099)
        {
            return 1;
        }
        else if (posY >= 9.399)
        {
            return -1;
        }
        else if (posY > 8.699)
        {
            return 1;
        }
        else if (posY > 7.999)
        {
            return -1;
        }
        else if (posY >= 7.299)
        {
            return 1;
        }
        else if (posY >= 6.599)
        {
            return -1;
        }
        else if (posY >= 5.899)
        {
            return 1;
        }
        else
        {
            return 1;
        }
    }
    /*public bool nearlyEqual(float a, float b)
    {
        float absA = Mathf.Abs(a);
        float absB = Mathf.Abs(b);
        float diff = Mathf.Abs(a - b);

        if (a == b)
        { // shortcut, handles infinities
            return true;
        }
        else if (a == 0 || b == 0 || absA + absB < float.MinValue)
        {
            // a or b is zero or both are extremely close to it
            // relative error is less meaningful here
            return diff < (Mathf.Epsilon * float.MinValue);
        }
        else
        { // use relative error
            return diff / (absA + absB) < Mathf.Epsilon;
        }
    }*/
    private void NormalMoveToTarget()
    {
        moveDirection = findDirection();
        if (Mathf.Abs(currentY - targetY) <= 0.001)
        {
            if (Mathf.Abs(currentX - targetX) >= 0.001)
            {
                animator.SetInteger("MovementSituation", 1);
                if (targetX > currentX)
                {
                    if(targetX > transform.position.x)
                    {
                        moveToRight();
                    }
                    else
                    {
                        transform.position = new Vector3(targetX, targetY, transform.position.z);
                        currentX = targetX;
                    }
                }
                else
                {
                    if (targetX < transform.position.x)
                    {
                        moveToLeft();
                    }
                    else
                    {
                        transform.position = new Vector3(targetX, targetY, transform.position.z);
                        currentX = targetX;
                    }
                }
            }
            else
            {
                animator.SetInteger("MovementSituation", 0);
            }
        }
        else
        {
            animator.SetInteger("MovementSituation", 1);
            if (moveDirection == 1)
            {
                if (Mathf.Abs(mostRightX - currentX) > 0.001)
                {
                    if(transform.position.x < mostRightX)
                    {
                        moveToRight();
                    }
                    else
                    {
                        transform.position = new Vector3(mostRightX, transform.position.y, transform.position.z);
                        currentX = mostRightX;
                    }
                }
                else
                {
                    if(Mathf.Abs(targetY - currentY) > 0.001)
                    {
                        if (transform.position.y < targetY)
                        {
                            moveToUp();
                        }
                        else
                        {
                            transform.position = new Vector3(currentX, targetY, transform.position.z);
                            currentY = targetY;
                        }
                    }
                }
            }
            else
            {
                if (Mathf.Abs(mostLeftX - currentX) > 0.001)
                {
                    if (transform.position.x > mostLeftX)
                    {
                        moveToLeft();
                    }
                    else
                    {
                        transform.position = new Vector3(mostLeftX, transform.position.y, transform.position.z);
                        currentX = mostLeftX;
                    }
                }
                else
                {
                    if (Mathf.Abs(targetY - currentY) > 0.001)
                    {
                        if (transform.position.y < targetY)
                        {
                            moveToUp();
                        }
                        else
                        {
                            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                            currentY = targetY;
                        }
                    }
                }
            }
        }
    }
    public void setTarget(float x, float y)
    {
        targetY = y;
        targetX = x;
    }
    public void setTargetByDieValue()
    {
        moveDirection = findDirection();
        int dieValue = CurrentDie.getDieValue();
        float target = (float)(currentX + moveDirection * dieValue * 0.7);
        if(target >= mostLeftX - 0.001 && target <= mostRightX + 0.001)
        {
            targetX = target;
        }
        else
        {
            if(target < mostLeftX - 0.001)
            {
                if(targetY < 11.6)
                {
                    targetY = (float)(currentY + 0.7);
                    targetX = (float)(mostLeftX + (mostLeftX - target - 0.7));
                }
                else
                {
                    targetY = (float)12.2;
                    targetX = mostLeftX;
                }
            }
            else if(target >= mostRightX + 0.001)
            {
                targetY = (float)(currentY + 0.7);
                targetX = (float)(mostRightX - (target - mostRightX - 0.7));
            }
        }
    }
    public int getID()
    {
        return ID;
    }
    private void controlMove()
    {
        if(Mathf.Abs(currentX - targetX) < 0.001 && Mathf.Abs(currentY - targetY) < 0.001)
        {
            animator.SetInteger("MovementSituation", 0);
            moveType = 1;
            for(int x = 0; x < coreGame.getSnakeCount(); x++)
            {
                if (Mathf.Abs(currentX - coreGame.snakeHeadX[x]) <= 0.001
                    && Mathf.Abs(currentY - coreGame.snakeHeadY[x]) <= 0.001)
                {
                    targetX = coreGame.snakeTailX[x];
                    targetY = coreGame.snakeTailY[x];
                    moveType = 2;
                }
            }
            for(int x = 0; x < coreGame.getLadderCount(); x++)
            {
                if(Mathf.Abs(currentX - coreGame.ladderHeadX[x]) <= 0.001 &&
                    Mathf.Abs(currentY - coreGame.ladderHeadY[x]) <= 0.001)
                {
                    targetX = coreGame.ladderTailX[x];
                    targetY = coreGame.ladderTailY[x];
                    moveType = 2;
                }
            }
            int number = 0;
            for(int x = 0; x < coreGame.getPlayerC(); x++)
            {
                if(Mathf.Abs(currentX - coreGame.getplayerByIndex(x).GetComponent<Movement>().getCurrentX()) < 0.001
                    && Mathf.Abs(currentY - coreGame.getplayerByIndex(x).GetComponent<Movement>().getCurrentY()) < 0.001
                    && ID != coreGame.getplayerByIndex(x).GetComponent<Movement>().getID())
                {
                    number++;
                }
            }
            if(number > 0)
            {
                for (int x = 0; x < coreGame.getPlayerC(); x++)
                {
                    if (Mathf.Abs(currentX - coreGame.getplayerByIndex(x).GetComponent<Movement>().getCurrentX()) < 0.001
                        && Mathf.Abs(currentY - coreGame.getplayerByIndex(x).GetComponent<Movement>().getCurrentY()) < 0.001
                        && ID != coreGame.getplayerByIndex(x).GetComponent<Movement>().getID())
                    {
                        if (Mathf.Abs(transform.position.x - coreGame.getplayerByIndex(x).GetComponent<Movement>().transform.position.x) < 0.001
                        && Mathf.Abs(transform.position.y - coreGame.getplayerByIndex(x).GetComponent<Movement>().transform.position.y) < 0.001)
                        {
                            coreGame.getplayerByIndex(x).GetComponent<Movement>().drag(x + 1);
                        }
                    }
                }
            }
        }
        else
        {
            if(moveType == 1)
            {
                NormalMoveToTarget();
            }
            else
            {
                directMotion();
            }
        }
    }
    private void directMotion()
    {
        float speedX = (targetX - currentX) / 5;
        float speedY = (targetY - currentY) / 5;
        if(speedX >= 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if(Mathf.Abs(currentX - targetX) > 0.001 || Mathf.Abs(currentY - targetY) > 0.001)
        {
            animator.SetInteger("MovementSituation", 1);
            if(speedX >= 0 && speedY >= 0)
            {
                if(/*transform.position.x - 0.0001 < targetX ||*/ transform.position.y < targetY)
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    transform.Translate(Vector2.up * Time.deltaTime * speedY);
                    transform.Translate(Vector2.right * Time.deltaTime * speedX);
                }
                else
                {
                    transform.position = new Vector3(targetX, targetY, transform.position.z);
                    currentX = targetX;
                    currentY = targetY;
                }
            }
            else if (speedX >= 0 && speedY < 0)
            {
                if (/*transform.position.x - 0.0001< targetX ||*/ transform.position.y > targetY)
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    transform.Translate(Vector2.up * Time.deltaTime * speedY);
                    transform.Translate(Vector2.right * Time.deltaTime * speedX);
                }
                else
                {
                    transform.position = new Vector3(targetX, targetY, transform.position.z);
                    currentX = targetX;
                    currentY = targetY;
                }
            }
            else if (speedX < 0 && speedY >= 0)
            {
                if (/*transform.position.x + 0.0001 > targetX ||*/ transform.position.y < targetY)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    transform.Translate(Vector2.up * Time.deltaTime * speedY);
                    transform.Translate(Vector2.right * Time.deltaTime * speedX);
                }
                else
                {
                    transform.position = new Vector3(targetX, targetY, transform.position.z);
                    currentX = targetX;
                    currentY = targetY;
                }
            }
            else if (speedX < 0 && speedY < 0)
            {
                if (/*transform.position.x + 0.0001 > targetX ||*/ transform.position.y > targetY)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    transform.Translate(Vector2.up * Time.deltaTime * speedY);
                    transform.Translate(Vector2.right * Time.deltaTime * speedX);
                }
                else
                {
                    transform.position = new Vector3(targetX, targetY, transform.position.z);
                    currentX = targetX;
                    currentY = targetY;
                }
            }
        }
        else
        {
            animator.SetInteger("MovementSituation", 0);
        }
    }
    public void drag(int a)
    {
        if(a == 1)
        {
            float newX = transform.position.x - (float)0.15;
            float newY = transform.position.y + (float)0.15;
            transform.position = new Vector3(newX, newY, transform.position.z);
            dragVal = 1;
        }
        else if (a == 2)
        {
            float newX = transform.position.x - (float)0.15;
            float newY = transform.position.y - (float)0.15;
            transform.position = new Vector3(newX, newY, transform.position.z);
            dragVal = 2;
        }
        else if (a == 3)
        {
            float newX = transform.position.x + (float)0.15;
            float newY = transform.position.y + (float)0.15;
            transform.position = new Vector3(newX, newY, transform.position.z);
            dragVal = 3;
        }
        else if (a == 4)
        {
            float newX = transform.position.x + (float)0.15;
            float newY = transform.position.y - (float)0.15;
            transform.position = new Vector3(newX, newY, transform.position.z);
            dragVal = 4;
        }
    }
    public void setDragVal(int a)
    {
        dragVal = a;
    }
    public int getDragVal()
    {
        return dragVal;
    }
}