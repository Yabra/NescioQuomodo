using TMPro;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private int variantId;

    public void Init(int variantId, string text)
    {
        this.variantId = variantId;
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void RunVariant()
    {
        FindObjectOfType<GameController>().RunVariant(variantId);
    }
}
