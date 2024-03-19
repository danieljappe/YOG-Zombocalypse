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
        currentAnimation = "Idle"; // anim ini
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            playerAnimator.SetTrigger("Trun");
        }
        
        if (Input.GetButton("Fire1") && Input.GetButton("Vertical"))
        {
            playerAnimator.SetTrigger("Tfire");
        }
        
        if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Fire1"))
        {
            playerAnimator.Play("Idle");
        }
        if (Input.GetButtonDown("Fire1")){

            playerAnimator.SetTrigger("TfireIdle");
            playerAnimator.Play("GShoot");

        }
    }
}
