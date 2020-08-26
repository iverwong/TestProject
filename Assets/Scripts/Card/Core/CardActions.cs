using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardActions
{

    /// <summary>
    /// 对角色造成HP伤害
    /// </summary>
    /// <param name="_object">目标对象</param>
    /// <param name="_value">面板值</param>
    /// <param name="_param">进攻方属性、防守方属性、进攻方副属性、防守方副属性</param>
    /// <returns>经过伤害系统计算后返回结果</returns>
    public static float HP_Harm(BaseInteractableObject _object, float _value, string[] _param)
    {
        //计算双方系数
        _value = OffensiveFactor(StateMachine.waitCommand, _value);
        _value = DefensiveFactor(_object, _value);
        //计算属性加权
        _value = AttrCalculate(StateMachine.waitCommand, _object, _value, _param);
        //计算双方角色BUFF
        //进攻方角色
        foreach (RoleBuffAbstract each in StateMachine.waitCommand.Buff)
        {
            if (each is IBuffHarm && each.IsImpact(StateMachine.waitCommand))
            {
                IBuffHarm buff = (IBuffHarm)each;
                _value = buff.Calculate(_value);
            }
            else continue;
        }
        //防守方角色
        foreach (RoleBuffAbstract each in _object.Buff)
        {
            if (each is IBuffHarm && each.IsImpact(_object))
            {
                IBuffHarm buff = (IBuffHarm)each;
                _value = buff.Calculate(_value);
            }
            else continue;
        }
        //计算双方地块BUFF
        //进攻方地块
        foreach (TileBuffAbstract each in StateMachine.waitCommand.standOn.Buff)
        {
            if (each is IBuffHarm && each.IsImpact(StateMachine.waitCommand))
            {
                IBuffHarm buff = (IBuffHarm)each;
                _value = buff.Calculate(_value);
            }
            else continue;
        }
        //防守方地块
        foreach(TileBuffAbstract each in _object.ObjectCoordinate.FindTile().Buff)
        {
            if (each is IBuffHarm && each.IsImpact(_object))
            {
                IBuffHarm buff = (IBuffHarm)each;
                _value = buff.Calculate(_value);
            }
            else continue;
        }
        //检验防御模式
        if(_object is BattleArea_Object_Camp_Role)
        {
            BattleArea_Object_Camp_Role role = (BattleArea_Object_Camp_Role)_object;
            //如果该角色激活了防御模式，且该防御模式继承了HPHarm接口
            if(role.defenseMode != null && role.defenseMode is IDefenseModeHPHarm)
            {
                IDefenseModeHPHarm IdefenseMode = (IDefenseModeHPHarm)role.defenseMode;
                //伤害结算
                IdefenseMode.Settle(_value);
            }
            else//如果没有激活防御模式
            {
                role.HP = -_value;
            }
        }
        //返回造成的最终伤害（未经过防御模式，用于UI展现）
        return _value;
    }
    /// <summary>
    /// 计算攻击方系数
    /// </summary>
    /// <param name="_object">计算方</param>
    /// <param name="_value">计算前值</param>
    /// <returns>计算结果</returns>
    private static float OffensiveFactor(BaseInteractableObject _object, float _value)
    {
        BattleArea_Object_Camp_Role role = (BattleArea_Object_Camp_Role)_object;
        float result = role.OFF * _value;
        return result;
    }
    /// <summary>
    /// 计算防守方系数，如果防守方不为Role类，则防守方系数为1（即返回原值）
    /// </summary>
    /// <param name="_object">计算方</param>
    /// <param name="_value">计算前值</param>
    /// <returns>计算结果</returns>
    private static float DefensiveFactor(BaseInteractableObject _object, float _value)
    {
        if(_object is BattleArea_Object_Camp_Role)
        {
            BattleArea_Object_Camp_Role role = (BattleArea_Object_Camp_Role)_object;
            float result = _value / role.DFF;
            return result;
        }
        else
        {
            return _value;
        }

    }
    /// <summary>
    /// 计算双方属性加权
    /// </summary>
    /// <param name="_oObj">进攻方</param>
    /// <param name="_dObj">防守方</param>
    /// <param name="_value">计算前值</param>
    /// <param name="_param">属性参数，数组为2时，第一个属性为进攻方属性，第二个属性为防守方属性；数组为4时，第1、3各属性为进攻方属性，第2、4为防守方属性，其中权重为6:4</param>
    /// <returns>返回计算后的结果，如属性异常或不存在属性，则返回原值</returns>
    private static float AttrCalculate(BaseInteractableObject _oObj, BaseInteractableObject _dObj, float _value, string[] _param)
    {
        //参数为空时，直接返回原值
        if (_param == null)
        {
            return _value;
        }
        else
        {
            //确定双方类型
            bool o_role = _oObj.GetType().IsInstanceOfType(typeof(BattleArea_Object_Camp_Role));
            bool d_role = _dObj.GetType().IsInstanceOfType(typeof(BattleArea_Object_Camp_Role));
            //当双方都为Camp_Role时，进行属性加成，否则返回原值
            if (o_role && d_role)
            {
                float value1, value2, value3, value4;
                //根据给出的参数数量，参数数量不正确返回原值
                switch (_param.Length)
                {
                    case 2:
                        value1 = float.Parse(typeof(BattleArea_Object_Camp_Role).GetField(_param[0]).GetValue(_oObj).ToString());
                        value2 = float.Parse(typeof(BattleArea_Object_Camp_Role).GetField(_param[1]).GetValue(_dObj).ToString());
                        _value = _value * value1 / value2;
                        return _value;
                    case 4:
                        value1 = float.Parse(typeof(BattleArea_Object_Camp_Role).GetField(_param[0]).GetValue(_oObj).ToString());
                        value2 = float.Parse(typeof(BattleArea_Object_Camp_Role).GetField(_param[1]).GetValue(_dObj).ToString());
                        value3 = float.Parse(typeof(BattleArea_Object_Camp_Role).GetField(_param[2]).GetValue(_oObj).ToString());
                        value4 = float.Parse(typeof(BattleArea_Object_Camp_Role).GetField(_param[3]).GetValue(_dObj).ToString());
                        _value = _value * (value1 * 0.6f + value3 * 0.4f) / (value2 * 0.6f + value4 * 0.4f);
                        return _value;
                    default:
                        return _value;
                }
            }
            else
            {
                return _value;
            }

        }
    }
}
