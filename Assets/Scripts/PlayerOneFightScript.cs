using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneFightScript : MonoBehaviour
{
    // Declare Variables
    private CharacterController controller;
    private Vector3 movementVector;
    private float verticalVelocity;
    private Animator Player1Anim;
    int DashHash = Animator.StringToHash("IsRunning");
    public Collider[] attackHitBoxes;
    private float timeBetweenAttacks = 0.3f;
    private float attackTimer;
    int noOfButtonPresses; //Determines Which Animation Will Play
    bool canPressButton; //Locks ability to press button during animation event
    float idleTimer = 0.5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Player1Anim = GetComponent<Animator>();

        noOfButtonPresses = 0;
        canPressButton = true;
    }


    private void Update()
    {
        // Locks the player on the z-Axis
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        // Attack Inputs
        if (Time.time >= attackTimer && (Input.GetKeyDown(KeyCode.O)))
        {
            StartAttack(attackHitBoxes[0]);
            Player1Anim.SetInteger("Animation", 31);
            ComboStarter();
        }
        if (Time.time >= attackTimer && (Input.GetKeyDown(KeyCode.P)))
        {
            StartAttack(attackHitBoxes[1]);
            Player1Anim.SetInteger("Animation", 30);
            ComboStarter();
        }
        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            // If the player has pressed W, let them jump.
            if (Input.GetKeyDown(KeyCode.W))
            {

                verticalVelocity = 10;
            }
        }
        else
        {
            verticalVelocity -= 14 * Time.deltaTime;
        }

        

        //Initialise movementVector
        movementVector = Vector3.zero;

        //Movement for the game    

        // Check if the player is attempting to move to the left or right and if so, go that way. If not, stay still.
        movementVector.x = Input.GetAxis("Horizontal") * 3;

        // Lets the animator know if the player is moving and uses the appropriate animation
        Player1Anim.SetFloat("Speed", movementVector.x);

        // This is the players vertical movement. This makes it so the player is effected by gravity and can jump.
        movementVector.y = verticalVelocity;

        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Dash whenever space button is pressed
            Debug.Log("Dash");
            Player1Anim.SetTrigger(DashHash);
            movementVector.x = 0;
            movementVector.x = Input.GetAxis("Horizontal") * 50;
        }


        //Below line actually lets player move. DANNY DONT FORGET THIS LINE IN FUTURE!!!!!
        controller.Move(movementVector * Time.deltaTime);

        // If no animation is playing, play idle animation
        idleTimer = -Time.deltaTime;

        if (idleTimer == 0)
        {
            if (!Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") & !Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") & Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")
                & !Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick") & !Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 2") & !Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 3"))
            {
                noOfButtonPresses = 0;
                Player1Anim.Play("Idle");
            }
        }
    

}

    private void StartAttack(Collider collider)
    {
        if (noOfButtonPresses != 3)
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
    }

    void ComboStarter()
    {
        if (canPressButton)
        {
            noOfButtonPresses++;
        }

        if (noOfButtonPresses == 1)
        {
            //Player1Anim.SetInteger("Animation", 31);
        }
    }

    void ComboCheck()
    {

        canPressButton = false;


        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && noOfButtonPresses == 1)
        {//If the first animation is still playing and only 1 button has been pressed, return to idle
            Player1Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && noOfButtonPresses >= 2)
        {//If the first animation is still playing and at least 2 buttons has been pressed, continue the combo
            Player1Anim.SetInteger("Animation", 33);
            canPressButton = true;
        
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && noOfButtonPresses == 2)
        {//If the second animation is still playing and only 2 buttons have been pressed, return to idle 
            Player1Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && noOfButtonPresses >= 3)
        {//If the second animation is still playing and only 2 buttons have been pressed, continue the combo
            Player1Anim.SetInteger("Animation", 6);
            canPressButton = true;
            
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 3"))
        {//Since this is the last animation, just go back to idle
            Player1Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick") && noOfButtonPresses == 1)
        {

            Player1Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick") && noOfButtonPresses >= 2)
        {//If the first animation is still playing and at least 2 buttons has been pressed, continue the combo
            Player1Anim.SetInteger("Animation", 20);
            canPressButton = true;
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 2") && noOfButtonPresses == 2)
        {
            // If the second animation is playing and no extra buttons are pressed, just go back to idle
            Player1Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 2") && noOfButtonPresses >= 3)
        {//If the second animation is still playing and at least 3 buttons has been pressed, continue the combo
            Player1Anim.SetInteger("Animation", 25);
            canPressButton = true;
        }

        if (Player1Anim.GetCurrentAnimatorStateInfo(0).IsName("Kick 3"))
        {//Since this is the last animation, just go back to idle
            Player1Anim.SetInteger("Animation", 4);
            canPressButton = true;
            noOfButtonPresses = 0;
        }

        
       

    }

    
       
}


