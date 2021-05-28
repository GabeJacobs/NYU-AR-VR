using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycasting : MonoBehaviour
{
    private GameObject yellowGameObject;
    public Color originalColor;

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
            yellowGameObject = hit.collider.gameObject;
        }
        else
        {
            yellowGameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", originalColor);
        }
    }
}
