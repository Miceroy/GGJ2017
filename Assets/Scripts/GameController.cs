using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : object {
	static GameController g_instance = null;

	static GameController instance () {
		if (g_instance == null) {
			g_instance = new GameController ();
		}
		return g_instance;
	}

}
