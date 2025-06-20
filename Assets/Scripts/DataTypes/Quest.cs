using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private Character Character;

    public Character character { get => Character; }
}
