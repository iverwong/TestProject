using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌目标触发器
/// </summary>

[CreateAssetMenu(fileName = "NewTrigger", menuName = "MyMenu/Trigger")]
public class CardTargetTrigger : ScriptableObject
{
    public enum TriggerType
    {
        NONE,//无触发类型
        ROUND_POINT,//圆形范围内点类型
    }

    [Header("目标触发器类型")]
    public TriggerType triggerType;
    [Header("触发范围")]
    public int distance;
    [Header("触发角度")]
    public int angle;
    /// <summary>
    /// 当卡牌调用Use方法时触发
    /// </summary>
    /// <param name="reverse">反向触发</param>
    public void Trigger(bool reverse)
    {
        switch (triggerType)
        {
            case TriggerType.NONE:
                break;
            case TriggerType.ROUND_POINT:
                //更新触发器状态
                StateMachine.triggerType = TriggerType.ROUND_POINT;
                //点亮周围一定范围的地块，并以额外颜色显示
                BattleAreaCoordinate roleCoordinate = StateMachine.waitCommand.standOn.coordinate;
                List<BattleAreaCoordinate> listCoordinate = roleCoordinate.AroundPoint(distance);
                foreach(BattleAreaCoordinate each in listCoordinate)
                {
                    //排除自身
                    if (roleCoordinate.x == each.x && roleCoordinate.y == each.y) continue;
                    if (reverse)
                    {
                        each.FindTile().plane.LightOff(Color.white);
                    }
                    else
                    {
                        each.FindTile().plane.LightOn(Color.green);
                    }

                }
                break;
        }
    }

    public void Trigger()
    {
        Trigger(false);
    }
}