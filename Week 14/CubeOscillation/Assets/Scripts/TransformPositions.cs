using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPositions : MonoBehaviour
{

    public GameObject blueCube;
    public GameObject yellowCube;

    public Vector3 bluePos;
    public Vector3 yellowPos;

    public float speed = 1f;
    // Start is called before the first frame update
    void computePositions()
    {
        Matrix4x4 blue4x4 = blueCube.transform.localToWorldMatrix;
        bluePos = new Vector3(blue4x4[0,3], blue4x4[1,3] + 3, blue4x4[2,3]);

        Matrix4x4 yellow4x4 = yellowCube.transform.localToWorldMatrix;
        yellowPos = new Vector3(yellow4x4[0,3], yellow4x4[1,3] - 3, yellow4x4[2,3]);

    }

    // Update is called once per frame
    void Update()
    {
        computePositions();
        transform.position = Vector3.Lerp(bluePos, yellowPos, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
