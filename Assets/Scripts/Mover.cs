using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    void Update()
    {
        // Get input for movement along the world axes
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Calculate movement vector based on world axes
        Vector3 movement = new Vector3(xValue, 0f, zValue);

        // Apply movement to the player's position
        transform.position += movement;
    }
}

