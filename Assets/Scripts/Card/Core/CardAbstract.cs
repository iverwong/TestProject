using System.Collections.Generic;
using UnityEngine;
public abstract class CardAbstract
{
    //构造函数
    public CardAbstract(CardType cardType, string name, string description, string textureName, List<CardAttribute> listCardAttribute,Vector3Int cost)
    {
        CardType = cardType;
        CardName = name;
        CardDescription = description;
        CardImage = Resources.Load(textureName) as Texture;
        ListCardAttribute = listCardAttribute;
        HPCost = cost.x;
        SPCost = cost.y;
        MPCost = cost.z;
    }
    //卡牌属性
    public CardType CardType;//卡牌种类
    public List<CardAttribute> ListCardAttribute;//卡牌特性列表
    public Texture CardImage;//卡牌的图片
    public string CardName;//卡牌名称
    public string CardDescription;//卡牌描述
    //卡牌花费
    public int HPCost;//HP花费
    public int SPCost;//SP花费
    public int MPCost;//MP花费
    //卡牌方法
    public abstract void Draw();//抽取卡牌时
    public abstract void Use();//使用卡牌时
    public abstract void Keep();//保留卡牌时
    public abstract void Drop();//弃掉卡牌时
    public abstract void Expend();//被消耗时
}
