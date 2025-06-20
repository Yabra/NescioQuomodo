using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int Money = 100;
    private int Reputation = 100;

    public int money { get => Money; }
    public int reputation { get => Reputation; }

    [Header("Pause")]
    [SerializeField] private GameObject pauseCanvas;

    [Header("Day Canvas Objects")]
    [SerializeField] private GameObject NewDayCanvas;
    [SerializeField] private TextMeshProUGUI NewDayCanvasText;
    [SerializeField] private GameObject DayCanvas;
    [SerializeField] private TextIndicator moneyIndicator;
    [SerializeField] private TextIndicator reputationIndicator;
    [SerializeField] private QuestVisualiser questVisualiser;

    [Header("Bills Canvas Objects")]
    [SerializeField] private GameObject BillsCanvas;
    [SerializeField] private BillsVisualiser billsVisualiser;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject demoEndCanvas;

    [Header("Day Data")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private Quest dayEndQuest;
    [SerializeField] private Quest currentQuest;
    [SerializeField] private QuestNode currentNode;
    [SerializeField] private Dictionary<int, List<Quest>> questsByDay = new Dictionary<int, List<Quest>>();

    [Header("Bills Data")]
    [SerializeField] private List<Bill> bills = new List<Bill>();

    private void Start()
    {
        if (SaveManager.loadSaveToGame)
        {
            Money = SaveManager.money;
            Reputation = SaveManager.reputation;

            bills = SaveManager.GetBills();

            currentDay = SaveManager.day;

            currentQuest = SaveManager.lastQuest;
            questsByDay = SaveManager.GetQuests();

            SaveManager.DeactivateGameLoading();
        }

        else
        {
            questsByDay[1] = new List<Quest>();
        }

        moneyIndicator.SetBaseValue(money);
        reputationIndicator.SetBaseValue(reputation);

        SetQuest(currentQuest);
        ShowNewDayCanvas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        }
    }

    private void SetQuest(Quest quest)
    {
        currentQuest = quest;
        currentNode = GetNodeById("0");
        questVisualiser.SetQuest(quest);
        questVisualiser.ShowQuestNode(currentNode);
    }

    private void SetNextQuest()
    {
        if (!questsByDay.ContainsKey(currentDay) || questsByDay[currentDay].Count == 0)
        {
            SetQuest(dayEndQuest);
        }

        else
        {
            SetQuest(questsByDay[currentDay][Random.Range(0, questsByDay[currentDay].Count)]);
        }

        SaveManager.Save(questsByDay, currentQuest, bills, currentDay, money, reputation);
    }

    public void SetNode(QuestNode node)
    {
        currentNode = node;
        questVisualiser.ShowQuestNode(currentNode);
    }

    public void RunVariant(int variantId)
    {
        var runnedVariant = currentNode.variants[variantId];

        foreach (var action in runnedVariant.actions)
        {
            action.DoAction();
        }

        if (runnedVariant.endNode)
        {
            if (questsByDay.ContainsKey(currentDay) && questsByDay[currentDay].Contains(currentQuest))
            {
                questsByDay[currentDay].Remove(currentQuest);
            }

            if (currentQuest != dayEndQuest)
            {
                SetNextQuest();
            }
        }

        else
        {
            if (runnedVariant.nextNode != null)
            {
                SetNode(runnedVariant.nextNode);
            }
        }
    }

    private QuestNode GetNodeById(string id)
    {
        var result = Resources.Load<QuestNode>($"Quests/{currentQuest.name}/nodes/{id}");

        if (result == null)
        {
            throw new System.Exception($"No node with name {id} in quest {currentQuest.name}");
        }

        return result;
    }

    public void StartDay()
    {
        BillsCanvas.SetActive(false);

        foreach (var bill in bills)
        {
            bill.DecTimeToPay();
            if (bill.timeToPay < 0)
            {
                gameOverCanvas.SetActive(true);
            }
        }

        DayCanvas.SetActive(true);

        currentDay++;

        if(currentDay >= 5)
        {
            demoEndCanvas.SetActive(true);
        }

        SetNextQuest();

        ShowNewDayCanvas();
    }

    public void EndDay()
    {
        DayCanvas.SetActive(false);
        BillsCanvas.SetActive(true);
        billsVisualiser.ShowBills(bills);
    }

    public void AddQuest(Quest quest, int questDay)
    {
        if (!questsByDay.ContainsKey(questDay))
        {
            questsByDay[questDay] = new List<Quest>();
        }

        questsByDay[questDay].Add(quest);
    }

    public void AddBill(Bill bill)
    {
        bills.Add(bill.Copy());
    }

    public void AddMoney(int moneyCount)
    {
        Money += moneyCount;
        moneyIndicator.AddValue(moneyCount);
    }

    public void AddReputatiion(int reputatiionCount)
    {
        Reputation += reputatiionCount;
        reputationIndicator.AddValue(reputatiionCount);
    }

    public bool PayBill(Bill bill)
    {
        if (bill.cost <= money)
        {
            AddMoney(-bill.cost);
            bills.Remove(bill);
            return true;
        }

        return false;
    }

    public void InMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void ShowNewDayCanvas()
    {
        NewDayCanvas.SetActive(true);
        NewDayCanvasText.text = "День " + currentDay.ToString();
    }
}
