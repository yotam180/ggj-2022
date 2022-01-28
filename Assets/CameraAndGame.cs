using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAndGame : MonoBehaviour
{


    List<List<Vector3>> create_map(float[] xrange, float[] yrange, float shift = .0f)
    {
        float d = 0.2f;
        float s = Random.Range(.0f, 1.5f), bias = Random.Range(0.0f, 2.0f); // change scale and bias
        float x0 = xrange[0], y0 = yrange[0];
        List<Vector3> xlocations = new List<Vector3>();
        List<Vector3> ylocations = new List<Vector3>();
        while (x0 < xrange[1])
        {
            // z is always 0,
            var loc = 2*(new Vector3(Mathf.Sin(s * x0 + bias) + shift, .0f, x0)); 
            
            xlocations.Add(loc);

            x0 += d;
        }
        s = Random.Range(.0f, 1.5f);
        while (y0 < yrange[1])
        {
            var loc = 2 * (new Vector3(y0, .0f, Mathf.Sin(s * y0 + bias) + shift));

            ylocations.Add(loc);
            y0 += d;
        }
        List<List<Vector3>> ret = new List<List<Vector3>>();
        ret.Add(xlocations);
        ret.Add(ylocations);
        return ret;
    }


    // Start is called before the first frame update
    void Start()
    {
        float[] xrange = { .0f, 20.0f }, yrange = { .0f, 20.0f };
        List<List<Vector3>> locs1 = create_map(xrange, yrange);
        List<List<Vector3>> locs2 = create_map(xrange, yrange, 15.0f);
        for(int i = 0; i < locs1[0].Count; i++)
        {
            Instantiate(Resources.Load("WallCube"), locs1[0][i], Quaternion.identity);
            Instantiate(Resources.Load("WallCube"), locs1[1][i], Quaternion.identity);
            Instantiate(Resources.Load("WallCube"), locs2[0][i], Quaternion.identity);
            Instantiate(Resources.Load("WallCube"), locs2[1][i], Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
