using UnityEngine;

[CreateAssetMenu(fileName = "AddReputation", menuName = "Quest/VariantActions/AddReputationAction")]
public class AddReputationAction : VariantAction
{
    [SerializeField] private int reputationCount;

    public override void DoAction()
    {
        FindAnyObjectByType<GameController>().AddReputatiion(reputationCount);
    }
}
