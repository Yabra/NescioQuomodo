using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class QuestVisualiser : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterNameText;

    [SerializeField] private TextMeshProUGUI phraseText;
    [SerializeField] private Transform answerButtonsParent;
    [SerializeField] private GameObject answerButtonPrefab;

    private Quest currentQuest;

    public void SetQuest(Quest quest)
    {
        currentQuest = quest;
    }

    public void ShowQuestNode(QuestNode node)
    {
        ShowCharacter(node.anotherCharacter == null ? currentQuest.character : node.anotherCharacter);

        phraseText.text = node.text;
        foreach (Transform t in answerButtonsParent)
        {
            Destroy(t.gameObject);
        }

        for (var i = 0; i < node.variants.Length; i++)
        {
            var v = node.variants[i];
            GameObject newButton = Instantiate(answerButtonPrefab, answerButtonsParent);
            newButton.GetComponent<AnswerButton>().Init(i, v.text);
        }
    }
    private void ShowCharacter(Character character)
    {
        if (character == null)
        {
            characterImage.sprite = null;
            characterNameText.text = "";
            characterNameText.color = Color.black;
            return;
        }

        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;
        characterNameText.color = character.characterNameColor;
    }
}
