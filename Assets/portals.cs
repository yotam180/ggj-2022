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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        SoundsManager.PlaySound("orbHit");
        var obj = collision.gameObject;
        string name = obj.name;
        if(name.ToLower().Contains("orb"))
        {

            // Create a tiny sparkle
            explosion.transform.localScale = new Vector3(10, 10, 10);
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            // Add score
            var player = obj.GetComponent<Orb>().player;
            var orb_col = obj.GetComponentInChildren<ParticleSystem>().startColor;
            var portal_col = name.Contains("02") || name.Contains("04") ? Color.red :
                name.Contains("07") ? Color.yellow : Color.white;
            int factor = orb_col == portal_col ? 4 : 1;
            if (player > 0)
            {
                GameObject.Find("Directional Light").GetComponent<CameraAndGame>().score[player - 1] += factor;
            }
            // Destroy orb clone
            obj.transform.position = new Vector3(obj.transform.position.x, 2, obj.transform.position.z);
            Destroy(obj.GetComponent<Orb>());
            Destroy(obj);
        }

    }
}
