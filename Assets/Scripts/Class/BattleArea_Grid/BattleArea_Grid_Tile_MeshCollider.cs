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
        //EventSystem（UI界面碰撞）始终优先于游戏物体
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
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
            case BattleArea_Grid.BattleArea_Grid_State.HOLDER:
                tile.frame.LightOn();
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
            case BattleArea_Grid.BattleArea_Grid_State.HOLDER:
                tile.frame.LightOff();
                break;
        }
    }

    private void OnMouseDown()
    {
        //EventSystem（UI界面碰撞）始终优先于游戏物体
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
        switch (StateMachine.state)
        {
            //无响应状态
            case BattleArea_Grid.BattleArea_Grid_State.NONE:
                break;
            //角色移动
            case BattleArea_Grid.BattleArea_Grid_State.COMMANDER:

                if (tile.plane.IsLight())
                {
                    //不再显示当前地块外框
                    OnMouseExit();
                    //灭掉玩家周围点亮地块（待修改）
                    StateMachine.RoleMovePlaneLightOnOff(false, 20);
                    StateMachine.waitCommand.MoveToPosition(coordinate, 0.05f);
                    //标记位置改变
                    StateMachine.waitCommand.standOn.objectOnIt = null;
                    StateMachine.waitCommand.standOn = tile;
                    tile.objectOnIt = StateMachine.waitCommand;
                }
                break;
            //手持卡牌
            case BattleArea_Grid.BattleArea_Grid_State.HOLDER:
                //判断是否在允许范围内
                if (!tile.plane.IsLight())
                {
                    Debug.Log("技能施放在允许的范围外");
                    return;
                }
                //不再显示当前地块外框
                OnMouseExit();
                //将目标添加到StateMachine中
                switch (StateMachine.triggerType)
                {
                    //round_point点目标
                    case CardTargetTrigger.TriggerType.ROUND_POINT:
                        if (tile.objectOnIt != null) StateMachine.listRole.Add(tile.objectOnIt);
                        break;
                }
                //取消Trigger所标记的地块
                StateMachine.currentCard.CardTrigger.Trigger(reverse: true);
                //触发Action列表
                StateMachine.currentCard.Action();
                break;
        }
    }
    private void OnMouseOver()
    {
        //当鼠标停留在UI界面上时，调用OnMouseExit()
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            OnMouseExit();
        }
    }
}
