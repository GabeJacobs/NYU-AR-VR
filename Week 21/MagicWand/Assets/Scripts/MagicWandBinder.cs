using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[VFXBinder("Magic Wand")]
public class MagicWandBinder : VFXBinderBase
{


    [VFXPropertyBinding("System.Single")]
    public ExposedProperty wandStart = "Wand Start";
    
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty wandEnd = "Wand End";
    
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty wandActive = "Wand Active";
    
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty hasHit = "Has Hit";
    
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty hitPoint = "Hit Point";
    
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty hitNormal = "Hit Normal";
    
    public MagicWand wand;

    public Transform wandStartPosition;
    public Transform wandEndPosition;

    // The IsValid method need to perform the checks and return if the binding
    // can be achieved.
    public override bool IsValid(VisualEffect component)
    {
        return wand != null && wandStartPosition!= null &
            wandEndPosition!= null &&
            component.HasVector3(wandStart) &&
            component.HasVector3(wandEnd) &&
            component.HasBool(hasHit) &&
            component.HasVector3(hitNormal) &&
            component.HasVector3(hitPoint) &&
            component.HasBool(wandActive);
    }

    // The UpdateBinding method is the place where you perform the binding,
    // by assuming that it is valid. This method will be called only if
    // IsValid returned true.
    public override void UpdateBinding(VisualEffect component)
    {

        if (wand.currentTeleporter  != null && wand.currentTeleporter.GetCurrentRaycastHit(out RaycastHit hit))
        {
            component.SetBool(hasHit, true);
            component.SetVector3(hitPoint, transform.InverseTransformPoint(hit.point));
            component.SetVector3(hitNormal, transform.InverseTransformDirection(hit.normal));

        }
        else
        {
            component.SetBool(hasHit, false);
        }
        
        component.SetVector3(wandStart,  transform.InverseTransformPoint(wandStartPosition.position));
        component.SetVector3(wandEnd, transform.InverseTransformPoint(wandEndPosition.position));
        component.SetBool(wandActive, wand.currentTeleporter != null);


    }
}
