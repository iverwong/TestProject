using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于游戏物体上的Buff
/// </summary>
public abstract class RoleBuffAbstract : BuffAbstract
{
    protected RoleBuffAbstract(GameObject _object, int? _count) : base(_object, _count)
    {
    }

}
