using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponController : MonoBehaviour {
    public float m_cooldownTime = 1.0f;

    bool m_canShoot;
    bool m_shoot;
    bool m_melee;

    // Use this for initialization
    void Start () {
        enableShooting();
    }

    void enableShooting()
    {
        m_canShoot = true;
    }

    // Update is called once per frame
    void Update () {

        m_shoot = CrossPlatformInputManager.GetButtonDown("Fire1");
        m_melee = CrossPlatformInputManager.GetButtonDown("Fire2");
        if (m_canShoot && m_shoot)
        {
            m_canShoot = false;
            Invoke("enableShooting", m_cooldownTime);
            Debug.Log("Shoot = " + m_shoot.ToString() );
        }
        else if (m_canShoot && m_melee)
        {
            m_canShoot = false;
            Invoke("enableShooting", m_cooldownTime);
            Debug.Log("Melee = " + m_melee.ToString());
        }
        else
        {
        }

    }
}
