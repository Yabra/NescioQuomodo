using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static bool exists;

    public static bool loadSaveToGame;
    public static bool hasSave;

    private static Quest LastQuest;
    private static int Day;
    private static int Money;
    private static int Reputation;

    public static Quest lastQuest { get => LastQuest; }
    public static int day { get => Day; }
    public static int money { get => Money; }
    public static int reputation { get => Reputation; }

    void Start()
    {
        if (exists)
        {
            Destroy(this.gameObject);
            return;
        }

        exists = true;
        DontDestroyOnLoad(this.gameObject);

        LoadSave();
    }

    private static void LoadSave()
    {
        hasSave = PlayerPrefs.GetInt("hasSave", 0) == 0 ? false : true;

        if (hasSave)
        {
            LastQuest = GetQuestById(PlayerPrefs.GetString("lastQuest"));
            Day = PlayerPrefs.GetInt("day");
            Money = PlayerPrefs.GetInt("money");
            Reputation = PlayerPrefs.GetInt("reputation");
        }
    }

    public static void Save(
        Dictionary<int, List<Quest>> questsByDay,
        Quest lastQuest,
        List<Bill> bills,
        int day, int money, int reputation)
    {
        SaveManager.hasSave = true;
        PlayerPrefs.SetInt("hasSave", 1);

        SaveManager.LastQuest = lastQuest;
        PlayerPrefs.SetString("lastQuest", lastQuest.name);

        PlayerPrefs.SetString("questsByDay", QuestsToString(questsByDay));

        PlayerPrefs.SetString("bills", BillsToString(bills));

        SaveManager.Day = day;
        PlayerPrefs.SetInt("day", day);

        SaveManager.Money = money;
        PlayerPrefs.SetInt("money", money);

        SaveManager.Reputation = reputation;
        PlayerPrefs.SetInt("reputation", reputation);

        PlayerPrefs.Save();
    }

    private static string QuestsToString(Dictionary<int, List<Quest>> questsByDay)
    {
        var result = new StringBuilder();

        foreach (var dayQuests in questsByDay)
        {
            result.Append(dayQuests.Key);
            result.Append(':');
            foreach (var quest in dayQuests.Value)
            {
                result.Append(quest.name);
                result.Append(',');
            }

            result.Append(';');
        }

        return result.ToString();
    }

    public static Dictionary<int, List<Quest>> GetQuests()
    {
        var result = new Dictionary<int, List<Quest>>();
        var saved = PlayerPrefs.GetString("questsByDay", "");

        foreach (var daySection in saved.Split(";"))
        {
            if (daySection == "")
            {
                continue;
            }

            var dayNum = int.Parse(daySection.Substring(0, 1));
            result[dayNum] = new List<Quest>();
            foreach (var questName in daySection.Substring(2).Split(","))
            {
                if (questName == "")
                {
                    continue;
                }

                result[dayNum].Add(GetQuestById(questName));
            }
        }

        return result;
    }

    private static string BillsToString(List<Bill> bills)
    {
        var result = new StringBuilder();
        foreach (var bill in bills)
        {
            result.Append(bill.text);
            result.Append(':');
            result.Append(bill.cost);
            result.Append(':');
            result.Append(bill.timeToPay);
            result.Append(';');
        }

        return result.ToString();
    }

    public static List<Bill> GetBills()
    {
        var result = new List<Bill>();
        var saved = PlayerPrefs.GetString("bills", "");

        foreach (var billSection in saved.Split(";"))
        {
            if (billSection == "")
            {
                continue;
            }

            var billSectionSplited = billSection.Split(":");
            result.Add(
                new Bill(
                    billSectionSplited[0],
                    int.Parse(billSectionSplited[1]),
                    int.Parse(billSectionSplited[2])
                    )
                );
        }

        return result;
    }

    private static Quest GetQuestById(string id)
    {
        var result = Resources.Load<Quest>($"Quests/{id}/{id}");

        if(result == null)
        {
            throw new System.Exception($"No quest with name {id}");
        }

        return result;
    }

    public static void DeleteSave()
    {
        hasSave = false;
        PlayerPrefs.SetInt("hasSave", 0);
        PlayerPrefs.Save();
    }

    public static void ActivateGameLoading()
    {
        loadSaveToGame = true;
    }

    public static void DeactivateGameLoading()
    {
        loadSaveToGame = false;
    }
}
