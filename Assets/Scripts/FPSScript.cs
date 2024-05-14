using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPSScript : MonoBehaviour
{
    float timer, refresh; 
    public float avgFramrate = 145;
    public string display = "{0} FPS";
    public Text m_Text;

    private void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0f) avgFramrate = (int)(1f / timelapse);
        m_Text.text = string.Format(display, avgFramrate.ToString());
    }
}
