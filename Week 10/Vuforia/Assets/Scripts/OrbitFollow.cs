using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitFollow : MonoBehaviour {

    public Transform target;
    public float speed;
    public float yOffset;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(Vector3.left, ForceMode.Impulse);
        rb.AddForce(target.position, ForceMode.Acceleration);


    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 relativePos = target.position + new Vector3(0, yOffset, 0) - transform.position;
        // // Vector3 relativePosXZ = Vector3.ProjectOnPlane(relativePos, Vector3.up);
        //
        // Quaternion lookRot = Quaternion.LookRotation(relativePos);
        //
        // Quaternion current = transform.rotation;
        //
        // transform.localRotation = Quaternion.Slerp(current, lookRot, Time.deltaTime);
        //
        // // go forward
        // transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));

    }
}
