using UnityEngine;

[RequireComponent(typeof(DBInterface))]
public class DBManager : MonoBehaviour
{
    public static DBManager Instance;

    public static bool isLoggedIn;
    public static string playerName;

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        isLoggedIn = (PlayerPrefs.GetInt("IsLoggedIn", 0) == 1 ? true : false);
        playerName = PlayerPrefs.GetString("PlayerName", "Default_Player");
    }

    public static void Login(string userName) {
        isLoggedIn = true;
        PlayerPrefs.SetInt("IsLoggedIn", 1);

        playerName = userName;
        PlayerPrefs.SetString("PlayerName", userName);
    }

    public static void Logout() {
        isLoggedIn = false;
        PlayerPrefs.SetInt("IsLoggedIn", 0);

        playerName = "Default_Player";
        PlayerPrefs.SetString("PlayerName", "Default_Player");
    }

    public static void NewRun(string levelName, float time) {
        if(!DBInterface.ifTableExist(playerName)) {
            DBInterface.CreateTable(playerName);
        }

        if(!DBInterface.ifColumnExist(playerName, levelName)) {
            DBInterface.AddColumn(playerName, levelName, "FLOAT");
        }

        int k = (DBInterface.nullCounter(playerName, levelName)) + 1;
        int attempts = DBInterface.nullCounter(playerName, "attempt") + 1;

        if(attempts > k) {
            DBInterface.InsertValueInExistingAttempt(playerName, levelName, k, time);
        }
        else {
            DBInterface.InsertNewAttempt(playerName, levelName, attempts, time);
        }
    }

}