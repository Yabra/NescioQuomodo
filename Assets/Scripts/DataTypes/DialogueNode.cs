using UnityEngine;

[CreateAssetMenu(fileName = "QuestNode", menuName = "Quest/QuestNode")]
public class QuestNode : ScriptableObject
{
    [SerializeField] [TextArea(2, 15)] private string Text;
    [SerializeField] private Variant[] Variants;
    [SerializeField] private Character AnotherCharacter;

    public string text { get => Text; }
    public Variant[] variants { get => Variants; }
    public Character anotherCharacter { get => AnotherCharacter; }
}
