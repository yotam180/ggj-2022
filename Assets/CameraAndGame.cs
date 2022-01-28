using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAndGame : MonoBehaviour
{


    List<List<Vector3>> create_map(float[] xrange, float[] yrange, float shiftx = .0f, float shifty = .0f, int direction=1)
    {
        float d = 0.2f;
        float s = Random.Range(.0f, 1.5f), bias = 0; // change scale and bias
        float x0 = xrange[0], y0 = yrange[0];
        List<Vector3> xlocations = new List<Vector3>();
        List<Vector3> ylocations = new List<Vector3>();
        while (x0 < xrange[1])
        {
            // z is always 0,
            var loc = 2*(new Vector3(Mathf.Sin(s * x0 + bias) + shiftx, .0f, direction*x0 + shifty)); 
            
            xlocations.Add(loc);

            x0 += d;
        }
        s = Random.Range(.0f, 1.5f);
        while (y0 < yrange[1])
        {
            var loc = 2 * (new Vector3(direction*y0 + shifty, .0f, Mathf.Sin(s * y0 + bias) + shiftx));

            ylocations.Add(loc);
            y0 += d;
        }
        List<List<Vector3>> ret = new List<List<Vector3>>();
        ret.Add(xlocations);
        ret.Add(ylocations);
        return ret;
    }
    bool smaller(Vector3 v1, Vector3 v2)
    {
        return Mathf.Abs(v1.x) <= Mathf.Abs(v2.x) && Mathf.Abs(v1.y) <= Mathf.Abs(v2.y) && Mathf.Abs(v1.z) <= Mathf.Abs(v2.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        float length = 20.0f;
        float[] xrange = { .0f, length * 1.5f}, yrange = { .0f, length * 1.5f };
        List<List<Vector3>> locs1 = create_map(xrange, yrange);
        List<List<Vector3>> locs2 = create_map(xrange, yrange, 15.0f, 15.0f, -1);
        int blocked = 0;
        for(int i = 0; i < locs1[0].Count; i++)
        {
            if(blocked % 2 == 0)
            {
                Instantiate(Resources.Load("WallCube"), locs1[0][i], Quaternion.identity);
                Instantiate(Resources.Load("WallCube"), locs2[1][i], Quaternion.identity);
                for(int j = i; j > 0; j--)
                {
                    if (smaller(locs1[0][i] - locs2[1][j], new Vector3(1, 1, 1))||
                        smaller(locs1[0][j] - locs2[1][i], new Vector3(1, 1, 1)))
                    {
                        Debug.Log(locs2[0][i]);
                        blocked += 1;
                        break;
                    }
                }
                
            }
           if (blocked < 2)
            {
                Instantiate(Resources.Load("WallCube"), locs1[1][i], Quaternion.identity);
                Instantiate(Resources.Load("WallCube"), locs2[0][i], Quaternion.identity);
                for (int j = i; j > 0; j--)
                {  
                        if (smaller(locs1[1][i] - locs2[0][j], new Vector3(1, 1, 1)) ||
                            smaller(locs1[1][j] - locs2[0][i], new Vector3(1, 1, 1)))
                        {
                            Debug.Log(locs1[1][i]);
                            blocked += 2;
                            break;
                        }
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
