using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour
{
    string leaderboadrString = "on {0}, {1}, {2} Gb RAM";
    float memorySize;

    public GameObject leaderboard_1, leaderboard_2;
    [SerializeField] GameManager gameManager;

    string userNameInput;
    public Text userName, userParrots, userSystem;
    public int userScore;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        leaderboard_1.SetActive(true);
        memorySize = SystemInfo.systemMemorySize / 1024;
        //Debug.Log(string.Format(leaderboadrString, SystemInfo.processorType, SystemInfo.graphicsDeviceName, memorySize.ToString(), userNameInput));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadUserNameInput(string s)
    {
        if (s.Length > 0)
        {
            userNameInput = s;            
        }
        else
        {
            userNameInput = "AnOnYmUs";
        }

    }

    public void LeaderboardProceed_1()
    {
        Debug.Log(string.Format(leaderboadrString, SystemInfo.processorType, SystemInfo.graphicsDeviceName, memorySize.ToString()));
        if (userNameInput == null)
        {
            userNameInput = "AnOnYmUs";
        }
        userName.text = userNameInput;
        userParrots.text = userScore.ToString();
        userSystem.text = string.Format(leaderboadrString, SystemInfo.processorType, SystemInfo.graphicsDeviceName, memorySize.ToString());
        
        leaderboard_2.SetActive(true);
    }

    public void LeaderboardProceed_2()
    {

    }
}
