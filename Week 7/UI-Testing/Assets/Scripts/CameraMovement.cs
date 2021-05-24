using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    public float horizontalSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            Debug.Log("Camera requires a reference to the player.");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position;

        if(Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            float h = horizontalSpeed * Input.GetAxis("Mouse X");

            transform.Rotate(0, h, 0);
        }
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
