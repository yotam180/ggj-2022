using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portals : MonoBehaviour
{
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
        var obj = collision.gameObject;
        if(obj.name.ToLower().Contains("orb"))
        {
            Destroy(obj.GetComponent<Orb>());
            obj.transform.position = new Vector3(obj.transform.position.x, 2, obj.transform.position.z);
            Destroy(obj);
            GameObject.Find("Directional Light").GetComponent<CameraAndGame>().score += 1;
        }
        
    }
}
