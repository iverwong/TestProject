using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 防御模式的抽象类
/// </summary>
public abstract class DefenseModeAbstract
{
    /// <summary>
    /// 防御模式实例持有者
    /// </summary>
    public BattleArea_Object_Camp_Role owner;
    public DefenseModeAbstract(BattleArea_Object_Camp_Role _owner)
    {
        owner = _owner;
    }
}