using UnityEngine;

[CreateAssetMenu(fileName = "Bill", menuName = "Quest/Bill")]
public class Bill : ScriptableObject
{
    [SerializeField] private string Text;
    [SerializeField] private int Cost;
    [SerializeField] private int TimeToPay;

    public string text { get => Text; }
    public int cost { get => Cost; }
    public int timeToPay { get => TimeToPay; }

    public Bill(string text, int cost, int timeToPay)
    {
        Text = text;
        Cost = cost;
        TimeToPay = timeToPay;
    }

    public Bill Copy()
    {
        return new Bill(text, cost, timeToPay);
    }

    public void DecTimeToPay()
    {
        TimeToPay--;
    }
}
