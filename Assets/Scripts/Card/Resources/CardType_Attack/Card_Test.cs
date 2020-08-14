using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Test : CardAbstract
{
    private CardType _cardType = CardType.Attack;
    public override CardType CardType => _cardType;


    private Sprite _cardImage = Resources.Load<Sprite>("Textures/CardImage/test");
    public override Sprite CardImage => _cardImage;


    private string _cardName = "测试";
    public override string CardName => _cardName;


    private string _cardDescription = "测试描述";
    public override string CardDescription => _cardDescription;


    private string _cardBackstory = "测试背景故事";
    public override string CardBackstory => _cardBackstory;


    private CardTrigger_Test _cardTrigger = new CardTrigger_Test(3);
    public override CardTriggerAbstract CardTrigger => _cardTrigger;


    public override event EventHandler.RoleEventHandler ActionEvent;
    public override event EventHandler.FilterEventHandler FilterEvent;

    public override List<BaseInteractableObject> Action(List<BaseInteractableObject> _targets)
    {
        //增加HP伤害动作
        ActionEvent += CardActions.HP_Harm;
        foreach (BaseInteractableObject each in _targets)
        {
            float result = EventHandler.Action(each, 40f, ActionEvent);//返回对该角色的最终伤害
        }
        //调用（待修改）
        return _targets;//待修改
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
