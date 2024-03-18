using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{

    public Vector3 returnPosition = new Vector3(8.1f, 0.34f, -9.05f);

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hit")){

        transform.position = returnPosition;

        }


        }

    }
