using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAnimationControl : MonoBehaviour
{

    void BeAttackedOver()
    {
        GetComponent<Animator>().SetBool("BeAttacked", false);
    }
}
