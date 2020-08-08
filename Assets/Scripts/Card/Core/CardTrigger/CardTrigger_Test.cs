using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTrigger_Test : CardTriggerAbstract
{
    public int distance;
    public CardTrigger_Test(int _distance)
    {
        distance = _distance;
    }

    public override List<BaseInteractableObject> CatchTarget(BattleArea_Grid_Tile_MeshCollider _meshCollider)
    {
        List<BaseInteractableObject> result = new List<BaseInteractableObject>();
        result.Add(_meshCollider.tile.objectOnIt);
        return result;
    }

    public override void EndFrameSign(BattleArea_Grid_Tile_MeshCollider _meshCollider)
    {
        _meshCollider.tile.frame.LightOff();
    }

    public override void EndTrigger()
    {
        BattleAreaCoordinate roleCoordinate = StateMachine.waitCommand.standOn.coordinate;
        List<BattleAreaCoordinate> listCoordinate = roleCoordinate.AroundPoint(distance);
        foreach (BattleAreaCoordinate each in listCoordinate)
        {
            //排除自身
            if (roleCoordinate.x == each.x && roleCoordinate.y == each.y) continue;
            each.FindTile().plane.LightOff(Color.white);
        }
    }

    public override void FrameSign(BattleArea_Grid_Tile_MeshCollider _meshCollider)
    {
        _meshCollider.tile.frame.LightOn();
    }

    public override void Trigger()
    {
        BattleAreaCoordinate roleCoordinate = StateMachine.waitCommand.standOn.coordinate;
        List<BattleAreaCoordinate> listCoordinate = roleCoordinate.AroundPoint(distance);
        foreach(BattleAreaCoordinate each in listCoordinate)
        {
            //排除自身
            if (roleCoordinate.x == each.x && roleCoordinate.y == each.y) continue;
            each.FindTile().plane.LightOn(Color.green);
        }
    }
}
