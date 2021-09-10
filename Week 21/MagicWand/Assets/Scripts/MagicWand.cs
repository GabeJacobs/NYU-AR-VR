using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicWand : XRGrabInteractable
{
    [HideInInspector]
    public XRRayInteractor currentTeleporter = null;
    [SerializeField]
    private XRRayInteractor leftTeleporter;
    [SerializeField]
    private XRRayInteractor rightTeleporter;

    protected override void OnEnable() //changed to protected override the protected virtual OnEnable() in XRBaseInteractable
    {
        base.OnEnable();//call the virtual OnEnable() on XRBaseInteractable to enable interactions
        selectEntered.AddListener(OnWandActivate);//replaced event to match upgraded API
        selectExited.AddListener(OnWandDeactivate);//replaced event to match upgraded API
    }

    protected override void OnDisable()//changed to protected override the protected virtual OnDisable() in XRBaseInteractable
    {
        base.OnDisable();//call the virtual OnDisable() on XRBaseInteractable to unregister interactions
        selectEntered.RemoveListener(OnWandActivate);//replaced event to match upgraded API
        selectExited.RemoveListener(OnWandDeactivate);//replaced event to match upgraded API
    }

    IEnumerator ActivateWand()
    {
        yield return new WaitForSeconds(0);
        currentTeleporter?.gameObject.SetActive(true);
    }

    IEnumerator DeactivateWand()
    {
        yield return new WaitForSeconds(0);
        currentTeleporter?.gameObject.SetActive(false);
        currentTeleporter = null;
    }
    void OnWandActivate(SelectEnterEventArgs args)//changed parameter from XRBaseInteractor per selectEntered signature, args has interactor and interactable members.
    {
        Hand hand = args.interactor.GetComponentInChildren<Hand>();
        if (hand != null)
        {
            switch (hand.type)
            {
                case HandType.Left:
                    currentTeleporter = leftTeleporter;
                    break;
                case HandType.Right:
                    currentTeleporter = rightTeleporter;
                    break;
            }
        }
        StartCoroutine(ActivateWand());
    }

    void OnWandDeactivate(SelectExitEventArgs args)//changed parameter from XRBaseInteractor per selectExited signature
    {
        StartCoroutine(DeactivateWand());
    }
}
