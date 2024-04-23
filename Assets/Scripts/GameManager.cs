using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float actionTimer = 3f;
    [SerializeField] float timeSpent = 0f;
    //public int objectsCount;

    public GameObject[] objectsToSpawnAtFirst;
    
    public delegate void TimerCut();
    public static event TimerCut TimeForAction;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
