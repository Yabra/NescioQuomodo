using TMPro;
using UnityEngine;

public class BillButton : MonoBehaviour
{
    [SerializeField] private Bill bill;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI timeText;

    public void Init(Bill bill)
    {
        this.bill = bill;
        mainText.text = bill.text;
        costText.text = bill.cost.ToString();
        timeText.text = bill.timeToPay.ToString();
    }

    public void PayBill()
    {
        if (FindObjectOfType<GameController>().PayBill(bill))
        {
            Destroy(this.gameObject);
        }
    }
}
