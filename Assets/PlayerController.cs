using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode CWKey, CCWKey, SprintKey, WallKey;
    public float turnSpeed;
    public float moveSpeed;
    public float maxMoveSpeed;

    public Vector3 lastWallPosition;
    public float timeSinceLastWall = 0;

    float desiredMoveSpeed;
    float currentMoveSpeed;
    float direction;

    void Start()
    {
        currentMoveSpeed = desiredMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (Input.GetKey(CWKey))
        {
            direction += turnSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(CCWKey))
        {
            direction -= turnSpeed * Time.deltaTime;
        }

        desiredMoveSpeed = Input.GetKey(SprintKey) ? maxMoveSpeed : moveSpeed;
        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, desiredMoveSpeed, 0.95f * Time.deltaTime); // TODO: Fix this mechanic...

        Vector3 moveDir = new Vector3(Mathf.Sin(direction), 0, Mathf.Cos(direction)) * Time.deltaTime * currentMoveSpeed;
        transform.position += moveDir;

        //if (Input.GetKeyDown(WallKey))
        //{
        //    timeSinceLastWall = 0;
        //    lastWallPosition = transform.position;
        //}
        /*else */if (true)
        {
            timeSinceLastWall += Time.deltaTime;
            if (timeSinceLastWall > 0.1f)
            {
                timeSinceLastWall = 0;

                var alpha = -Mathf.Atan2(transform.position.z - lastWallPosition.z, transform.position.x - lastWallPosition.x);
                var rotation = new Vector3(0, alpha * 180 / Mathf.PI, 0);
                var barrier = Resources.Load<GameObject>("Barrier");
                var loc = (lastWallPosition + transform.position) / 2;
                var size = new Vector3((lastWallPosition - transform.position).magnitude, 1, 0.6f);
                var obj = Instantiate(barrier, loc, Quaternion.Euler(rotation));
                obj.transform.localScale = size;

                lastWallPosition = transform.position;
            }
        }
    }
}

