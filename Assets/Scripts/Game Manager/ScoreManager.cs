using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // private TextMeshProUGUI[] timeCounter;

    private static string levelName;

    private static float startTime;
    private static float endTime;

    private static float time = 0;

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // timeCounter = GameObject.FindGameObjectWithTag("TimeCounterUI").GetComponent<TextMeshProUGUI[]>();
        // foreach(TextMeshProUGUI currentScoreText in timeCounter) {
        //     currentScoreText.text = time.ToString("D7");
        // }

        startTime = Time.time;
        levelName = scene.name;
    }

    // void FixedUpdate() {
    //     foreach(TextMeshProUGUI currentScoreText in timeCounter) {
    //         currentScoreText.text = time.ToString("D7");
    //     }
    // }

    public static void LogTime() {
        endTime = Time.time;

        time = endTime - startTime;

        DBManager.NewRun(levelName, time);
    }

}
