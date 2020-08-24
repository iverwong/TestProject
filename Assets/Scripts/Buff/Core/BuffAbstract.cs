using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BUFF抽象类
/// </summary>
public abstract class BuffAbstract
{
    /// <summary>
    /// Buff种类
    /// </summary>
    public BuffType BuffType { get => _buffType; }
    public BuffType _buffType;

    /// <summary>
    /// Buff名称
    /// </summary>
    public string BuffName { get => _buffName; }
    public string _buffName;
    /// <summary>
    /// Buff描述
    /// </summary>
    public string BuffDescription { get => _buffDescription; }
    public string _buffDescription;
    /// <summary>
    /// Buff图片
    /// </summary>
    public Sprite BuffImage { get => _buffImage; }
    public Sprite _buffImage;
    /// <summary>
    /// Buff计数器，用于计算何时Buff失效，如为空则是常驻效果
    /// </summary>
    private int? _buffCount;
    /// <summary>
    /// Buff持有人，可以是游戏物体，也可以依附于Tile上
    /// </summary>
    private GameObject _owner;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="_count">Buff计数器</param>
    /// <param name="_object">Buff持有人</param>
    public BuffAbstract(GameObject _object,int? _count)
    {
        _buffCount = _count;
        _owner = _object;
    }

    /// <summary>
    /// 判断物体是否会受到该Buff影响
    /// </summary>
    /// <param name="_object">被判断的物体</param>
    /// <returns>返回是否影响的bool值</returns>
    public abstract bool IsImpact(BaseInteractableObject _object);
}
