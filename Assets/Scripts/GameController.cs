using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject gameOverObject;
    public Text scoreText;
    int m_score;
	public static GameController instance () {
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        return obj.GetComponent<GameController>();
	}

    // Called, when player has died
    public void onGameOver()
    {
        gameOverObject.SetActive(true);
        Invoke("startGame", 5.0f);
    }

    public void onEnemyDestroyed()
    {
        ++m_score;
        scoreText.text = "Score: " + m_score.ToString();
    }

    void startGame()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }
}
