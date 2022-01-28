using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    float disposeTime = 5f;

    public float lifetime = 0;

    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        if (lifetime > 0.1f)
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        lifetime += Time.deltaTime;
        if (lifetime > disposeTime)
        {
            Destroy(gameObject);
        }
    }
}
