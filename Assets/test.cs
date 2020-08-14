using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class test : MonoBehaviour
{

    public int count = 0;
    private void OnGUI()
    {
        if (GUILayout.Button("测试"))
        {
            //设定当前角色
            StateMachine.waitCommand = GameObject.Find("BattleArea_Object_Camp0_Role0").GetComponent<BattleArea_Object_Camp_Role>();

            StateMachine.RoleTurn();
        }

        if (GUILayout.Button("发牌"))
        {
            //获取一个卡牌实例
            StateMachine.waitCommand.DrawCard(1);
        }
    }
}
