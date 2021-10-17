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
    public Rigidbody2D myBod;
    SpriteRenderer myRend;
    Animator myAnim;
    Text scoreDisplay, logicDisplay; 
    public int booleanTracker; 

    bool boolean1, boolean2;
    public static int deathCounter = 0, //variables for stats...do not change name or modifiers without changing Stats script
        highScore = 0, 
        trueCoinsCollected = 0, 
        falseCoinsCollected = 0,
        totalCoinsCollected = 0,
        score;
    public static bool climbing = false;
    public float distance;
    public LayerMask ladder;
    bool bothCoinsGained = false;
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


/// <summary>This method will allow the player to jump upwards when 
/// the user presses the spacebar.
/// </summary>
    private void Jump() {
        if (Input.GetButtonDown("Jump") && JumpCheck.isGrounded) {
            myBod.velocity = new Vector2(myBod.velocity.x, 15f);
        }
    }


/// <summary>This method will allow the player to move left and right
/// when the user presses the left or right arrow keys, 'A', or 'D'
/// </summary>
    private void Walk() {
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
            myBod.velocity = new Vector2(myBod.velocity.x, v * 5);
            myBod.gravityScale = 0;
        } 
        else {
            myBod.gravityScale = 2;
        }
    }


/// <summary>This method will give the player movement controls by
/// implementing the <c>Walk()</c>, <c>Jump()</c>, and <c>Climb()</c>
/// methods.
/// </summary>

    private void TransformPlayer() {
        Walk();
        Jump();
        Climb();
    }


    /// <summary>This method will control which game mode will load
    /// and what will occur when the player forms a <paramref name="collision"/> 
    /// with the coins based on the player's selection in <c>MainMenu.unity</c>.
    /// </summary>
    /// <param name="collision">trigger collider when an object collides with it</param>

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
        if(collision.transform.tag == "Spike") {
            SceneManager.LoadScene("GameOver");
            deathCounter++;

            if(score > highScore) {
                highScore = score;
            }
        }
    }
    
    void Update()
    {      
        TransformPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ControlGameMode(collision);
        DeathUponImpact(collision);
    }

}