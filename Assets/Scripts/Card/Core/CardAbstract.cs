using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡的抽象类
/// </summary>

[CreateAssetMenu(fileName = "New Card", menuName = "MyMenu/NewCard")]
public class CardAbstract:ScriptableObject
{
    //构造函数
    public CardAbstract(CardType cardType, string name, string description,string backstory, string textureName,Vector3Int cost, List<CardAttribute> listCardAttribute,CardTargetTrigger trigger,List<CardAction> cardActions)
    {
        CardType = cardType;
        CardName = name;
        CardDescription = description;
        CardBackstory = backstory;
        CardImage = Resources.Load(textureName) as Texture;
        ListCardAttribute = listCardAttribute;
        HPCost = cost.x;
        SPCost = cost.y;
        MPCost = cost.z;
        CardTrigger = trigger;
        ListCardAction = cardActions;
    }
    //卡牌属性
    [Header("卡牌属性")]
    public CardType CardType;//卡牌种类
    public Texture CardImage;//卡牌的图片
    public string CardName;//卡牌名称
    public string CardDescription;//卡牌描述
    public string CardBackstory;//卡牌背景故事
    //卡牌花费
    [Header("卡牌花费")]
    public int HPCost;//HP花费
    public int SPCost;//SP花费
    public int MPCost;//MP花费
    //卡牌方法
    [Header("卡牌方法")]
    public List<CardAttribute> ListCardAttribute;//卡牌特性列表
    public CardTargetTrigger CardTrigger;//目标触发器
    public List<CardAction> ListCardAction;//卡牌动作组
    /// <summary>
    /// 抽取卡牌（从牌库移至手牌时触发）
    /// </summary>
    public void Draw(){ }
    /// <summary>
    /// 确认使用卡牌时触发
    /// </summary>
    public void Use()
    {
        //触发卡牌特性（待补充）
        //触发卡牌目标触发器
        StateMachine.currentCard = this;
        CardTrigger.Trigger();
    }
    /// <summary>
    /// 使用冻结指令时触发，保留该卡片至下一回合
    /// </summary>
    public void Keep() { }
    /// <summary>
    /// 卡牌移动至弃牌堆时触发
    /// </summary>
    public void Drop() { }
    /// <summary>
    /// 卡牌被删除时触发（非主动或被动弃牌，而是在该场战斗中无法再次使用）
    /// </summary>
    public void Expend() { }
    /// <summary>
    /// 触发action列表，选中目标后调用
    /// </summary>
    public void Action()
    {
        //禁止操作
        StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.NONE;
        foreach(CardAction each in ListCardAction)
        {
            switch (each.actionType)
            {
                case CardAction.ActionType.FILTER:
                    StateMachine.listAfterTrigger = each.Filter();
                    break;
                case CardAction.ActionType.ACTION:
                    each.Action();
                    break;
                case CardAction.ActionType.GROUP:
                    //清空listAfterTrigger
                    StateMachine.listAfterTrigger.Clear();
                    //
                    break;
            }
            //触发动画（待编辑）
        }
        //恢复操作
        StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.COMMANDER;
    }
}
