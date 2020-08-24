using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可交互物体的基类
/// </summary>
public abstract class BaseInteractableObject : MonoBehaviour
{
    /// <summary>
    /// 角色Buff列表
    /// </summary>
    public List<RoleBuffAbstract> Buff = new List<RoleBuffAbstract>();
    /// <summary>
    /// 角色所在坐标
    /// </summary>
    public abstract BattleAreaCoordinate ObjectCoordinate { get; set; }
}
