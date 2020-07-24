using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制战斗场景中玩家的状态，记录战场信息
public static class StateMachine
{
    //当前状态
    public static BattleArea_Grid.BattleArea_Grid_State state = BattleArea_Grid.BattleArea_Grid_State.NONE;


    //当前玩家控制的角色，如为空则不为玩家控制阶段
    public static BattleArea_Object_Camp_Role waitCommand;



    //到达玩家回合
    public static void RoleTurn()
    {
        //获取角色坐标
        BattleAreaCoordinate coordinate = waitCommand.standOn.coordinate;
        //根据角色可移动范围点亮plane（待修改）
        List<BattleAreaCoordinate> aroundCoordinate = coordinate.AroundPoint(4);
        foreach(BattleAreaCoordinate each in aroundCoordinate)
        {
            each.FindTile().plane.LightOn();
        }
        //更改状态
        state = BattleArea_Grid.BattleArea_Grid_State.COMMANDER;
    }
} 
