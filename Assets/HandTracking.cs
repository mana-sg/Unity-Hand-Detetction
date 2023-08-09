using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject[] handPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
        string[] points = data.Split(',');
        try
        {
            float x;
            float y;
            float z;
            for(int i = 0; i < 21; i++)
            {
                bool a = float.TryParse(points[i*3], out x);
                bool b = float.TryParse(points[i*3 + 1], out y);
                bool c = float.TryParse(points[i*3 + 2], out z);

                x /= 100;
                y /= 100;
                z /= 100;
                handPoints[i].transform.localPosition = new Vector3(x, y, z);
            }
        }
        catch (System.Exception)
        {
            print("No Hands!");
        }
        
    }
}