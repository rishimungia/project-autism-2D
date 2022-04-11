using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player" && (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)) {
            ScoreManager.LogTime();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else {
            ScoreManager.LogTime();
            SceneManager.LoadScene(1);
        }
    }
}
