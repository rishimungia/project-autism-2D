using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public InputField PlayerName;
    public InputField Highscore;
    public List<Text> PlayerNames = new List<Text>();
    public List<Text> Highscores = new List<Text>();
    DBInterface DBInterface;
    // Start is called before the first frame update
    void Start()
    {
        DBInterface = FindObjectOfType<DBInterface>();
    }
    public void InsertHighscore()
    {
        if (DBInterface == null)
        {
            Debug.LogError("UserInterface: Could not insert a highscore. DBIitefrace is not present.");
            return;
        }
        if (PlayerName == null || Highscore == null)
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName or Highscore is not set.");
            return;
        }
        if (string.IsNullOrEmpty(PlayerName.text) || string.IsNullOrWhiteSpace(PlayerName.text))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName is empty.");
            return;
        }
        int highscore;
        if (!System.Int32.TryParse(Highscore.text, out highscore))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. Highscore is not an integer.");
            return;
        }
        bool isTable = DBInterface.ifTableExist(PlayerName.text);
        if(!isTable)
        {
            DBInterface.CreateTable(PlayerName.text);
        }
        string LevelName = "Level1";
        if(!DBInterface.ifColumnExist(PlayerName.text, LevelName))
        {
            DBInterface.AddColumn(PlayerName.text, LevelName, "INT");
        }
        int k = (DBInterface.nullCounter(PlayerName.text, LevelName))+1;
        int attempts = DBInterface.nullCounter(PlayerName.text, "attempt")+1;
        if(attempts>k)
        {
            DBInterface.InsertValueInExistingAttempt(PlayerName.text, LevelName, k, 100);
        }
        else
        {
            DBInterface.InsertNewAttempt(PlayerName.text, LevelName, attempts, 200);
        }
        PlayerName.text = "";
        Highscore.text = "";
    }
    public void RetrieveTopFiveHighscores()
    {
        if (DBInterface == null)
        {
            Debug.LogError("UserInterface: Could not retrieve the top five highscores. DBIitefrace is not present.");
            return;
        }
        if (PlayerNames.Count < 5 || Highscores.Count < 5)
        {
            Debug.LogError("UserInterface: Could not retrieve the top five highscores. Not all PlayerName labels or Highscore labels are present.");
            return;
        }
        clearScoreboard();
    }
    private void clearScoreboard()
    {
        foreach (Text playername in PlayerNames)
        {
            playername.text = "";
        }
        foreach (Text highscore in Highscores)
        {
            highscore.text = "";
        }
    }
}
