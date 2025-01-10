using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected bool beginClick = false;
    [SerializeField] protected bool endClick = false;
    [SerializeField] protected Vector3 begin;
    [SerializeField] protected Vector3 end;
    [SerializeField] protected Vector3 checkVector;
    public bool isMoving;
    public CheckInFront checkInFront;
    [SerializeField] protected GameObject player;
    private Stack<GameObject> bricks = new Stack<GameObject>();
    private void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        RunInput();
        if (isMoving)
        {
            StartCoroutine(GoNext());
        }

    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!beginClick)
            {
                begin = Input.mousePosition;
                beginClick = true;
                endClick = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            {
                if (beginClick)
                {
                    end = Input.mousePosition;
                    endClick = true;
                }
            }

        }
    }
    public void RunInput()
    {

        if (endClick)
        {
            Debug.Log("run");
            beginClick = false;
            endClick = false;
            StopAllCoroutines();
            checkVector = new Vector3(end.x - begin.x, end.y - begin.y).normalized;
            if (checkVector.y > 0.5 && (checkVector.x < 0.5 || checkVector.x > -0.5))
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                isMoving = true;
            }
            else if (checkVector.y < -0.5 && (checkVector.x < 0.5 || checkVector.x > -0.5))
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                isMoving = true;
            }
            else if (checkVector.x > 0.5 && (checkVector.y < 0.5 || checkVector.y > -0.5))
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                isMoving = true;
            }
            else if (checkVector.x < -0.5 && (checkVector.y < 0.5 || checkVector.y > -0.5))
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                isMoving = true;
            }


        }
        else
        {
            return;
        }


    }
    public void Rundir(string dir)
    {



        switch (dir)
        {
            case "RunUp":
                {
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    GoNext();
                    return;
                }
            case "RunDown":
                {
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    GoNext();
                    return;
                }
            case "RunRight":
                {
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    GoNext();
                    return;
                }
            case "RunLeft":
                {
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                    GoNext();
                    return;
                }
            default:
                return;


        }

    }

    private IEnumerator GoNext()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 nextPos = transform.position;
        //Debug.Log("in Go next");
        if (!checkInFront.curGameObjt.CompareTag("Wall") && !checkInFront.curGameObjt.CompareTag("End") && nextPos == transform.position)
        {
            nextPos = new Vector3(checkInFront.curGameObjt.transform.position.x, transform.position.y, checkInFront.curGameObjt.transform.position.z);
            this.gameObject.transform.position = nextPos;
            if(checkInFront.lateObject != null && checkInFront.lateObject.CompareTag("Brick"))
            GetBrick(checkInFront.lateObject);


        }
        else if(checkInFront.curGameObjt.CompareTag("AutoTurn"))
        {
           
        }
        else if (checkInFront.curGameObjt.CompareTag("End"))
        {
            nextPos = new Vector3(checkInFront.curGameObjt.transform.position.x, transform.position.y, checkInFront.curGameObjt.transform.position.z);
            this.gameObject.transform.position = nextPos;
            yield return new WaitForSeconds(0.1f);
            isMoving = false;
            GameManager.Instance.ReachFinish();    
        }
        else
        {
            isMoving = false;
        }

        if(checkInFront.lateObject != null && checkInFront.lateObject.CompareTag("LoseBrick"))
        {
            if(bricks.Count == 0)
            {
                isMoving = false;
                yield return null;
            }
            LostBrick(checkInFront.lateObject);
        }


    }
    public void GetBrick(GameObject tile)
    {
        if (tile.CompareTag("Brick"))
        {
            checkInFront.lateObject.transform.SetParent(player.transform);
            Vector3 pos = player.transform.position;
            pos.y -= 0.1f;
            pos.y -= player.transform.position.y;
            tile.transform.position = pos;
            Vector3 playerRePos = player.transform.localPosition;
            playerRePos.y += 0.2f;
            player.transform.localPosition = playerRePos;

            tile.GetComponent<BoxCollider>().isTrigger = false;
            tile.GetComponent<Rigidbody>().isKinematic = true;
            tile.tag = "Untagged";
            bricks.Push(tile);
        }
    }
    public void LostBrick(GameObject tile)
    {
        // neu la lostbrick thi mat set up ngc lai giong tile thuong ggwp
        if (tile.CompareTag("LoseBrick"))
        {
            GameObject brick = bricks.Pop();
            brick.transform.SetParent(tile.transform);
            Vector3 pos = tile.transform.position;
            pos.y += 0.1f;
            brick.transform.position = pos;
            Vector3 playerRePos = player.transform.localPosition;
            playerRePos.y -= 0.2f;
            player.transform.localPosition = playerRePos;
            tile.tag = "Untagged";
            checkInFront.lateObject = null;

        }
    }
}
