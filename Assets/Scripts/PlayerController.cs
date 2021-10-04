using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    public Rigidbody2D myBod;
    SpriteRenderer myRend;
    Animator myAnim;

    JumpCheck myJumpCheck;
    Text scoreDisplay, logicDisplay; //lastbooleanDisplay;
    int score = 0, booleanTracker; //booleanTracker keeps track of which side of the boolean the player is working on

    bool boolean1, boolean2;
    public static int deathCounter = 0, //variables for stats...do not change name or modifiers without changing Stats script
        highScore = 0, 
        trueCoinsCollected = 0, 
        falseCoinsCollected = 0, 
        andCoinsCollected = 0, 
        orCoinsCollected = 0,
        totalCoinsCollected = 0;

    SceneControl scene;
    Transform player;
    bool climbing = false;
    bool climbingUp;
    BoxCollider2D topLadder;
    void Start()
    {
        myBod = GetComponent<Rigidbody2D>();
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        logicDisplay = GameObject.Find("Logic").GetComponent<Text>();
        //lastbooleanDisplay = GameObject.Find("LastBooleanCollected").GetComponent<Text>();
        //scene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneControl>();
        myRend = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        myJumpCheck = GetComponentInChildren<JumpCheck>();
        player = GetComponent<Transform>();


        booleanTracker = 1;

        boolean1 = false;
        boolean2 = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //transform.position += (new Vector3(h, 0, 0)) * Time.deltaTime * 5;
        myBod.velocity = new Vector2(h * 5, myBod.velocity.y); //change x only

        
        if (Input.GetButtonDown("Jump") && myJumpCheck.isGrounded) {
            myBod.velocity = new Vector2(myBod.velocity.x, 15);
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
        if (climbing && climbingUp) {
            if(Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("CLIMBING!" + topLadder.transform.position);

                //player.transform.Translate(topLadder.transform.position * Time.deltaTime, Space.World);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        /*
        OPTION 1 FOR "AND" LOGIC: The player cannot collect boolean values for the right side until the left is true
        This is easier for the player...
        (ex) 
        The default value for the statement is: "false AND false"
        1st coin collected - "false"     logic statement - "false AND false"
        2nd coin collected - "false"     logic statement - "false AND false"
        3rd coin collected - "true"      logic statement - "true AND false"
        4th coin collected - "true"      logic statement - "true AND true"       --> the player gets a point
        */
        if (collision.transform.tag == "ClimbUp")
        {
            climbing = true;
            climbingUp = true;
            //Debug.Log("CLIMBING!");
            topLadder = collision.transform.parent.GetChild(1).GetComponent<BoxCollider2D>();
            //player.transform.position = topLadder.transform.position;

        }
        else if (collision.transform.tag == "ClimbDown"){
            climbing = true;
            climbingUp = false;
        }
        //AND Game Mode
        else if (SceneControl.andGameIndicator)
        {
            if (booleanTracker == 1)
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = true;
                    booleanTracker = 2;
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = false;
                }
                //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean1;
                logicDisplay.text = boolean1 + " AND ...";
            }
            else
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = true;
                    booleanTracker = 3; // 3 allows the points to increase
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = false;
                }
                //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean2;
                logicDisplay.text = boolean1 + " AND " + boolean2;
            }
            if (boolean1 && boolean2 && booleanTracker == 3)
            {
                score++;
                boolean1 = false;
                boolean2 = false;
                scoreDisplay.text = "Score: " + score;
                booleanTracker = 1;
                //lastbooleanDisplay.text = "";
                //logicDisplay.text = "No Logic Coin Collected";
            }
        }

        else
        {
            if (booleanTracker == 1)
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = true;
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = false;
                }
                booleanTracker = 2;
                //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean1;
                logicDisplay.text = boolean1 + " OR ...";
            }
            else
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = true;
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = false;
                }
                booleanTracker = 3;
                //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean2;
                logicDisplay.text = boolean1 + " OR " + boolean2;
            }
            if ((boolean1 || boolean2) && booleanTracker == 3)
            {
                score++;
                boolean1 = false;
                boolean2 = false;
                scoreDisplay.text = "Score: " + score;
                //lastbooleanDisplay.text = "";
                //logicDisplay.text = "No Logic Coin Collected";
                booleanTracker = 1;
            }
        }
        /*
        OPTION 2 FOR "AND" LOGIC: Each coin the player collects changes the value for the entire statement
        This will be harder for the players...
        (ex) 
        The default value for the statement is: "false AND false"
        1st coin collected - "false"     logic statement - "false AND false"
        2nd coin collected - "true"      logic statement - "false AND true"
        3rd coin collected - "true"      logic statement - "true AND true"    --> the player gets a point*/

        /*if(booleanTracker == 1) {
            if(collision.transform.tag == "TrueCoin"){
                Destroy(collision.gameObject);
                boolean1 = true;
            }
            if(collision.transform.tag == "FalseCoin") {
                Destroy(collision.gameObject);
                boolean1 = false;
            }
            booleanTracker = 2;
            lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean1;
            logicDisplay.text = boolean1 + " AND " + boolean2;
        }
        else {
            if(collision.transform.tag == "TrueCoin") {
                Destroy(collision.gameObject);
                boolean2 = true;
            }
            if(collision.transform.tag == "FalseCoin") {
                Destroy(collision.gameObject);
                boolean2 = false;
            }
            booleanTracker = 1;
            lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean2;
            logicDisplay.text = boolean1 + " AND " + boolean2;
        }
        if(boolean1 && boolean2) {
            score++;
            boolean1 = false;
            boolean2 = false;
            scoreDisplay.text = "Score: " + score;
            lastbooleanDisplay.text = "";
            logicDisplay.text = "No Logic Coin Collected";
        }
        */

        //restarts the game if the player collides with a spike        
        if(collision.transform.tag == "Spike") {
            SceneManager.LoadScene("GameOver");
            Time.timeScale = 0;
            deathCounter++;

            if(score > highScore) highScore = score;
        }
    }

}