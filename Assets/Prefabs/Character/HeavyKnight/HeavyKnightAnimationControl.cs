using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyKnightAnimationControl : MonoBehaviour
{
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    //private void OnGUI()
    //{
    //    if (GUILayout.Button("SmashingAgainst"))
    //    {
    //        anim.SetBool("SmashingAgainst", true);
    //    }
    //    if (GUILayout.Button("SwordBlockIdle"))
    //    {
    //        anim.SetBool("SwordBlockIdle", !anim.GetBool("SwordBlockIdle"));
    //    }
    //    if (GUILayout.Button("Block"))
    //    {
    //        anim.SetTrigger("Block");
    //    }
    //    if (GUILayout.Button("InterdictionStand"))
    //    {
    //        anim.SetBool("InterdictionStand", !anim.GetBool("InterdictionStand"));
    //    }
    //    if (GUILayout.Button("InterdictionStandAttack") && anim.GetBool("InterdictionStand"))
    //    {
    //        anim.SetBool("InterdictionStandAttack", true);
    //    }
    //    if (GUILayout.Button("StormStrike"))
    //    {
    //        anim.SetBool("StormStrike", true);
    //    }
    //    if (GUILayout.Button("Execute"))
    //    {
    //        anim.SetBool("Execute", true);
    //    }
    //    if (GUILayout.Button("WeaponDisintegrate"))
    //    {
    //        anim.SetBool("WeaponDisintegrate", true);
    //    }
    //}

    void SmashingAgainstEnd()
    {
        anim.SetBool("SmashingAgainst", false);
    }
    void TestMotionEnd()
    {
        anim.SetBool("TestMotion", false);
    }
    void InterdictionStandIdle()
    {
        anim.SetTrigger("InterdictionStandIdle");
    }
    void InterdictionStandAttackEnd()
    {
        anim.SetBool("InterdictionStandAttack", false);
    }
    void StormStrike1_2()
    {
        anim.SetTrigger("StormStrike1_2");
    }
    void StormStrikeEnd()
    {
        anim.SetBool("StormStrike", false);
    }
    void ExecuteEnd()
    {
        anim.SetBool("Execute", false);
    }
    void WeaponDisintegrateEnd()
    {
        anim.SetBool("WeaponDisintegrate", false);
    }
}
