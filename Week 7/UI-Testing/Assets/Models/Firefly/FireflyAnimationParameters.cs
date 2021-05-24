using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAnimationParameters : MonoBehaviour
{
    public Animator anim;
    public float animationOffset;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("AnimationOffset", animationOffset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
