/// <summary>Class <c>PlayerController</c> controls how the environment 
/// interacts with the player</summary>
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    
    SpriteRenderer myRend;
    Animator myAnim;
    Text scoreDisplay, logicDisplay;

    bool boolean1, boolean2;
    bool bothCoinsGained = false;
    
    public Rigidbody2D myBod; 
    public LayerMask ladder;

    public int booleanTracker; 
    public float distance;
    
    public static bool climbing = false;
    public static int deathCounter = 0, //variables for stats...do not change name or modifiers without changing Stats script
        highScore = 0, 
        trueCoinsCollected = 0, 
        falseCoinsCollected = 0,
        totalCoinsCollected = 0,
        score;
    
    void Start()
    {
        myBod = GetComponent<Rigidbody2D>();
        myRend = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();

        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        logicDisplay = GameObject.Find("Logic").GetComponent<Text>();

        score = 0;
        booleanTracker = 1;

        boolean1 = false;
        boolean2 = false;
    }

    void Update()
    {      
        TransformPlayer();
    }

    /// <summary>This method will allow the player to jump upwards when 
    /// the user presses the spacebar.
    /// </summary>

    private void Jump() {

        /*
        Lines 71-73: Allows Boolario to jump upwards
        
        Line 71:     If the user presses spacebar and Boolario is touching the ground,
        Line 73:     then Boolario will jump upwards 5.0.

        If you change 15f to another value, such as 14f or 10.25f, then Boolario will jump upwards 
        to 14 or 10.25, respectively.
        */

        if (Input.GetButtonDown("Jump") && JumpCheck.isGrounded) {
            myBod.velocity = new Vector2(myBod.velocity.x, 5f); //  <-- YOU MAY CHANGE THIS LINE
        }
    }


    /// <summary>This method will allow the player to move left and right
    /// when the user presses the left or right arrow keys, 'A', or 'D'
    /// </summary>

    private void Walk() {

        /*
        Lines 93-106: Allows Boolario to move left or right
        
        Line 93:     When the user presses "A", "D", or the left or right arrow keys,
        Line 94:     Boolario will move left or right at a speed of 5.

        If you change 5 to another value in line 94, such as 10 or 4, then Boolario will move
        left or right at a speed of 10 or 4, respectively.
        */

        float h = Input.GetAxisRaw("Horizontal");
        myBod.velocity = new Vector2(h * 5, myBod.velocity.y);

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
    }


    /// <summary>This method will allow the player to climb up and down 
    /// ladders when they collide with <c>ladder.prefab</c>
    /// </summary>

    private void Climb() {

        /*
        Lines 140-159:  Allows Boolario to climb ladders
        
        Line 142-150:   If the user collides with a ladder and presses the up or down arrow keys or "W" or "S", 
                        then the boolean variable, climbing, will hold the value of TRUE.  This will allow the
                        program to let Boolario climb the ladder in lines 99-104.

                        Otherwise, if Boolario does not collide with the ladder, then the boolean variable,
                        climbing, will hold the value of FALSE.  The player will not be able to climb the ladder.
        
        Lines 152-159:   If the boolean variable, climbing, is equal to TRUE and the player collides with the ladder,
                        then the player can press "W", "S", or the up or down arrow keys to move Boolario upwards
                        at a speed of 5.

                        Otherwise, if the boolean variable, climbing, is equal to FALSE or the player is not touching
                        a ladder, then the player will not be able to move upwards.

        If you change 5 to another value in line 154, such as 10 or 4, then Boolario will move
        left or right at a speed of 10 or 4, respectively.

        If you change the gravityScale in lines 155 and 158 to another value, such as 1 or 3, then Boolario's gravity will adjust
        accordingly.
        */

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.up, distance, ladder);

        if(raycastHit2D.collider != null) {
            if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                climbing = true;
            }
        } 
        else {
            climbing = false;
        }

        if(climbing == true && raycastHit2D.collider != null) {
            float v = Input.GetAxisRaw("Vertical");
            myBod.velocity = new Vector2(myBod.velocity.x, v * 5);      //  <-- YOU MAY CHANGE THIS LINE
            myBod.gravityScale = 0;         //  <-- YOU MAY CHANGE THIS LINE
        } 
        else {
            myBod.gravityScale = 2;         //  <-- YOU MAY CHANGE THIS LINE
        }
    }


    /// <summary>This method will give the player movement controls by
    /// implementing the <c>Walk()</c>, <c>Jump()</c>, and <c>Climb()</c>
    /// methods.
    /// </summary>

    private void TransformPlayer() {
        
        /*
            Lines 174-176:    Gives Boolario movement
        */

        Walk();
        Jump();
        Climb();
    }


    /// <summary>This method will control which game mode will load
    /// and what will occur when the player forms a <paramref name="collision"/> 
    /// with the coins based on the player's selection in <c>MainMenu.unity</c>.
    /// </summary>
    /// <param name="collision">trigger collider when an object collides with it</param>


    /*
        DO NOT CHANGE CODE BELOW
    */
    private void ControlGameMode(Collider2D collision) {
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
                if (collision.transform.tag == "TrueCoin" || collision.transform.tag == "FalseCoin")
                {
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
                    logicDisplay.text = boolean1 + " AND " + boolean2;
                    booleanTracker = 3;
                    bothCoinsGained = true;
                }
            }
            if (boolean1 && boolean2 && booleanTracker == 3)
            {
                score++;            //  <-- YOU MAY CHANGE THIS LINE | if you change score++; to score += 5;
                                    //                                  then your score will increase by 5
                boolean1 = false;
                boolean2 = false;
                scoreDisplay.text = "Score: " + score;
            }
            if (booleanTracker == 3 && bothCoinsGained)
            {
                booleanTracker = 1;
                bothCoinsGained = false;
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
            }
            if (booleanTracker == 3 && bothCoinsGained)
            {
                booleanTracker = 1;
                bothCoinsGained = false;
            }
        }
        totalCoinsCollected = trueCoinsCollected + falseCoinsCollected;
    }
    

    /// <summary>This method will load <c>GameOver.unity</c> when the player
    /// forms a <paramref name="collision"/> with <c>Spike.prefab</c>
    /// </summary>
    /// <param name="collision">trigger collider when an object collides with it</param>  

    private void DeathUponImpact(Collider2D collision) {

        /*
        Lines 324-331:  The game resets when the player touches a dangerous object.

                        If the player collides with a spike, then the Game Over screen will appear, and the game will end.
                        Your number of deaths will increase by one, and your highscore will be calculated based 
                        on your score upon death.
        */

        if(collision.transform.tag == "Spike") {
            SceneManager.LoadScene("GameOver");
            deathCounter++;

            if(score > highScore) {
                highScore = score;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ControlGameMode(collision);
        DeathUponImpact(collision);
    }

}