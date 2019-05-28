using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoFighterScript : MonoBehaviour
{
    // Declare Variables
    private CharacterController controller;
    public CharacterController enemyPlayer;
    private Vector3 movementVector;
    private float verticalVelocity;
    public Collider[] attackHitBoxes;
    private float attackTimer;
    int noOfButtonPresses; //Determines Which Animation Will Play
    bool canPressButton; //Locks ability to press button during animation event
    bool playerIsAttacking = false;
    bool takingDamage = false;
    bool playerIsBlocking = false;

    // Animation Variables
    private Animator Player2Anim;
    int DashHash = Animator.StringToHash("IsRunning");

    // Audio Variables
    public AudioClip hitSoundClip;

    public AudioSource hitSoundSource;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Player2Anim = GetComponent<Animator>();

        noOfButtonPresses = 0;
        canPressButton = true;

        hitSoundSource.clip = hitSoundClip;
    }


    private void Update()
    {

        // Locks the player on the z-Axis
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        // Inputs. The player cannot make an input whilst taking damage
        if (takingDamage == false)
        {


            // Attack Inputs
            if (!Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Punch")
                && Input.GetKeyDown(KeyCode.Z))
            {
                Player2Anim.SetInteger("Animation", 31);
                StartAttack(attackHitBoxes[0]);
                ComboStarter();
                Player2Anim.SetFloat("Speed", 0);
                playerIsAttacking = true;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Player2Anim.SetInteger("Animation", 30);
                StartAttack(attackHitBoxes[1]);
                ComboStarter();
                Player2Anim.SetFloat("Speed", 0);
                playerIsAttacking = true;
            }
            if (controller.isGrounded)
            {
                verticalVelocity = -1;
                // If the player has pressed W, let them jump.
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    verticalVelocity = 10;
                }
            }
            else
            {
                verticalVelocity -= 14 * Time.deltaTime;
            }
            // Blocking for if player is on the left
            if (transform.position.x < enemyPlayer.transform.position.x)
            {
                // Blocking mechanic
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    playerIsBlocking = true;
                }
                else
                {
                    playerIsBlocking = false;
                }
            }

            // Blocking for if player is blocking on the right
            if (transform.position.x > enemyPlayer.transform.position.x)
            {
                // Blocking mechanic
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    playerIsBlocking = true;
                }
                else
                {
                    playerIsBlocking = false;
                }
            }

            // Check if players x is higher than enemies and if it is, flip the sprite.
            if (transform.position.x > enemyPlayer.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }

        }

        //Initialise movementVector
        movementVector = Vector3.zero;

        //Movement for the game    
        // Lets player jump
        movementVector.y = verticalVelocity;

        // Check if the player is attempting to move to the left or right and if so, go that way. If not, stay still.
        movementVector.x = Input.GetAxis("Horizontal2") * 3;



        // Lets the animator know if the player is moving and uses the appropriate animation
        Player2Anim.SetFloat("Speed", movementVector.x);

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Dash whenever space button is pressed
            Debug.Log("Dash");
            Player2Anim.SetTrigger(DashHash);
            movementVector.x = 0;
            movementVector.x = Input.GetAxis("Horizontal2") * 50;
        }

        if (playerIsAttacking == false)
        {
            //Below line actually lets player move. DANNY DONT FORGET THIS LINE IN FUTURE!!!!!
            controller.Move(movementVector * Time.deltaTime);
        }

        



    }
    void ComboStarter()
    {
        // Checks if the player is allowed to pressed the button and if they are, increase number of presses.
        if (canPressButton)
        {
            noOfButtonPresses++;
        }

        if (noOfButtonPresses == 1)
        {
            StartAttack(attackHitBoxes[0]);
        }
    }

    void ComboCheck()
    {

        canPressButton = false;
        // Set animator to avoid errors
        Player2Anim = GetComponent<Animator>();


        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && noOfButtonPresses == 1)
        {//If the first animation is still playing and only 1 button has been pressed, return to idle
            Player2Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
            playerIsAttacking = false;
            enemyDamageCheck(attackHitBoxes[1]);
        }


        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && noOfButtonPresses >= 2)
        {//If the first animation is still playing and at least 2 buttons has been pressed, continue the combo
            Player2Anim.SetInteger("Animation", 33);
            canPressButton = true;

        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && noOfButtonPresses == 2)
        {//If the second animation is still playing and only 2 buttons have been pressed, return to idle 
            Player2Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
            playerIsAttacking = false;
            enemyDamageCheck(attackHitBoxes[1]);
        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && noOfButtonPresses >= 3)
        {//If the second animation is still playing and only 2 buttons have been pressed, continue the combo
            Player2Anim.SetInteger("Animation", 6);
            canPressButton = true;

        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 3"))
        {//Since this is the last animation, just go back to idle
            Player2Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
            playerIsAttacking = false;
            enemyDamageCheck(attackHitBoxes[1]);
        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick") && noOfButtonPresses == 1)
        {

            Player2Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
            playerIsAttacking = false;
            enemyDamageCheck(attackHitBoxes[1]);
        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick") && noOfButtonPresses >= 2)
        {//If the first animation is still playing and at least 2 buttons has been pressed, continue the combo
            Player2Anim.SetInteger("Animation", 20);
            canPressButton = true;
        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 2") && noOfButtonPresses == 2)
        {
            // If the second animation is playing and no extra buttons are pressed, just go back to idle
            Player2Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
            playerIsAttacking = false;
            enemyDamageCheck(attackHitBoxes[1]);
        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 2") && noOfButtonPresses >= 3)
        {//If the second animation is still playing and at least 3 buttons has been pressed, continue the combo
            Player2Anim.SetInteger("Animation", 25);
            canPressButton = true;
        }

        if (Player2Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 3"))
        {//Since this is the last animation, just go back to idle
            Player2Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
            playerIsAttacking = false;
            enemyDamageCheck(attackHitBoxes[1]);
        }

      
    }

    private void StartAttack(Collider collider)
    {

        // Detects if the hitbox overlaps the enemy players hitbox.
        Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation, LayerMask.GetMask("HitBox"));
        foreach (Collider c in colliders)
        {
            if (c.transform.root == transform)
                continue;

            Debug.Log(c.name);

            // Set up the damage for each hit
            float damage = 5;

            // Tells the enemy that they have taken damage
            c.SendMessageUpwards("RecieveDamage", damage);
        }

    }

    private void RecieveDamage(float damage)
    {
        if (playerIsBlocking == false)
        {
            takingDamage = true;
            Debug.Log("Took damage");
            hitSoundSource.Play();
        }
    }

    private void enemyDamageCheck(Collider collider)
    {

        // Detects if the hitbox overlaps the enemy players hitbox.
        Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation, LayerMask.GetMask("HitBox"));
        foreach (Collider c in colliders)
        {

            // Set up the bool to tell the enemey that player is not attacking
            bool playerIsNotAttacking = true;

            // Tell the enemy that they are not being attacked
            c.SendMessageUpwards("enemyPlayerNotAttacking", playerIsNotAttacking);

            Debug.Log("Enemy is not taking damage");

        }
    }

    public bool checkBlocking()
    {
        return playerIsBlocking;
    }

    private void enemyPlayerNotAttacking(bool playerIsNotAttacking)
    {
        takingDamage = false;
    }
}

