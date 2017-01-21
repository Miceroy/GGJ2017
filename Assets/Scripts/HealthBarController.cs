using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

	// Use this for initialization
	public void setValue (float value, float maxValue) {
        GetComponent<Scrollbar>().size = value/maxValue;
	}
	
}
