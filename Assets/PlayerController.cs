using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody myBod;

    // Start is called before the first frame update
    void Start()
    {
        myBod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //transform.position += (new Vector3(h, 0, 0)) * Time.deltaTime * 5;
        myBod.velocity = new Vector3(h * 5, myBod.velocity.y,myBod.velocity.z); //change x only
    }
}
