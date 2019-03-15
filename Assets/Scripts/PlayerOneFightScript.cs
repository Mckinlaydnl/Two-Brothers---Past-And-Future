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
    int PunchHash = Animator.StringToHash("Punch");
    int KickHash = Animator.StringToHash("Kick");
    public Collider[] attackHitBoxes;
    private float timeBetweenAttacks = 0.3f;
    private float attackTimer;
  
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Player1Anim = GetComponent<Animator>();

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
            Player1Anim.SetTrigger(PunchHash);
            attackTimer = Time.time + timeBetweenAttacks;

        }
        if (Time.time >= attackTimer && (Input.GetKeyDown(KeyCode.P)))
        {
            StartAttack(attackHitBoxes[1]);
            Player1Anim.SetTrigger(KickHash);
            attackTimer = Time.time + timeBetweenAttacks;
        }
        if(controller.isGrounded)
        {
            verticalVelocity = -1;
            // If the player has pressed space, let them jump.
            if (Input.GetKeyDown(KeyCode.Space))
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

        //Below line actually lets player move. DANNY DONT FORGET THIS LINE IN FUTURE!!!!!
        controller.Move(movementVector * Time.deltaTime);
        
    }

    private void StartAttack (Collider collider)
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
