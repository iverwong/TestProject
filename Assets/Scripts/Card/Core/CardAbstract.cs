using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class CardAbstract
{
    public abstract CardType CardType { get; }
    public abstract Sprite CardImage { get; }
    public abstract string CardName { get; }
    public abstract string CardDescription { get; }
    public abstract string CardBackstory { get; }
    public abstract CardTriggerAbstract CardTrigger { get; }

    public abstract void Draw();
    public abstract void Use();
    public abstract void Keep();
    public abstract void Drop();
    public abstract void Expend();
    public abstract List<BaseInteractableObject> Action(List<BaseInteractableObject> _targets);
    public abstract CardAbstract Clone();
}