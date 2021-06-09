using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTheForce : MonoBehaviour
{
    public Rigidbody rb;

    public float minGroundDistance;
    
    private bool canUseForce;
    public AudioSource theOneSound;
    public AudioSource neoFall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        
        if (Physics.Raycast(landingRay, out hit, minGroundDistance) && hit.collider.gameObject.CompareTag("Ground"))
        {
            
                Debug.DrawRay(transform.position, Vector3.down * minGroundDistance, Color.green);
                canUseForce = true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * minGroundDistance, Color.red);
            canUseForce = false;
        }

    }
    
    private void OnJump(){
        if (canUseForce)
        {
            theOneSound.Play();
            neoFall.Pause();
            rb.AddForce(Vector3.up * 360, ForceMode.Impulse);
            rb.AddForce(Camera.main.transform.forward * 100, ForceMode.Impulse);

        }
      
    }
    
}
