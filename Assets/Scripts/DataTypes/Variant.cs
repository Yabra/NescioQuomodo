using System;
using UnityEngine;


[Serializable]
public class Variant
{
    [SerializeField] [TextArea(2, 15)] private string Text;
    [SerializeField] private bool EndNode;
    [SerializeField] private QuestNode NextNode;
    [SerializeField] private VariantAction[] Actions;

    public string text { get => Text; }
    public bool endNode { get => EndNode; }
    public QuestNode nextNode { get => NextNode; }
    public VariantAction[] actions { get => Actions; }
}
