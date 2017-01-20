using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponController : MonoBehaviour {
    bool m_shoot;
    bool m_melee;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        m_shoot = CrossPlatformInputManager.GetButtonDown("Fire1");
        m_melee = CrossPlatformInputManager.GetButtonDown("Fire2");

        Debug.Log("Shoot = " + m_shoot.ToString() + " Melee = " + m_melee.ToString());
    }
}
