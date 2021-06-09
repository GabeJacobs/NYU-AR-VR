using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour {

    [Header("Player Camera")]
    public Camera playerCamera;

    public float deltaX;
    public float deltaY;

    public float xRot; // rotation around x axis in degrees
    public float yRot; // rotation around y axis in degrees

    [Range(0, 1)]
    public float mouseSensitiviy = 1.0f;
    public float lookSpeed = 10.0f;

    // Start is called before the first frame update
    void Start() {
        playerCamera = Camera.main;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        
        yRot += deltaX * mouseSensitiviy;
        xRot += deltaY * mouseSensitiviy;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localEulerAngles = new Vector3(0, yRot, 0);
        playerCamera.transform.localEulerAngles = new Vector3(-xRot, 0, 0);
        
        
       
        
    }
    
    //OnCameraLook event handler

    public void OnCameraLook(InputValue value) {
        Vector2 inputVector = value.Get<Vector2>();
        deltaX = inputVector.x;
        deltaY = inputVector.y;
    }
}
