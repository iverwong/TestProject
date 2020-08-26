using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御模式HP伤害接口
/// </summary>
interface IDefenseModeHPHarm
{
    /// <summary>
    /// 伤害结算
    /// </summary>
    /// <param name="_value">最终伤害</param>
    void Settle(float _value);
}
