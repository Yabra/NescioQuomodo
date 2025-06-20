using UnityEngine;

[CreateAssetMenu(fileName = "EndDay", menuName = "Quest/VariantActions/EndDayAction")]
public class EndDayAction : VariantAction
{
    public override void DoAction()
    {
        FindAnyObjectByType<GameController>().EndDay();
    }
}
