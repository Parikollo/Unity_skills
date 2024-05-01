using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesScript : MonoBehaviour
{
    public float forcePower = 2.5f;
    Vector3 torqueVector;
    [SerializeField] Rigidbody rb;
    [SerializeField] bool readyToSpawn = false;
    public GameObject assetToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.TimeForAction += Action;
        rb = gameObject.GetComponent<Rigidbody>();
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2f)
        {
            GameManager.TimeForAction -= Action;
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.objectsCount -= 1;
            Destroy(gameObject);
        }
    }

    public void SetColor()
    {
        Vector3 generatedVector = RandomVector();
        Color color = new Vector4(1f, Mathf.Abs(generatedVector.x), Mathf.Abs(generatedVector.y), Mathf.Abs(generatedVector.z));
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    public Vector3 RandomVector ()
    {
        float a = Random.Range(-1f, 1f);
        float b = Random.Range(-1f, 1f);
        float c = Random.Range(-1f, 1f);
        Vector3 generatedVector = new Vector3(a, b, c);
        return generatedVector;
    }

    public virtual void Action()                                    // virtual keyword allows overriding also this is an example of abstraction
    {        
        rb.AddForce(Vector3.up * forcePower, ForceMode.Impulse);
        torqueVector = RandomVector();
        rb.AddTorque(torqueVector, ForceMode.Impulse);
        if (!readyToSpawn)
        {
            readyToSpawn = true;
        }
        else
        {
            Vector3 pos = gameObject.transform.position;
            Instantiate(assetToSpawn, pos, Quaternion.identity);
        }
    }

}
