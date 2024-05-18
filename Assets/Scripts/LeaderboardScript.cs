using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class LeaderboardScript : MonoBehaviour
{
    string leaderboadrString = "UserName has XXXXXXXXX parrots, on {0}, {1}, {2} Gb RAM";
    float memorySize;
    // Start is called before the first frame update
    void Start()
    {
        memorySize = SystemInfo.systemMemorySize / 1024;
        Debug.Log(string.Format(leaderboadrString, SystemInfo.processorType, SystemInfo.graphicsDeviceName, memorySize.ToString()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
