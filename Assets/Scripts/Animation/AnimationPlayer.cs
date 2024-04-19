using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public GameObject Player;
    private Animator playerAnimator;
    private string currentAnimation; 

    void Start()
    {
        playerAnimator = Player.GetComponent<Animator>();
        currentAnimation = "Idle"; // Set the initial animation state to Idle
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal") ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerAnimator.SetTrigger("Trun");
        }
        
        if (Input.GetButton("Fire1") && Input.GetButton("Vertical"))
        {
            currentAnimation = "GShoot"; // Update current animation to shooting animation
            playerAnimator.SetTrigger("Tfire");
        }
        
        if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Fire1"))
        {
            currentAnimation = "Idle"; // Update current animation to idle animation
            playerAnimator.Play("Idle");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            currentAnimation = "GShoot"; // Update current animation to shooting animation
            playerAnimator.SetTrigger("TfireIdle");
            playerAnimator.Play("GShoot");
        }
    }
}
