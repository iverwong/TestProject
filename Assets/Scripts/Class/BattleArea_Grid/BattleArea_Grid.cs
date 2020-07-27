using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//记录关于战场网格的操作状态，判断如何相应玩家操作
public class BattleArea_Grid : MonoBehaviour
{
    public enum BattleArea_Grid_State
    {
        NONE,//无响应状态
        OBSERVER,//观察者状态
        COMMANDER,//等待命令状态
        HOLDER,//手持卡牌状态
    }


}
