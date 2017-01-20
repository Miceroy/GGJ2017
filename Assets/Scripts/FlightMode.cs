using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class FlightMode : MonoBehaviour {
    public bool m_flying = false;

    void Update()
    {
        if ( CrossPlatformInputManager.GetButtonDown("Fire3") )
        {
            Debug.Log("Fire3");
            m_flying = !m_flying;
            FirstPersonController fpsCtrl = GetComponent<FirstPersonController>();
            fpsCtrl.setFlying(m_flying);
        }
    }
}
