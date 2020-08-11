using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Test : CardAbstract
{
    public override CardType CardType => CardType.Attack;

    public override Sprite CardImage => Resources.Load<Sprite>("Textures/CardImage/test");

    public override string CardName => "测试";

    public override string CardDescription => "测试描述";

    public override string CardBackstory => "测试背景故事";

    public override CardTriggerAbstract CardTrigger => new CardTrigger_Test(3);

    public override List<BaseInteractableObject> Action(List<BaseInteractableObject> _targets)
    {
        CardAction_Test hurt = new CardAction_Test(20);
        List<BaseInteractableObject> results =  hurt.Action(_targets);
        return results;
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
