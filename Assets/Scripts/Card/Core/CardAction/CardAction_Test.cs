using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HP伤害
/// </summary>
public class CardAction_Test : CardActionAbstract
{
    public int HP;
    public CardAction_Test(int _HP)
    {
        HP = _HP;
    }
    public override List<BaseInteractableObject> Action(List<BaseInteractableObject> _objects)
    {
        BattleArea_Object_Camp_Role target = (BattleArea_Object_Camp_Role)_objects[0];
        target.HP -= HP;
        return _objects;
    }
}
