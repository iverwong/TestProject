using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一个防御模式测试类，将受到的伤害以200%的代价用SP进行抵抗
/// </summary>
public class DefenseMode_Test : DefenseModeAbstract, IDefenseModeHPHarm
{
    public DefenseMode_Test(BattleArea_Object_Camp_Role _owner) : base(_owner)
    {
    }

    /// <summary>
    /// 伤害结算
    /// </summary>
    /// <param name="_value">最终伤害值</param>
    public void Settle(float _value)
    {
        float SP = owner.SP;
        //如果SP不能完全抵挡
        if(_value * 2 > SP)
        {
            owner.HP = -(_value - SP / 2);
            owner.SP = -SP;
        }
        else
        {
            owner.SP = -2 * _value;
        }
    }
}
