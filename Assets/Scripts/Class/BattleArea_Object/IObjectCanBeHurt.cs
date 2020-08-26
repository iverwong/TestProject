using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一个能被伤害的物体，提供HP属性和HP值修改的方法、事件
/// </summary>
interface IObjectCanBeHurt
{
    /// <summary>
    /// HP属性
    /// </summary>
    float HP { get; set; }
    /// <summary>
    /// HP更改后的事件
    /// </summary>
    event EventHandler.ChangeValueEventHandler HPChangedBroadcast;
}
