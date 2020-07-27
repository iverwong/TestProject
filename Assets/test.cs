using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class test : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUILayout.Button("发牌"))
        {
            Object cardUI = Resources.Load("CardUI");
            GameObject prefebCardUI;
            prefebCardUI = PrefabUtility.InstantiatePrefab(cardUI) as GameObject;
            //Resources.UnloadAsset 待学习
            Card_TestAttack test = new Card_TestAttack("测试名称", "测试描述", "0x0ss-85", null, new Vector3Int(),new CardGenericTrigger_PointTest());//new关键词待更改
            prefebCardUI.GetComponent<CardUI>().Initialize(test); 
        }
        if (GUILayout.Button("测试"))
        {
            StateMachine.waitCommand = FindObjectOfType<BattleArea_Object_Camp_Role>();
            StateMachine.RoleTurn();
        }
    }
}
