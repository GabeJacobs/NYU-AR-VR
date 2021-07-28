using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHighlightOnHover : MonoBehaviour
{
    public Material hoverMaterial;
    
    private XRBaseInteractable m_interactable = null;
    private Renderer m_renderer = null;

    private Material[] m_currentMaterials = null;
    // Start is called before the first frame update
    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
        m_interactable = GetComponent<XRBaseInteractable>();
        m_currentMaterials = m_renderer.materials;
        
    }

    private void OnEnable()
    {
        m_interactable.onHoverEntered.AddListener(SwapInMaterial);
        m_interactable.onHoverExited.AddListener(SwapOutMaterial);

    }

    private void OnDisable()
    {
        m_interactable.onHoverEntered.RemoveListener(SwapInMaterial);
        m_interactable.onHoverExited.RemoveListener(SwapOutMaterial);
    }

    void SwapInMaterial(XRBaseInteractor interactor)
    {
        Material[] hoverMats = new Material[m_currentMaterials.Length];
        for (int i = 0; i < m_currentMaterials.Length; i++)
        {
            hoverMats[i] = hoverMaterial;
        }

        m_renderer.materials = hoverMats;
    }
    
    void SwapOutMaterial(XRBaseInteractor interactor)
    {
        m_renderer.materials = m_currentMaterials;
        m_currentMaterials = null;

    }
}
