﻿using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 卡牌的抽象类
/// </summary>
public abstract class CardAbstract
{
    /// <summary>
    /// 卡牌种类
    /// </summary>
    public CardType CardType { get => _cardType; }
    public CardType _cardType;
    /// <summary>
    /// 卡牌图片
    /// </summary>
    public Sprite CardImage { get => _cardImage; }
    public Sprite _cardImage;
    /// <summary>
    /// 卡牌名称
    /// </summary>
    public string CardName { get => _cardName; }
    public string _cardName;
    /// <summary>
    /// 卡牌描述
    /// </summary>
    public string CardDescription { get => _cardDescription; }
    public string _cardDescription;
    /// <summary>
    /// 卡牌背景故事
    /// </summary>
    public string CardBackstory { get => _cardBackstory; }
    public string _cardBackstory;
    /// <summary>
    /// 卡牌目标触发器
    /// </summary>
    public CardTriggerAbstract CardTrigger { get => _cardTrigger; }
    public CardTriggerAbstract _cardTrigger;
    /// <summary>
    /// 抽取卡牌时触发
    /// </summary>
    public abstract void Draw();
    /// <summary>
    /// 点击卡牌UI时触发
    /// </summary>
    public abstract void Use();
    /// <summary>
    /// 选择保留该张卡牌时触发
    /// </summary>
    public abstract void Keep();
    /// <summary>
    /// 卡牌进入弃牌堆时触发
    /// </summary>
    public abstract void Drop();
    /// <summary>
    /// 卡牌被消耗时触发
    /// </summary>
    public abstract void Expend();
    /// <summary>
    /// 选择目标对象完成后触发的动作列表
    /// </summary>
    /// <param name="_targets">目标触发器收到的目标对象</param>
    /// <returns>完成动作后受影响的目标对象</returns>
    public abstract List<BaseInteractableObject> Action(List<BaseInteractableObject> _targets);
    /// <summary>
    /// 返回一个该卡牌的复制，使用new关键字进行新的实例构造
    /// </summary>
    /// <returns>使用相同参数构造卡牌</returns>
    public abstract CardAbstract Clone();

    /// <summary>
    /// 选择目标对象完成后触发的动作事件
    /// </summary>
    public abstract event EventHandler.RoleEventHandler ActionEvent;
    /// <summary>
    /// 对目标触发器返回的目标清单进行筛选的事件
    /// </summary>
    public abstract event EventHandler.FilterEventHandler FilterEvent;
}