using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float speed;
    public float direction;

    public int player = 0; //0 : Neutral, 1: Player, 2: Second Player (Player (1))

    void Start()
    {
        
    }

    Vector3 GetDirection()
    {
        return new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction));
    }

    void FixedUpdate()
    { 
        GetComponent<Rigidbody>().velocity = GetDirection() * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        var reflectionVector = Vector3.Reflect(GetDirection(), other.contacts[0].normal); // TODO: Is this correct?
        direction = Mathf.Atan2(reflectionVector.z, reflectionVector.x); // TODO: Is this correct?

        speed = Mathf.Min(speed + 10f, 12f);
    }
}
    