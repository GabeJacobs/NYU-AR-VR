using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancing : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void startDancing()
    {
        anim.SetBool("IsDancing", true);

    }

    public void stopDancing()
    {
        anim.SetBool("IsDancing", false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
