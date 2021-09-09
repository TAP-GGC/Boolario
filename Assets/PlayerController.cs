using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    Rigidbody2D myBod;
    SpriteRenderer myRend;
    Animator myAnim;

    Transform camTran;

    JumpCheck myJumpCheck;


    void Start()
    {
        myBod = GetComponent<Rigidbody2D>();
        myRend = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();

        camTran = GameObject.Find("Main Camera").transform;

        myJumpCheck = GetComponentInChildren<JumpCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //transform.position += (new Vector3(h, 0, 0)) * Time.deltaTime * 5;
        myBod.velocity = new Vector2(h * 5, myBod.velocity.y); //change x only

        
        if (Input.GetButtonDown("Jump") && myJumpCheck.isGrounded) {
            myBod.velocity = new Vector2(myBod.velocity.x, 10);
        }
        
        //USE IF STATEMENTS and the SpriteRenderer component to flip X Mario when he moves left.
        if(h < 0) {
            myRend.flipX = true;
            myAnim.SetBool("RUN", true);
        }

        else if(h > 0) {
            myRend.flipX = false;
            myAnim.SetBool("RUN", true);
        }

        else {
            myAnim.SetBool("RUN", false);
        }

        //camTran.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "PinkPot") {
            if(GameObject.FindGameObjectsWithTag("Player").Length >= 50) {
                SceneManager.LoadScene(0);
            }
            GameObject clone = Instantiate(gameObject);
            clone.transform.position = new Vector3(0, 5, 0);
        }
    }


}