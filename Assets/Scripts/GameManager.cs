using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float actionTimer = 3f;                    //timers to control object spawning
    [SerializeField] float timeSpent = 0f;                      //timers to control object spawning
    [SerializeField] bool stopSpawning;                         //bool to stop spawn new objects
    [SerializeField] bool stopScoring;                         //bool to stop count points

    [SerializeField] private AudioSource managerAudio;
    [SerializeField] private AudioClip clip1, clip2, clip3, clip4;

    //variables for counting objects system
    public int objectsCount;
    string objCountString = "Obj Count: {0}/{1}";            //text template for text
    public Text m_objectsCountText;
    public GameObject go_clickText;
    //variables to count system's score
    string scoreString_1 = "Your system score is calculating! At this moment you have {0} parrots";            //text template for text
    string scoreString_2 = "Well done! Your system managed to get {0} parrots";            //text template for text
    public Text m_scoreText;
    int finalScore;                                 //final score of this system
    int testCount;                                  //number of benchmarkings

    //variables to stop object spawning via FPS and objectCount controls
    [SerializeField]  int minFPS = 30, maxObjCount = 9000;
    public FPSScript fpsScript;                                     //reference to a FPS Script

    //variables to parse InputFields with FPS and Obj count
    public Text fps_Text;           //text to explain min FPS
    string minFPSDisplay = "Limit to {0} FPS";
    public int fpsInput                                     //implementation of encapsulation
    {
        get { return minFPS; }
        set
        {
            minFPS = value;
            if(144 < minFPS || minFPS < 10 )
            {
                Debug.Log("Wrong FPS Setting");
                fpsInput = 30;
            }
        }
    }

    public int objCountInput                                //implementation of encapsulation
    {
        get { return maxObjCount; }
        set
        {
            maxObjCount = value;
            if (16384 < maxObjCount || maxObjCount < 1024)
            {
                Debug.Log("Wrong OBJ Setting");
                maxObjCount = 9000;
            }
        }
    }

    public GameObject[] objectsToSpawnAtFirst;
    
    public delegate void TimerCut(int digit);                            //event to iniciate object spawning
    public static event TimerCut TimeForAction;                         //event to iniciate object spawning
    int sendDigit = 1;                                                  //variable to send when I need to stop instantiating action on Cubes and Spheres

    // Start is called before the first frame update
    void Start()
    {
        objectsCount = SceneManager.GetActiveScene().rootCount - 6;
        m_objectsCountText.text = string.Format(objCountString, objectsCount.ToString(), maxObjCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (objectsCount > 0)
        {
            timeSpent += Time.deltaTime;

        }

        if (Input.GetMouseButtonDown(0) && !stopSpawning)
        {
            Debug.Log("Left mouse clicked");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                int spawnIndex = Random.Range(0, 2);
                Instantiate(objectsToSpawnAtFirst[spawnIndex], new Vector3(hit.point.x, 0.75f, hit.point.z), Quaternion.identity);
                CountObjects();

                go_clickText.SetActive(false);

                managerAudio.clip = clip1;
                managerAudio.Play();
            }
        }

        if (actionTimer < timeSpent)            //event to trigger division of objects
        {
            timeSpent = 0f;
            if (TimeForAction != null)
            {
                TimeForAction(sendDigit);
                Debug.Log("EVENT!!!");

                if (!stopSpawning) 
                {
                    managerAudio.clip = clip2;
                    managerAudio.Play();
                }

            }

            if (stopSpawning && !stopScoring)
            {
                testCount += 1;
                if (testCount > 5)
                {
                    sendDigit = 3;
                    m_scoreText.text = string.Format(scoreString_2, finalScore.ToString());
                    LeaderboardScript lbScript = GetComponent<LeaderboardScript>();
                    lbScript.enabled = true;
                    lbScript.userScore = finalScore;
                    stopScoring = true;

                    managerAudio.clip = clip3;
                    managerAudio.Play();
                }
                else
                {
                    int avgFPS = (int)fpsScript.avgFramrate;
                    GetScore(objectsCount, avgFPS);

                    managerAudio.clip = clip4;
                    managerAudio.Play();
                }
            }

        }
    }

    private void LateUpdate()
    {
        if(timeSpent == 0f)
        {
            CountObjects();
            float avgFPS = fpsScript.avgFramrate / 1.6f;
            if (objectsCount > 1 && !stopSpawning)
            {
                if (avgFPS < minFPS || objectsCount > maxObjCount)
                {
                    stopSpawning = true;
                    sendDigit = 2;
                    Debug.Log("Stop Spawning!!!");
                }
            }

        }
    }

    void CountObjects()
    {
        objectsCount = SceneManager.GetActiveScene().rootCount - 6;
        m_objectsCountText.text = string.Format(objCountString, objectsCount.ToString(), maxObjCount.ToString());
    }

    public void ReadFPSInput(string s)
    {
        int i;
        bool result;
        result = int.TryParse(s, out i);
        if(result)
        {
            fpsInput = int.Parse(s);
        }      

        fps_Text.text = string.Format(minFPSDisplay, minFPS.ToString());
    }

    public void ReadOBJInput(string s)
    {
        int i;
        bool result;
        result = int.TryParse(s, out i);
        if (result)
        {
            objCountInput = int.Parse(s);            
        }

        //fps_Text.text = string.Format(minFPSDisplay, minFPS.ToString());
    }

    void GetScore (int obj, int fps)
    {
        finalScore += obj * fps;
        m_scoreText.text = string.Format(scoreString_1, finalScore.ToString());
    }
}
