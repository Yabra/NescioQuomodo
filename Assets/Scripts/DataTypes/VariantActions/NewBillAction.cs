using UnityEngine;

[CreateAssetMenu(fileName = "NewBill", menuName = "Quest/VariantActions/NewBillAction")]
public class NewBillAction : VariantAction
{
    [SerializeField] private Bill bill;

    public override void DoAction()
    {
        FindAnyObjectByType<GameController>().AddBill(bill);
    }
}
