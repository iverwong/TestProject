using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于地形上的Buff
/// </summary>
public abstract class TileBuffAbstract : BuffAbstract
{
    protected TileBuffAbstract(GameObject _object, int? _count) : base(_object, _count)
    {
    }
}
