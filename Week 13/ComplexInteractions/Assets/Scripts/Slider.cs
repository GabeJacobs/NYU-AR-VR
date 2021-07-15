using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class VolumeEvent : UnityEvent<float>
{
}
public class Slider : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;

    private MeshRenderer meshRenderer = null;

    public VolumeEvent onVolumeEvent = new VolumeEvent();

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnSlideStart()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
    }
    public void OnSlideStop()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void UpdateSlider(float percent)
    {
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percent);
        onVolumeEvent?.Invoke(percent);
    }
}
