using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveright : MonoBehaviour
{

    private Vector3 initialPosition;
    private float moveSpeed = 1.0f;
    private float timer = 0.0f;
    private float moveDuration = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {

        initialPosition = transform.position;
        
    }

    void Update()
    {

        timer += Time.deltaTime;

        if (timer > moveDuration){

            timer = 0.0f;
            MoveBack();
        }

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        
    }

    void MoveBack(){

        transform.position = initialPosition;
    }
}
