using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 卡牌动作
/// </summary>

[CreateAssetMenu(fileName = "NewAction", menuName = "MyMenu/Action")]
public class CardAction : ScriptableObject
{
    public enum ActionType
    {
        GROUP,//分组
        FILTER,//过滤器
        ACTION,//动作
    }

    public enum ActionClass
    {
        //单体直接伤害
        HARM_SINGLE,

    }

    public enum FilterClass
    {
        //无筛选
        NONE,
        //筛选Role类
        ROLE_ONLY,
    }

    public List<BaseInteractableObject> Filter()
    {
        switch (filterClass)
        {
            case FilterClass.NONE:
                return StateMachine.listRole;
            case FilterClass.ROLE_ONLY:
                List<BaseInteractableObject> newList = null;
                foreach (BaseInteractableObject each in StateMachine.listRole)
                {
                    if(each.GetType() == typeof(BattleArea_Object_Camp_Role))
                    {
                        newList.Add(each);
                    }
                }
                return newList;
            default:
                return StateMachine.listRole;
        }
    }
    public void Action()
    {
        //判断listAfterTrigger是否为空
        if(StateMachine.listAfterTrigger.Count == 0)
        {
            Debug.Log("未包含任何目标或filter触发器未设置");
            return;
        }
        switch (actionClass)
        {
            case ActionClass.HARM_SINGLE:
                if(StateMachine.listRole[0].GetType() == typeof(BattleArea_Object_Camp_Role))
                {
                    BattleArea_Object_Camp_Role target = (BattleArea_Object_Camp_Role)StateMachine.listAfterTrigger[0];
                    target.HP -= param[0];
                    target.SP -= param[1];
                    target.MP -= param[2];
                }
                break;
        }
    }

    [Header("动作分类")]
    public ActionType actionType;
    [Header("动作类型")]
    public ActionClass actionClass;
    [Header("筛选器类型")]
    public FilterClass filterClass;
    [Header("动作参数")]
    public int[] param;


}
