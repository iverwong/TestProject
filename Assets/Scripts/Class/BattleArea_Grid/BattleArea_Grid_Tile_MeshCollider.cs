using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class BattleArea_Grid_Tile_MeshCollider : MonoBehaviour
{
    //绑定tile脚本
    public BattleArea_Grid_Tile tile;
    //记录自身坐标信息
    public BattleAreaCoordinate coordinate;


    private void Start()
    {
        //初始化当前坐标
        string[] s = tile.name.Split('_');
        coordinate = new BattleAreaCoordinate(int.Parse(s[s.Length - 2]), int.Parse(s[s.Length - 1]));
    }

    private void OnMouseEnter()
    {
        switch (StateMachine.state)
        {
            case BattleArea_Grid.BattleArea_Grid_State.NONE:
                break;
            case BattleArea_Grid.BattleArea_Grid_State.COMMANDER:
                if (tile.plane.IsLight())
                {
                    //目标寻路
                    BattleAreaCoordinate roleCoordinate = StateMachine.waitCommand.standOn.coordinate;
                    BattleAreaCoordinate aStar = coordinate.AStarPathing(roleCoordinate);
                    do
                    {
                        aStar.FindTile().frame.LightOn();
                        aStar = aStar.parentCoordinate;
                    } while (aStar != null);
                }
                break;
        }
    }
    private void OnMouseExit()
    {
        switch (StateMachine.state)
        {
            case BattleArea_Grid.BattleArea_Grid_State.NONE:
                break;
            case BattleArea_Grid.BattleArea_Grid_State.COMMANDER:

                if (tile.frame.IsLight())
                {
                    //目标寻路
                    BattleAreaCoordinate roleCoordinate = StateMachine.waitCommand.standOn.coordinate;
                    BattleAreaCoordinate aStar = coordinate.AStarPathing(roleCoordinate);
                    do
                    {
                        aStar.FindTile().frame.LightOff();
                        aStar = aStar.parentCoordinate;
                    } while (aStar != null);
                }
                break;
        }
    }

    private void OnMouseDown()
    {
        switch (StateMachine.state)
        {
            case BattleArea_Grid.BattleArea_Grid_State.NONE:
                break;
            //角色移动
            case BattleArea_Grid.BattleArea_Grid_State.COMMANDER:

                if (tile.plane.IsLight())
                {
                    OnMouseExit();
                    StateMachine.waitCommand.MoveToPosition(coordinate, 0.05f);
                }
                break;
        }
    }
}
