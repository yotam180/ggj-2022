using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portals : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(explosion.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
       

        var obj = collision.gameObject;
        if(obj.name.ToLower().Contains("orb"))
        {
            // Create a tiny sparkle
            explosion.transform.localScale = new Vector3(10, 10, 10);
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            // Add score
            var player = obj.GetComponent<Orb>().player;
            if (player > 0)
                GameObject.Find("Directional Light").GetComponent<CameraAndGame>().score[player - 1] += 1;
            
            // Destroy orb clone
            obj.transform.position = new Vector3(obj.transform.position.x, 2, obj.transform.position.z);
            Destroy(obj.GetComponent<Orb>());
            Destroy(obj);


        }

    }
}
