using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI usernameInput;
    [SerializeField]
    private TextMeshProUGUI playerName;

    [SerializeField]
    private GameObject newUserUI;
    [SerializeField]
    private GameObject mainUI;

    private string userName;

    void Start() {
        newUserUI.SetActive(!DBManager.isLoggedIn);
        mainUI.SetActive(DBManager.isLoggedIn);

        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public void RegisterUser() {
        userName = usernameInput.text;

        DBManager.Login(userName);

        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LogoutUser() {
        DBManager.Logout();
        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public void QuitGame() {
        PlayerPrefs.Save();
        Application.Quit();
    }
}