using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierFX : MonoBehaviour
{
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTimer(float desiredTimer)
    {
        timer = desiredTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
