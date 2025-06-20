using System.Collections.Generic;
using UnityEngine;

public class BillsVisualiser : MonoBehaviour
{
    [SerializeField] private Transform billButtonsParent;
    [SerializeField] private GameObject billButtonPrefab;

    public void ShowBills(List<Bill> bills)
    {
        foreach (Transform t in billButtonsParent)
        {
            Destroy(t.gameObject);
        }

        for (var i = 0; i < bills.Count; i++)
        {
            var bill = bills[i];
            GameObject newButton = Instantiate(billButtonPrefab, billButtonsParent);
            newButton.GetComponent<BillButton>().Init(bill);
        }
    }
}
