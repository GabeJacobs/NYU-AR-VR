using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

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
    public InputAction gripAction;

    public Animator handAnimator = null;
    private int m_gripAmountParameter = 0;
    
    private bool m_isCurrentlyTracked = false;

    private List<Renderer> m_currentRenderers = new List<Renderer>();

    private Collider[] m_colliders = null;

    public bool isCollisionEnabled { get; private set; } = false;

    public XRBaseInteractor interactor = null;

    private void Awake()
    {
        if (interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }
    }

    private void OnEnable()
    {
        interactor.onSelectEntered.AddListener(onGrab);
        interactor.onSelectExited.AddListener(onRelease);
    }

    private void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(onGrab);
        interactor.onSelectExited.RemoveListener(onRelease);
    }
    
    

    void Start()
    {
        trackedAction.Enable();
        m_colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        m_gripAmountParameter = Animator.StringToHash("GripAmount");
        gripAction.Enable();
        Hide();
    }

    void UpdateAnimations()
    {
        float gripAmount = gripAction.ReadValue<float>();
        handAnimator.SetFloat(m_gripAmountParameter, gripAmount);
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

        UpdateAnimations();
    }

    public void Show()
    {
        foreach (Renderer renderer in m_currentRenderers)
        {
            renderer.enabled = true;
            Debug.Log("Showing Hands");
        }

        isHidden = false;
        EnableCollisions(true);

    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Hide()
    {
        m_currentRenderers.Clear();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Debug.Log("Hiding Hands");
            renderer.enabled = false;
            m_currentRenderers.Add(renderer);
        }

        isHidden = true;
        EnableCollisions(false);

    }

    public void EnableCollisions(bool enable)
    {
        if (isCollisionEnabled == enable)
        {
            return;
        }
        isCollisionEnabled = enable;
        foreach(Collider collider in m_colliders)
        {
            collider.enabled = isCollisionEnabled;
        }
    }

    void onGrab(XRBaseInteractable grabbedObject)
    {
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand == true)
            {
                Hide();
            }
        }
    }
    void onRelease(XRBaseInteractable grabbedObject)
    {
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand == true)
            {
                Show();
            }
        }
    }
}
