using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public GameObject footstep;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal"))
        {
            footsteps();
        }

        if(Input.GetButtonDown("Horizontal"))
        {
            footsteps();
        }

        if(Input.GetButtonDown("Vertical"))
        {
            footsteps();
        }

        if(Input.GetButtonUp("Horizontal"))
        {
            StopFootsteps();
        }

        if(Input.GetButtonUp("Vertical"))
        {
            StopFootsteps();
        }

    }

    void footsteps()
    {
        footstep.SetActive(true);
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);
    }
}
