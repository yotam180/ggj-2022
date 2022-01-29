using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    float timeout = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout < 0)
            gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
