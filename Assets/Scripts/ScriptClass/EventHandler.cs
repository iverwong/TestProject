using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义委托类
/// </summary>
public static class EventHandler
{
    /// <summary>
    /// 角色委托类，对角色产生影响，用于伤害计算系统
    /// </summary>
    /// <param name="_interactableObject">触发响应的角色，用于获取角色的基本信息及属性</param>
    /// <param name="_value">触发响应的值，计算前的基础值</param>
    /// <returns>触发后返回计算后的值</returns>
    public delegate float RoleEventHandler(BaseInteractableObject _interactableObject, float _value,string[] _param = null);
    /// <summary>
    /// 过滤器委托类，对目标列表产生影响，用于过滤掉无法造成影响的目标
    /// </summary>
    /// <param name="_objectList">从目标触发器获得的目标清单</param>
    /// <returns>过滤后返回的目标清单</returns>
    public delegate List<BaseInteractableObject> FilterEventHandler(List<BaseInteractableObject> _objectList,string[] _param = null);
    /// <summary>
    /// 对目标（单个）产生一个或多个影响，通过事件进行多播，带入初始值并进行计算
    /// </summary>
    /// <param name="_interactableObject">目标</param>
    /// <param name="_value">初始值</param>
    /// <param name="_event">事件</param>
    /// <returns>经过所有动作影响后的返回值</returns>
    public static float Action(BaseInteractableObject _interactableObject,float _value, RoleEventHandler _event,string[] _param = null)
    {
        if(_event != null)
        {
            //获取委托链表
            System.Delegate[] delegates = _event.GetInvocationList();
            foreach(System.Delegate each in delegates)
            {
                //将委托链表中的每一项强制转换为RoleEventHandler
                RoleEventHandler handler = (RoleEventHandler)each;
                //将_value的值更改为运行委托后的值
                _value = handler(_interactableObject, _value,_param);
            }
            return _value;//返回最终值
        }
        return _value;//如事件为空，直接返回原值
    }
    /// <summary>
    /// 对由目标触发器返回的目标列表进行一次或多次筛选，返回符合条件的目标列表
    /// </summary>
    /// <param name="_objectList">传入的目标列表</param>
    /// <param name="_event">事件</param>
    /// <returns>经过所有筛选器影响后的目标列表</returns>
    public static List<BaseInteractableObject> Filter(List<BaseInteractableObject> _objectList, FilterEventHandler _event,string[] _param = null)
    {
        if (_event != null)
        {
            //获取委托链表
            System.Delegate[] delegates = _event.GetInvocationList();
            foreach (System.Delegate each in delegates)
            {
                //将委托链表中的每一项强制转换为FilterEventHandler
                FilterEventHandler handler = (FilterEventHandler)each;
                //将_value的值更改为运行委托后的值
                _objectList = handler(_objectList,_param);
            }
            return _objectList;//返回最终值
        }
        return _objectList;//如事件为空，直接返回原值
    }
}
