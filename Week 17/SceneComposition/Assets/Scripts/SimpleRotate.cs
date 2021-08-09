using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{

    public float speed = 0.04f;
    private float m_angle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        m_angle += speed * Time.deltaTime;
        transform.Rotate(Vector3.one, m_angle);
    }
}
