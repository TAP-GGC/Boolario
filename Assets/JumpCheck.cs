using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
    public bool isGrounded;
    Rigidbody2D myBod;
    // Start is called before the first frame update
    void Start()
    {
        myBod = GameObject.Find("Boolrio").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //transform.position += (new Vector3(h, 0, 0)) * Time.deltaTime * 5;
        myBod.velocity = new Vector3(h * 5, myBod.velocity.y, 0); //change x only

        
        if (Input.GetButtonDown("Jump") && isGrounded) {
            myBod.velocity = new Vector3(myBod.velocity.x, 10, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isGrounded = false;
    }
}
