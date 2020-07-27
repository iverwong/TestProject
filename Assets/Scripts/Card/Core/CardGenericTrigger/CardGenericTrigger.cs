using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌泛型触发器，抽象类
/// </summary>


public abstract class CardGenericTrigger : ScriptableObject
{
    /// <summary>
    /// 启动触发器
    /// </summary>
    public abstract void Trigger();
}
