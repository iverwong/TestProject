using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡的抽象类
/// </summary>
public abstract class CardAbstract:ScriptableObject
{
    //构造函数
    public CardAbstract(CardType cardType, string name, string description, string textureName, List<CardAttribute> listCardAttribute,Vector3Int cost,CardGenericTrigger trigger)
    {
        CardType = cardType;
        CardName = name;
        CardDescription = description;
        CardImage = Resources.Load(textureName) as Texture;
        ListCardAttribute = listCardAttribute;
        HPCost = cost.x;
        SPCost = cost.y;
        MPCost = cost.z;
        CardTrigger = trigger;
    }
    //卡牌属性
    [Header("卡牌属性")]
    public CardType CardType;//卡牌种类
    public List<CardAttribute> ListCardAttribute;//卡牌特性列表
    public CardGenericTrigger CardTrigger;//卡牌触发器
    public Texture CardImage;//卡牌的图片
    public string CardName;//卡牌名称
    public string CardDescription;//卡牌描述
    //卡牌花费
    [Header("卡牌花费")]
    public int HPCost;//HP花费
    public int SPCost;//SP花费
    public int MPCost;//MP花费
    //卡牌方法
    /// <summary>
    /// 抽取卡牌（从牌库移至手牌时触发）
    /// </summary>
    public abstract void Draw();//抽取卡牌时

    public abstract void Use();//使用卡牌时
    /// <summary>
    /// 使用冻结指令时触发，保留该卡片至下一回合
    /// </summary>
    public abstract void Keep();//保留卡牌时
    /// <summary>
    /// 卡牌移动至弃牌堆时触发
    /// </summary>
    public abstract void Drop();//弃掉卡牌时
    /// <summary>
    /// 卡牌被删除时触发（非主动或被动弃牌，而是在该场战斗中无法再次使用）
    /// </summary>
    public abstract void Expend();//被消耗时
}
