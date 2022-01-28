using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SharedCameraController : MonoBehaviour
{
    public Transform[] targets;

    public float OffsetX, OffsetY = 0;

    // Update is called once per frame
    void Update()
    {
        var maxX = targets.Select(t => t.position.x).Max();
        var minX = targets.Select(t => t.position.x).Min();
        var maxY = targets.Select(t => t.position.z).Max();
        var minY = targets.Select(t => t.position.z).Min();

        var heightX = 10 + 0.6f * (maxX - minX) / (2 * Mathf.Tan(Mathf.Deg2Rad * 30));
        var heightY = 10 + 0.6f * (maxY - minY) / (2 * Mathf.Tan(Mathf.Deg2Rad * 30));
        var height = Mathf.Max(heightX, heightY);

        var avgX = (minX + maxX) / 2 - OffsetX * height;
        var avgY = (minY + maxY) / 2 - OffsetY * height;

        transform.position = new Vector3(avgX, height, avgY);
    }
}
