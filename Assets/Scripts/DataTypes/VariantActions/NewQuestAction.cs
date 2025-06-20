using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/VariantActions/NewQuestAction")]
public class NewQuestAction : VariantAction
{
    [SerializeField] private Quest quest;
    [SerializeField] private int questDay;

    public override void DoAction()
    {
        FindAnyObjectByType<GameController>().AddQuest(quest, questDay);
    }
}
