using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesScript : MonoBehaviour
{

    [SerializeField] float pushTime = 3f, timer = 0f;
    public float forcePower = 2.5f;
    Vector3 torqueVector;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (pushTime < timer)
        {
            timer = 0f;
            rb.AddForce(Vector3.up * forcePower, ForceMode.Impulse);
            torqueVector = RandomVector();
            rb.AddTorque(torqueVector, ForceMode.Impulse);
        }
    }

    void SetColor()
    {
        Vector3 generatedVector = RandomVector();
        Color color = new Vector4(1f, generatedVector.x, generatedVector.y, generatedVector.z);
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    Vector3 RandomVector ()
    {
        float a = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float c = Random.Range(0f, 1f);
        Vector3 generatedVector = new Vector3(a, b, c);
        return generatedVector;
    }

}
