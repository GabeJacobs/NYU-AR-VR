using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Neo : MonoBehaviour
{
    
    public Vector3 direction;
    public float speed;
    public Rigidbody rb;
    public bool canMove;
    public bool inJumpForceArea;
    public float jumpForce;

    public AudioSource neoBanter;
    public AudioSource neoJump;
    public AudioSource neoFall;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (canMove)
        {
            Vector3 localDirection = transform.TransformDirection(direction);
            rb.MovePosition(rb.position + (localDirection * (speed * Time.deltaTime)));     
        }
    }

    public void OnPlayerMove(InputValue value) {
        // vector with corresponding components to wasd and arrow inputs
        Vector2 inputVector = value.Get<Vector2>();
        direction.x = inputVector.x;
        direction.z = inputVector.y;
        direction.y = 0;
    }

    private void OnJump(){
        if (inJumpForceArea)
        {
            neoBanter.Stop();
            neoJump.Play();
            jumpForce = 45;
        }
        else
        {
            jumpForce = 5;
        }
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

    public void playBanter()
    {
        neoBanter.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("JumpForce"))
        {
            inJumpForceArea = true;
        }

        if (other.CompareTag("FallZone") && !neoJump.isPlaying)
        {
            neoFall.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JumpForce"))
        {
            inJumpForceArea = false;
        }
    }
}
