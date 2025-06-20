using System;
using UnityEngine;

[Serializable]
public abstract class VariantAction : ScriptableObject
{
    public virtual void DoAction() { }
}