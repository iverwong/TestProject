using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimationControl : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Backstab"))
        {
            anim.SetTrigger("Backstab");
        }
        if (GUILayout.Button("Assassinate"))
        {
            anim.SetTrigger("Assassinate");
        }
    }

}
