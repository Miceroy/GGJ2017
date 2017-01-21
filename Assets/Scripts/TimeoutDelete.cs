using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeoutDelete : MonoBehaviour {
    public float timeoutTime = 2.0f;

	// Use this for initialization
	void Start () {
        Invoke("deleteMe", timeoutTime);
	}
	
	// Update is called once per frame
	void deleteMe() {
        Destroy(gameObject);
	}
}
