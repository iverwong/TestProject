using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Test : CardAbstract
{
    public override event EventHandler.RoleEventHandler ActionEvent;
    public override event EventHandler.FilterEventHandler FilterEvent;

    /// <summary>
    /// 实例化时初始化事件
    /// </summary>
    public Card_Test()
    {
        _cardName = "测试";
        _cardDescription = "测试描述";
        _cardBackstory = "测试背景";
        _cardType = CardType.Attack;
        _cardImage = Resources.Load<Sprite>("Textures/CardImage/test");
        _cardTrigger = new CardTrigger_Test(3);

        FilterEvent += CardFilters.RoleFilter;
        ActionEvent += CardActions.HP_Harm;
    }
    public override List<BaseInteractableObject> Action(List<BaseInteractableObject> _targets)
    {
        //目标筛选
        _targets = EventHandler.Filter(_targets, FilterEvent);
        //执行动作
        foreach (BaseInteractableObject each in _targets)
        {
            float result = EventHandler.Action(each, 40f, ActionEvent);//返回对该角色的最终伤害
        }
        //调用（待修改）
        return _targets;
    }

    public override CardAbstract Clone()
    {
        return new Card_Test();
    }

    public override void Draw()
    {
        throw new System.NotImplementedException();
    }

    public override void Drop()
    {
        throw new System.NotImplementedException();
    }

    public override void Expend()
    {
        throw new System.NotImplementedException();
    }

    public override void Keep()
    {
        throw new System.NotImplementedException();
    }

    public override void Use()
    {
        //触发卡牌特性（待补充）
        //触发卡牌目标触发器
        CardTrigger.Trigger();
    }
}
