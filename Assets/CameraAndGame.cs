using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraAndGame : MonoBehaviour
{
    [SerializeField] OrbObjectPooler _orbObjectPooler;
    public int[] score = { 0, 0 };
    const float width = 2.0f;

    const int num_portals = 8;
    const float timeD = 5.0f;
    float time_interval = timeD;
    string[] crystals_map = { "02", "04", "07", "08" };

    public GameObject scoreText;

    public StressReceiver cameraShake;

    Color[] colors = { Color.red, Color.white, Color.yellow };

    public int Player1Score => score[0];
    public int Player2Score => score[1];

    public void UpdateScores()
    {
        scoreText.GetComponent<TextMeshProUGUI>().SetText($"Score P1: {score[0]}\nScore P2: {score[1]}");
        cameraShake.InduceStress(0.6f);
    }

    List<List<Vector3>> create_map(float[] xrange, float[] yrange, float shiftx = .0f, float shifty = .0f, int direction=1)
    {
        float d = 2f;
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

    int Preprocess(List<Vector3> p1, List<Vector3> p2)
    {
        for (int i = 0; i < p1.Count; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (smaller(p1[i] - p2[j], new Vector3(width, 1, width)))
                {
                    return j + 2;
                }
            }
        }
        for (int i = 0; i < p1.Count; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (smaller(p1[j] - p2[i], new Vector3(width, 1, width)))
                {
                    return j + 2;
                }
            }
        }
        return p1.Count - 1;
    }

    void ConnectPoints(Vector3 first,Vector3 last)
    {
        var alpha = -Mathf.Atan2(first.z - last.z, first.x - last.x);
        var rotation = new Vector3(0, alpha * 180 / Mathf.PI, 0);
        var barrier = Resources.Load<GameObject>("WallCube");
        var loc = (last + first) / 2;
        var size = new Vector3((last - first).magnitude, 1, 0.1f);
        var obj = Instantiate(barrier, loc, Quaternion.Euler(rotation));
        obj.transform.localScale = size;

        last = transform.position;
    }
    void CreatePortals()
    {
        for(int i = 0; i < num_portals; i++)
        {
            float x = 2.5f * Random.Range(-10.0f, 10.0f);
            float y = 2.5f * Random.Range(-10.0f, 10.0f);
            GameObject obj = Instantiate(Resources.Load<GameObject>("Crystalsv" + crystals_map[i % 4]), new Vector3(x, 0, y), Quaternion.identity);
            obj.GetComponent<portals>().explosion = GameObject.Find("Explosion");
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        int[] s = { 0, 0 };
        score = s;
        float length = 45.0f;
        float[] xrange = { .0f, length * 1.5f}, yrange = { .0f, length * 1.5f };
        List<List<Vector3>> locs1 = create_map(xrange, yrange, -length/3, -length/3);
        List<List<Vector3>> locs2 = create_map(xrange, yrange, length/3, length/3, -1);

        int l1 = Preprocess(locs1[0], locs2[1]);
        int l2 = Preprocess(locs1[1], locs2[0]);

        for(int i = 1; i <= l1; i++)
        {
            ConnectPoints(locs1[0][i - 1], locs1[0][i]);
            ConnectPoints(locs2[1][i - 1], locs2[1][i]);
            
        }
        for (int i = 1; i <= l2; i++)
        {
            ConnectPoints(locs1[1][i - 1], locs1[1][i]);
            ConnectPoints(locs2[0][i - 1], locs2[0][i]);
        }
        CreatePortals();

    }

    // Update is called once per frame
    void Update()
    {
        if (this.time_interval > 0.0f)
            this.time_interval -= Time.deltaTime;

        else
        {
            // SoundsManager.PlaySound("orbHit");
            this.time_interval = timeD;
            _orbObjectPooler.PullAndActiveOrb();
        }
    }
}
