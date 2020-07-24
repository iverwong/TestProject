using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackdawAnimationControl : MonoBehaviour
{
    Animator anim;
    public GameObject throwWeapon;
    public Transform handTransform;
    GameObject tempWeapon;
    Rigidbody tempWeaponRig;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    //private void OnGUI()
    //{
    //    if (GUILayout.Button("PoisonedDagger"))
    //    {
    //        anim.SetTrigger("PoisonedDagger");
    //    }
    //    if (GUILayout.Button("Insane"))
    //    {
    //        anim.SetTrigger("Insane");
    //    }
    //    if (GUILayout.Button("ToxicSmog"))
    //    {
    //        anim.SetTrigger("ToxicSmog");
    //    }
    //}
    void PoisonedDaggerThrowWeapon()
    {
        Vector3 rightHandPosition = handTransform.position;
        tempWeapon = Instantiate(throwWeapon, rightHandPosition, new Quaternion());
        tempWeaponRig = tempWeapon.GetComponent<Rigidbody>();
        tempWeaponRig.angularVelocity = new Vector3(5f , 0f);
        tempWeaponRig.velocity = new Vector3(-0.3f,5.5f,0f);
    }
    void PosionedDaggerThrowCatch()
    {
        Destroy(tempWeapon);

    }
}
