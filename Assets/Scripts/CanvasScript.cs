using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour               //this script manages canvas layers from start to finish
{
    public bool hideFirstLayer, hideSecondLayer;

    public GameObject arena, firstLayer, secondLayer;

    [SerializeField] private AudioSource canvasAudio;
    [SerializeField] private AudioClip clip1, clip2;

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

                canvasAudio.clip = clip1;
                canvasAudio.Play();
            }
            else if (!hideSecondLayer)
            {
                /*hideSecondLayer = true;
                secondLayer.SetActive(false);
                arena.SetActive(true);*/
            }
            else
            {
                //Destroy(this);
                Debug.Log("LeftClick");
            }
        }
    }

    public void ProceedClick()
    {
        hideSecondLayer = true;
        secondLayer.SetActive(false);
        arena.SetActive(true);

        canvasAudio.clip = clip2;
        canvasAudio.Play();
    }

    public void RepeatButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
}
