using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{

    void Start()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            var rb = child.gameObject.AddComponent<Rigidbody>();
            rb.AddForce(100 * (child.position - transform.position), ForceMode.Force);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
