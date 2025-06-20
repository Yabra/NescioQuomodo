using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject loadButton;
    [SerializeField] private GameObject deleteSaveButton;

    void Update()
    {
        loadButton.SetActive(SaveManager.hasSave);
        deleteSaveButton.SetActive(SaveManager.hasSave);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        SaveManager.ActivateGameLoading();
        NewGame();
    }

    public void DeleteSave()
    {
        SaveManager.DeleteSave();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
