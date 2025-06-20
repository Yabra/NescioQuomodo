using UnityEngine;

[CreateAssetMenu(fileName = "AddMoney", menuName = "Quest/VariantActions/AddMoneyAction")]
public class AddMoneyAction : VariantAction
{
    [SerializeField] private int moneyCount;

    public override void DoAction()
    {
        FindAnyObjectByType<GameController>().AddMoney(moneyCount);
    }
}
