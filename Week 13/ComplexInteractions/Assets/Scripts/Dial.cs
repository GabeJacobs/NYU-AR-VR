using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    private Vector3 m_startRotation;
    private Material[] materials;

    private MeshRenderer m_meshRenderer = null;

    private void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        materials = (Material[])m_meshRenderer.materials.Clone();
    }

    // Start is called before the first frame update
    public void StartTurn()
    {
        m_startRotation = transform.localEulerAngles;
        Material[] newMaterials = (Material[])materials.Clone();
 
        for (var i = 0; i < newMaterials.Length; i++) {
            newMaterials[i].color = Color.red;
        }
        m_meshRenderer.materials = newMaterials;
    }

    public void StopTurn()
    {
        for (var i = 0; i < materials.Length; i++) {
            if (i == 0)
            {
                materials[i].color = Color.red;
            }

            if (i == 1)
            {
                materials[i].color = Color.white;
            }
        }
        m_meshRenderer.materials = materials;
    }
    // Update is called once per frame
    public void DialUpdate(float angle)
    {
        Vector3 angles = m_startRotation;
        angles.z += angle;
        transform.localEulerAngles = angles;
    }
}
