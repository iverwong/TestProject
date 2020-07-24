using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCrossbowmanAnimationControl : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    //private void OnGUI()
    //{
    //    if (GUILayout.Button("PiercingShot"))
    //    {
    //        anim.SetBool("PiercingShot", true);
    //    }
    //    if (GUILayout.Button("QuickShotIdle"))
    //    {
    //        anim.SetBool("QuickShotIdle", !anim.GetBool("QuickShotIdle"));
    //    }
    //    if (GUILayout.Button("QuickShot"))
    //    {
    //        anim.SetTrigger("QuickShot");
    //    }
    //    if (GUILayout.Button("PerforativeShot"))
    //    {
    //        anim.SetTrigger("PerforativeShot");
    //    }
    //    if (GUILayout.Button("ArmorBlock"))
    //    {
    //        anim.SetBool("ArmorBlock", !anim.GetBool("ArmorBlock"));
    //    }
    //    if (GUILayout.Button("Block"))
    //    {
    //        anim.SetTrigger("Block");
    //    }
    //    if (GUILayout.Button("StrikeBack"))
    //    {
    //        anim.SetTrigger("StrikeBack");
    //    }
    //    if (GUILayout.Button("PreciseShot"))
    //    {
    //        anim.SetTrigger("PreciseShot");
    //    }
    //}
}
