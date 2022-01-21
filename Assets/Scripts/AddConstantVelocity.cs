using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddConstantVelocity : MonoBehaviour
{
    [SerializeField]
    public Vector3 v3Force;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody>().velocity += new Vector3(0.1f, 0, 0);
        //Debug.Log("Update time :" + Time.deltaTime);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += v3Force;
        //Debug.Log("FixedUpdate time :" + Time.deltaTime);
    }
}
