﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class WeaponController : MonoBehaviour
{
    public GameObject hitParticle;
    public Transform characterDirection;
    public float m_cooldownTime = 1.0f;
    public float m_gunDamageAmount = 50.0f;

    bool m_canShoot;
   
    // Use this for initialization
    void Start()
    {
        enableShooting();
    }

    void enableShooting()
    {
        m_canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        bool shoot = CrossPlatformInputManager.GetButtonDown("Fire1");
        bool melee = CrossPlatformInputManager.GetButtonDown("Fire2");
        if (m_canShoot && shoot)
        {
            m_canShoot = false;
            Invoke("enableShooting", m_cooldownTime);
            doShooting();
        }
        else if (m_canShoot && melee)
        {
            m_canShoot = false;
            Invoke("enableShooting", m_cooldownTime);
            doMelee();
        }
        else
        {
        }

    }


    void doShooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(characterDirection.position, characterDirection.forward, out hit, 100f) && hit.transform.tag == "Enemy")
        {
            Instantiate(hitParticle, hit.point, Quaternion.identity);
            hit.transform.GetComponent<HealthController>().applyDamage(m_gunDamageAmount);
        }
    }

    void doMelee()
    {

    }
}
