using System.Collections.Generic;
using UnityEngine;

public class OrbObjectPooler : MonoBehaviour
{
    public static OrbObjectPooler SharedInstance;

    [SerializeField] private Orb _objectToPool;
    [SerializeField] private int amountToPool;

    private List<Orb> pooledObjects;
    private Color[] colors = { Color.red, Color.white, Color.yellow };

    void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<Orb>();
        for (int i = 0; i < amountToPool; i++)
        {
            Orb orb = (Orb)Instantiate(_objectToPool);
            orb.gameObject.SetActive(false);
            pooledObjects.Add(orb);
        }
    }

    public Orb GetPooledObject()
    {
        //1
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        //3   
        return null;
    }

    public void PullAndActiveOrb()
    {
        Orb orb = OrbObjectPooler.SharedInstance.GetPooledObject();
        float dx = 2 * Random.Range(-10.0f, 10.0f);
        float dy = 2 * Random.Range(-10.0f, 10.0f);
        orb.GetComponentInChildren<ParticleSystem>().startColor = colors[(int)(Random.Range(0.0f, 2.99f))];
        if (orb != null)
        {
            orb.transform.position = new Vector3(dx, 0, dy);
            orb.gameObject.SetActive(true);
        }
    }
}
