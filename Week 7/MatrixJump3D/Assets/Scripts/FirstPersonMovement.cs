using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 localDirection = transform.TransformDirection(direction);
        rb.MovePosition(rb.position + (localDirection * (speed * Time.deltaTime)));
    }

    public void OnPlayerMove(InputValue value) {
        // vector with corresponding components to wasd and arrow inputs
        Vector2 inputVector = value.Get<Vector2>();
        direction.x = inputVector.x;
        direction.z = inputVector.y;
        direction.y = 0;
    }
}
