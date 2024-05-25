using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.UI;
using Dan.Main;

public class LeaderboardScript : MonoBehaviour
{
    string leaderboadrString = "on {0}, {1}, {2} Gb RAM";
    float memorySize;

    public GameObject leaderboard_1, leaderboard_2;
    [SerializeField] GameManager gameManager;

    string userNameInput;
    public Text userName, userParrots, userSystem;
    public int userScore;

    //Variables for leaderboard
    [SerializeField] private List<Text> names;
    [SerializeField] private List<Text> scores;
    [SerializeField] private List<Text> extras;

    public string publicLeaderboardKey = "cd57b93e7d74d474f4ee0346dd0c567d32844b301b5b8c2a41c27a9bc7258846";
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        leaderboard_1.SetActive(true);
        memorySize = SystemInfo.systemMemorySize / 1024;
        //Debug.Log(string.Format(leaderboadrString, SystemInfo.processorType, SystemInfo.graphicsDeviceName, memorySize.ToString(), userNameInput));
        //LeaderboardCreator.LoggingEnabled = false;
        //LeaderboardCreator.ResetPlayer();                 //use this line if you want to create unique record for each test
        GetLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => 
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
                extras[i].text = msg[i].Extra;
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score, string extra)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, extra, ((_msg) =>
        {
            GetLeaderboard();
        }));
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

        SetLeaderboardEntry(userNameInput, userScore, userSystem.text);
    }

}
