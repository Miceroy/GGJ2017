using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    //static GameController g_instance = null;
    public GameObject gameOverObject;

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

    void startGame()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }
}
