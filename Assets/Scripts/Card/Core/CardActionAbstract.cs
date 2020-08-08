using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardActionAbstract
{
    /// <summary>
    /// 触发该卡牌动作，可以进行筛选或完成动作
    /// </summary>
    /// <returns></returns>
    public abstract List<BaseInteractableObject> Action(List<BaseInteractableObject> _objects);
}
