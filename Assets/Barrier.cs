using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Barrier : MonoBehaviour
{
    float disposeTime = 10f;

    public float lifetime = 0;
    public bool player = false; //false: first player, true: second player

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

    public void OnTriggerEnter(Collider collider)
    {
        var obj = collider.gameObject;
        if (obj.name.Contains("Orb"))
        {
            obj.GetComponent<Orb>().player = player ? 2 : 1; //second player 2 first player 1
        }
    }
}
