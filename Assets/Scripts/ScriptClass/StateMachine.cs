using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制战斗场景中玩家的状态，记录战场信息
public static class StateMachine
{
    //当前战场状态
    public static BattleArea_Grid.BattleArea_Grid_State state = BattleArea_Grid.BattleArea_Grid_State.NONE;
    //当前控制卡牌
    public static CardAbstract currentCard = null;
    //当前玩家控制的角色，如为空则不为玩家控制阶段
    public static BattleArea_Object_Camp_Role waitCommand;



    //到达玩家回合(测试）
    public static void RoleTurn()
    {
        //更改状态
        state = BattleArea_Grid.BattleArea_Grid_State.COMMANDER;
        //初始化亮起移动区域
        RoleMovePlaneLightOnOff(true, 10);

    }

    /// <summary>
    /// 控制角色周围plane地块亮起或关闭，用于移动
    /// </summary>
    /// <param name="isOn">亮起或关闭</param>
    /// <param name="distance">施放距离</param>
    public static void RoleMovePlaneLightOnOff(bool isOn,int distance)
    {
        //获取角色坐标
        BattleAreaCoordinate coordinate = waitCommand.standOn.coordinate;
        //根据角色可移动范围点灭plane（待修改）
        List<BattleAreaCoordinate> aroundCoordinate = coordinate.AroundPoint(distance,true);
        foreach (BattleAreaCoordinate each in aroundCoordinate)
        {
            if (isOn)
            {
                each.FindTile().plane.LightOn();
            }
            else
            {
                each.FindTile().plane.LightOff();
            }

        }

    }
} 
