using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Buff测试类，对进攻方进行一定伤害的加成
/// </summary>
public class Buff_test : RoleBuffAbstract,IBuffHarm
{
    float Upper;
    /// <summary>
    /// Buff测试类，对进攻方进行一定伤害的加成
    /// </summary>
    /// <param name="_object">Buff持有者</param>
    /// <param name="_count">Buff计数器</param>
    /// <param name="_upper">提升幅度，如10%即为0.1f</param>
    public Buff_test(GameObject _object, int? _count,float _upper) : base(_object, _count)
    {
        Upper = _upper;
        _buffName = "测试Buff";
        _buffType = BuffType.BUFF;
        _buffDescription = "测试Buff描述";
        _buffImage = Resources.Load<Sprite>("Textures/CardImage/test");
    }

    public float Calculate(float _value)
    {

        return _value * (1+ Upper);
    }

    public override bool IsImpact(BaseInteractableObject _object)
    {
        //仅对进攻方生效
        if (StateMachine.waitCommand == _object)
        {
            return true;
        }
        else return false;
    }
}
