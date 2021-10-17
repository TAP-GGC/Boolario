using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
    public static bool isGrounded;
    Rigidbody2D myBod;
    void Start()
    {
        myBod = GameObject.Find("Boolrio").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
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
