using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoleAnimationControl : MonoBehaviour
{
    public Transform battleAreaGrid;
    private Transform beAttacked;
    private Transform selectedGridMeshCollider;

    void ReadyToAttack()
    {

    }
    //void Attack()
    //{
    //    //获取选中的网格
    //    selectedGridMeshCollider = battleAreaGrid.GetComponent<BattleArea_Grid>().selectedGridMeshCollider.transform;
    //    //获取攻击对象
    //    beAttacked = selectedGridMeshCollider.parent.GetComponent<BattleArea_Grid_Tile>().objectOnIt;
    //    //播放被攻击对象动画
    //    beAttacked.GetChild(0).GetComponent<Animator>().SetBool("BeAttacked",true);
    //    //隐藏该物体（造成攻击伤害）
    //    //beAttacked.gameObject.SetActive(false);
    //    //selectedGridMeshCollider.parent.GetComponent<BattleArea_Grid_Tile>().objectOnIt = null;
    //}
    //void AttackOver()
    //{
    //    GetComponent<Animator>().SetBool("IsAttacking", false);
    //}
}
