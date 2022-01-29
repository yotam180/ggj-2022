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

    public float maxStamina = 3;
    public float currentStamina;
    public StaminaBar staminaBar;

    public GameObject BarrierFX;
    GameObject currentBarrierFX;
    
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        currentMoveSpeed = desiredMoveSpeed = moveSpeed;
    }

    public Vector3 GetDirection()
    {
        return new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction));
    }

    void Update()
    {
        if (Input.GetKey(CWKey))
        {
            direction -= turnSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(CCWKey))
        {
            direction += turnSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(SprintKey))
        {
            desiredMoveSpeed = maxMoveSpeed;
        }

        desiredMoveSpeed = Input.GetKey(SprintKey) ? maxMoveSpeed : moveSpeed;
        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, desiredMoveSpeed, 0.95f * Time.deltaTime); // TODO: Fix this mechanic...

        GetComponent<Rigidbody>().velocity = GetDirection() * currentMoveSpeed;

        if (Input.GetKeyDown(WallKey) && currentStamina > 0)
        {
            timeSinceLastWall = 0;
            lastWallPosition = transform.position;

            currentBarrierFX = Instantiate(BarrierFX, transform.position, Quaternion.identity);
        }
        else if (Input.GetKey(WallKey) && currentStamina > 0)
        {
            ChangeStamina(-Time.deltaTime);

            timeSinceLastWall += Time.deltaTime;
            if (timeSinceLastWall > 0.1f)
            {
                timeSinceLastWall = 0;

                var alpha = -Mathf.Atan2(transform.position.z - lastWallPosition.z, transform.position.x - lastWallPosition.x);
                var rotation = new Vector3(0, alpha * 180 / Mathf.PI, 0);
                var barrier = Resources.Load<GameObject>("Barrier");
                var loc = (lastWallPosition + transform.position) / 2;
                var size = new Vector3((lastWallPosition - transform.position).magnitude, 1, 0.1f);
                var obj = Instantiate(barrier, loc, Quaternion.Euler(rotation));
                obj.transform.localScale = size;
                obj.GetComponent<Barrier>().player = gameObject.name.Contains("(1)"); //true for second player

                lastWallPosition = transform.position;
            }

            if (currentBarrierFX != null)
                currentBarrierFX.transform.position = transform.position;
        }
        else if (Input.GetKeyUp(WallKey) || currentStamina <= 0)
        {
            currentBarrierFX = null;
            ChangeStamina(Time.deltaTime);
        }
        else
        {
            ChangeStamina(Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Barrier>()?.lifetime < 0.1f)
        {
            return;
        }

        var reflectionVector = Vector3.Reflect(GetDirection(), other.contacts[0].normal);
        direction = Mathf.Atan2(reflectionVector.z, reflectionVector.x);
    }

    void ChangeStamina(float stamina)
    {
        currentStamina = Mathf.Min(Mathf.Max(0, currentStamina + stamina), staminaBar.MaxStamina);

        staminaBar.SetStamina(currentStamina);
    }
}

