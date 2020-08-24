using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 伤害修正类Buff，用于在伤害计算的过程中影响计算结果
/// </summary>
public interface IBuffHarm
{
    /// <summary>
    /// 计算buff修正结果
    /// </summary>
    /// <param name="_value">计算前数值</param>
    /// <returns>计算后数值</returns>
    public float Calculate(float _value);
}
