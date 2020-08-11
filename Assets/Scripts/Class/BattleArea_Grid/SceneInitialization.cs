using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化场景内物体数据，包括生成初始卡牌等（持续补充）
/// </summary>
public class SceneInitialization : MonoBehaviour
{
    [Header("Camp0")]
    public BattleArea_Object_Camp_Role camp0_role0;
    [Header("Camp1")]
    public BattleArea_Object_Camp_Role camp1_role0;
    private void Start()
    {
        //camp0_role0 = GameObject.Find("BattleArea_Object_Camp0_Role0").GetComponent<BattleArea_Object_Camp_Role>();
        CardInit();//为测试角色创建5张卡牌
    }

    internal void  CardInit()
    {
        for (int i = 0; i < 5; i++)
        {
            camp0_role0.CardLibrary.Add(new Card_Test());
        }

        //复制牌库牌到待发牌库
        camp0_role0.CardLibraryToCardReady();
    }
}
