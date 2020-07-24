using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_TestAttack : CardAbstract
{
    public Card_TestAttack(string name, string description, string textureName, List<CardAttribute> listCardAttribute, Vector3Int cost) : base(CardType.Attack, name, description, textureName, listCardAttribute, cost)
    {

    }

    public override void Draw()
    {
        Debug.Log("这张牌被抽到了！");
    }

    public override void Drop()
    {
        Debug.Log("这张牌被弃掉了！");
    }

    public override void Expend()
    {
        Debug.Log("这张牌被消耗了！");
    }

    public override void Keep()
    {
        Debug.Log("这张牌被保留了");
    }

    public override void Use()
    {
        Debug.Log("这张牌被用掉了");
    }
}
