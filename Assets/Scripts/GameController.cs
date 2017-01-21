using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : object {
	static GameController g_instance = null;

	public static GameController instance () {
		if (g_instance == null) {
			g_instance = new GameController ();
		}
		return g_instance;
	}

    // Called, when player has died
    public void onGameOver()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }
}
