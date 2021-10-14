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
    Text scoreDisplay, logicDisplay; //lastbooleanDisplay;
    public int booleanTracker; //booleanTracker keeps track of which side of the boolean the player is working on

    bool boolean1, boolean2;
    public static int deathCounter = 0, //variables for stats...do not change name or modifiers without changing Stats script
        highScore = 0, 
        trueCoinsCollected = 0, 
        falseCoinsCollected = 0,
        totalCoinsCollected = 0,
        score;

    //SceneControl scene;
    //bool climbing = false;
    //bool climbingUp;
    public static bool climbing = false;
    //BoxCollider2D topLadder;
    public float distance;
    public LayerMask ladder;
    bool bothCoinsGained = false;
    void Start()
    {
        myBod = GetComponent<Rigidbody2D>();
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        logicDisplay = GameObject.Find("Logic").GetComponent<Text>();
        //lastbooleanDisplay = GameObject.Find("LastBooleanCollected").GetComponent<Text>();
        //scene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneControl>();
        myRend = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();


        booleanTracker = 1;

        boolean1 = false;
        boolean2 = false;

        score = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetButtonDown("Jump") && JumpCheck.isGrounded) {
            myBod.velocity = new Vector2(myBod.velocity.x, 15f);
        }
        
        float h = Input.GetAxisRaw("Horizontal");
        //transform.position += (new Vector3(h, 0, 0)) * Time.deltaTime * 5;
        myBod.velocity = new Vector2(h * 5, myBod.velocity.y); //change x only

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.up, distance, ladder);

        if(raycastHit2D.collider != null) {
            if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                climbing = true;
            }
        } else {
            climbing = false;
        }

        if(climbing == true && raycastHit2D.collider != null) {
            float v = Input.GetAxisRaw("Vertical");
            myBod.velocity = new Vector2(myBod.velocity.x, v * 5);
            myBod.gravityScale = 0;
        } else {
            myBod.gravityScale = 2;
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
        /*if (climbing && climbingUp) {
            if(Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("CLIMBING!" + topLadder.transform.position);

                //player.transform.Translate(topLadder.transform.position * Time.deltaTime, Space.World);
            }
        }*/

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
        //AND Game Mode
        if (SceneControl.andGameIndicator)
        {
            if (booleanTracker == 1)
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = true;
                    trueCoinsCollected++;
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = false;
                    falseCoinsCollected++;
                }

                //if player collects any coin, advance the boolean tracker and change text
                if (collision.transform.tag == "TrueCoin" || collision.transform.tag == "FalseCoin")
                {
                    //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean1;
                    logicDisplay.text = boolean1 + " AND ...";
                    booleanTracker = 2;
                }
            }
            else
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = true;
                    trueCoinsCollected++;
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = false;
                    falseCoinsCollected++;
                }
                if (collision.transform.tag == "TrueCoin" || collision.transform.tag == "FalseCoin")
                {
                    //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean2;
                    logicDisplay.text = boolean1 + " AND " + boolean2;
                    booleanTracker = 3; // 3 allows the points to increase
                    bothCoinsGained = true;
                }
            }
            if (boolean1 && boolean2 && booleanTracker == 3)
            {
                score++;
                boolean1 = false;
                boolean2 = false;
                scoreDisplay.text = "Score: " + score;
                //lastbooleanDisplay.text = "";
                //logicDisplay.text = "No Logic Coin Collected";
            }
            if (booleanTracker == 3 && bothCoinsGained) //reset boolean tracker after
            {
                booleanTracker = 1;
                bothCoinsGained = false;
            }
        }

        //OR Game Mode
        else
        {
            if (booleanTracker == 1)
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = true;
                    trueCoinsCollected++;
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean1 = false;
                    falseCoinsCollected++;
                }
                if (collision.transform.tag == "TrueCoin" || collision.transform.tag == "FalseCoin")
                {
                    booleanTracker = 2;
                    //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean1;
                    logicDisplay.text = boolean1 + " OR ...";
                }
            }
            else
            {
                if (collision.transform.tag == "TrueCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = true;
                    trueCoinsCollected++;
                }
                if (collision.transform.tag == "FalseCoin")
                {
                    Destroy(collision.gameObject);
                    boolean2 = false;
                    falseCoinsCollected++;
                }
                if (collision.transform.tag == "TrueCoin" || collision.transform.tag == "FalseCoin")
                {
                    booleanTracker = 3;
                    //lastbooleanDisplay.text = "Last Logic Coin Collected: " + boolean2;
                    logicDisplay.text = boolean1 + " OR " + boolean2;
                    bothCoinsGained = true;
                }
            }

            if ((boolean1 || boolean2) && booleanTracker == 3)
            {
                score++;
                boolean1 = false;
                boolean2 = false;
                scoreDisplay.text = "Score: " + score;
                //lastbooleanDisplay.text = "";
                //logicDisplay.text = "No Logic Coin Collected";
            }
            if (booleanTracker == 3 && bothCoinsGained) //reset boolean tracker after
            {
                booleanTracker = 1;
                bothCoinsGained = false;
            }
        }
        totalCoinsCollected = trueCoinsCollected + falseCoinsCollected;
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
            deathCounter++;

            if(score > highScore) {
                highScore = score;
            }
        }
    }

}