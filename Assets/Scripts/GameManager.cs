using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float actionTimer = 3f;                    //timers to control object spawning
    [SerializeField] float timeSpent = 0f;                      //timers to control object spawning
    
    //variables for counting objects system
    public int objectsCount;
    string objCountString = "Obj Count: {0}/{1}";            //text template for text
    public Text m_objectsCountText;

    //variables to stop object spawning via FPS and objectCount controls
    int minFPS = 30, maxObjCount = 9000;

    public GameObject[] objectsToSpawnAtFirst;
    
    public delegate void TimerCut();                            //event to iniciate object spawning
    public static event TimerCut TimeForAction;                 //event to iniciate object spawning
    // Start is called before the first frame update
    void Start()
    {
        objectsCount = SceneManager.GetActiveScene().rootCount - 6;
        m_objectsCountText.text = string.Format(objCountString, objectsCount.ToString(), maxObjCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse clicked");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                int spawnIndex = Random.Range(0, 2);
                Instantiate(objectsToSpawnAtFirst[spawnIndex], new Vector3(hit.point.x, 0.75f, hit.point.z), Quaternion.identity);
                CountObjects();
            }
        }

        if (actionTimer < timeSpent)            //event to trigger division of objects
        {
            timeSpent = 0f;
            if (TimeForAction != null)
                TimeForAction();
            Debug.Log("EVENT!!!");
        }
    }

    private void LateUpdate()
    {
        if(timeSpent == 0f)
        {
            CountObjects();
        }
    }

    void CountObjects()
    {
        objectsCount = SceneManager.GetActiveScene().rootCount - 6;
        m_objectsCountText.text = string.Format(objCountString, objectsCount.ToString(), maxObjCount.ToString());
    }
}
