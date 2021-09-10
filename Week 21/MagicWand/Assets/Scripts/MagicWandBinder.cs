using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[VFXBinder("Magic Wand")]
public class MagicWandBinder : VFXBinderBase
{


    [VFXPropertyBinding("System.Single")]
    public ExposedProperty wandStart = "Want Start";
    
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty wandEnd = "Want End";

    public MagicWand wand;

    public Transform wandStartPosition;
    public Transform wandEndPosition;

    // The IsValid method need to perform the checks and return if the binding
    // can be achieved.
    public override bool IsValid(VisualEffect component)
    {
        return wand != null && wandStartPosition!= null && wandEndPosition!= null && component.HasVector3(wandStart) && component.HasVector3(wandEnd);
    }

    // The UpdateBinding method is the place where you perform the binding,
    // by assuming that it is valid. This method will be called only if
    // IsValid returned true.
    public override void UpdateBinding(VisualEffect component)
    {
        component.SetVector3(wandStart,  transform.InverseTransformPoint(wandStartPosition.position));
        component.SetVector3(wandEnd, transform.InverseTransformPoint(wandEndPosition.position));

    }
}
