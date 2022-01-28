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
        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, desiredMoveSpeed, 0.7f * Time.deltaTime); // TODO: Fix this mechanic...

        Vector3 moveDir = new Vector3(Mathf.Sin(direction), 0, Mathf.Cos(direction)) * Time.deltaTime * currentMoveSpeed;
        transform.position += moveDir;

        if (Input.GetKeyDown(WallKey))
        {
            timeSinceLastWall = 0;
            lastWallPosition = transform.position;
        }
        else if (Input.GetKey(WallKey))
        {
            timeSinceLastWall += Time.deltaTime;
            if (timeSinceLastWall > 0.2f)
            {
                timeSinceLastWall = 0;
                Debug.Log("Lastposition: " + lastWallPosition + " to " + transform.position);
                var obj = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
                var comp = obj.AddComponent<LineRenderer>();
                comp.SetPositions(new[] { lastWallPosition, transform.position });
                lastWallPosition = transform.position;
            }
        }
    }
}

