using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        Debug.Log("A");

        var reflectionVector = Vector3.Reflect(GetDirection(), other.contacts[0].normal); // TODO: Is this correct?
        direction = VecToAngle(reflectionVector);
        speed = Mathf.Min(5, speed - 0.2f * Time.deltaTime);

        // speed = Mathf.Min(speed + 10f, 12f);
    }

    static float VecToAngle(Vector3 dir)
    {
        return Mathf.Atan2(dir.z, dir.x);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("B " + other.transform.name);
        direction = VecToAngle(other.transform.right);
        speed = Mathf.Min(10, speed + 1f);
        Debug.Log(direction);
    }
}
    