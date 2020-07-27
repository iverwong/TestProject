using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这是一个触发器的测试类，向周边一定范围内的点施放
/// </summary>

[CreateAssetMenu(fileName = "New Trigger", menuName = "MyMenu/Trigger/Test")]
public class CardGenericTrigger_PointTest:CardGenericTrigger
{
    [Header("触发器参数")]
    public int distance;//施放距离
    public override void Trigger()
    {
        //启动触发器
    }
}
