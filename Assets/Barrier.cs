using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    float disposeTime = 2;

    float lifetime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > disposeTime)
        {
            Destroy(gameObject);
        }
    }
}
