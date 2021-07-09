using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum HandType
{
    Left,
    Right
};

public class Hand : MonoBehaviour
{
    public HandType type = HandType.Left;
    public bool isHidden { get; private set; } = false;

    public InputAction trackedAction = null;
    private bool m_isCurrentlyTracked = false;

    private List<SkinnedMeshRenderer> m_currentRenderers = new List<SkinnedMeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        trackedAction.Enable();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if(isTracked == 1.0f &&  !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            Show();
        } else if (isTracked == 0.0f && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            Hide();
        }
            
    }

    public void Show()
    {
        foreach (SkinnedMeshRenderer renderer in m_currentRenderers)
        {
            renderer.enabled = true;
            Debug.Log("Showing Hands");
        }

        isHidden = false;

    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Hide()
    {
        m_currentRenderers.Clear();
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            Debug.Log("Hiding Hands");
            renderer.enabled = false;
            m_currentRenderers.Add(renderer);
        }

        isHidden = true;
    }
}
