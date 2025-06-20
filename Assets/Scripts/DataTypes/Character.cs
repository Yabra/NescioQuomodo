using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Quest/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private string CharacterName;
    [SerializeField] private Color CharacterNameColor;
    [SerializeField] private Sprite CharacterSprite;

    public string characterName { get => CharacterName; }
    public Color characterNameColor { get => CharacterNameColor; }
    public Sprite characterSprite { get => CharacterSprite; }
}
