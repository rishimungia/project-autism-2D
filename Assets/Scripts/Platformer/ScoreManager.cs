using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

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
        startTime = Time.time;
        levelName = scene.name;

        Debug.Log("Time:");
        // Debug.Log("OnSceneLoaded: " + scene.name);
    }

    public static void LogTime() {
        endTime = Time.time;

        time = endTime - startTime;

        Debug.Log(levelName + ": " + time);
    }

}
