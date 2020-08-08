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
            Object cardUI = Resources.Load("UI/CardUI");
            GameObject prefebCardUI;
            prefebCardUI = (GameObject)PrefabUtility.InstantiatePrefab(cardUI);
            //载入资源
            CardUI cardUIScript = prefebCardUI.GetComponent<CardUI>();
            cardUIScript.card = new Card_Test();
            cardUIScript.Init(); 
        }
        if (GUILayout.Button("测试"))
        {
            StateMachine.waitCommand = GameObject.Find("BattleArea_Object_Camp0_Role0").GetComponent<BattleArea_Object_Camp_Role>();
            StateMachine.RoleTurn();
        }
    }
}
