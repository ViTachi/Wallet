using System;
using UnityEngine;

public interface IOperation
{
    public string AccountId { get; }
    public DateTime DateTime { get; }
    public Sprite Icon { get; }
    public string Name { get; }
    public float Value { get; }
}
