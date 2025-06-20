using UnityEngine;

[CreateAssetMenu(fileName = "RandomNextNode", menuName = "Quest/VariantActions/RandomNextNodeAction")]
public class RandomNextNodeAction : VariantAction
{
    [SerializeField] private QuestNode[] nodes;

    public override void DoAction()
    {
        FindAnyObjectByType<GameController>().SetNode(nodes[Random.Range(0, nodes.Length)]);
    }
}
