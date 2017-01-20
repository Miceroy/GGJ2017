using System.Collections;
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
        RaycastHit[] hits;
        hits = Physics.RaycastAll(characterDirection.position, characterDirection.forward, 100.0F);
        Debug.Log("hits.Length = " + hits.Length.ToString());
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Instantiate(hitParticle, hit.point, Quaternion.identity);
            HealthController health = hit.transform.GetComponent<HealthController>();
            if( health != null)
            {
                health.applyDamage(m_gunDamageAmount);
            }
        }
    }

    void doMelee()
    {
    }
}
