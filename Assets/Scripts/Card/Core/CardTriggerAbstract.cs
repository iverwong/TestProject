using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTriggerAbstract
{
    /// <summary>
    /// 调用该触发器，在选择卡牌后触发
    /// </summary>
    public abstract void Trigger();
    /// <summary>
    /// 结束调用该触发器，在选择目标或取消时触发
    /// </summary>
    public abstract void EndTrigger();

    public abstract List<BaseInteractableObject> CatchTarget(BattleArea_Grid_Tile_MeshCollider _meshCollider);

    public abstract void FrameSign(BattleArea_Grid_Tile_MeshCollider _meshCollider);
    public abstract void EndFrameSign(BattleArea_Grid_Tile_MeshCollider _meshCollider);
}
