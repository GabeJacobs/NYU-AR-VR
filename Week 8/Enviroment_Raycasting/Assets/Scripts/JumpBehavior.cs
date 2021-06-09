using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour {

    public float jumpForce;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            playerRb.AddForce(other.transform.up * jumpForce, ForceMode.Impulse);
        }
    }

}
