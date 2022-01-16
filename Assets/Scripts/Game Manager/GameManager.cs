using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    void Awake () {
        Instance = this;
    }

    public void Restart () {
        // reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}