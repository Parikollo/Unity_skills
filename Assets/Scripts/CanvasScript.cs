using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour               //this script manages canvas layers from start to finish
{
    public bool hideFirstLayer, hideSecondLayer;

    public GameObject arena, firstLayer, secondLayer;

    // Start is called before the first frame update
    void Start()
    {
        arena.SetActive(false);
        firstLayer.SetActive(true);
        secondLayer.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!hideFirstLayer )
            {
                hideFirstLayer = true;
                firstLayer.SetActive(false);
            }
            else if (!hideSecondLayer)
            {
                /*hideSecondLayer = true;
                secondLayer.SetActive(false);
                arena.SetActive(true);*/
            }
            else
            {                
                Destroy(this);
            }
        }
    }

    public void ButtonClick()
    {
        hideSecondLayer = true;
        secondLayer.SetActive(false);
        arena.SetActive(true);
    }
}
