﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class WeaponController : MonoBehaviour
{
    public Transform pyssy;
    public ParticleSystem smokeParticle;
    public GameObject bulletPrefab;
    public GameObject hitParticle;
    public Transform characterDirection;
    public float m_cooldownTime = 1.0f;
    public float m_gunDamageAmount = 50.0f;
    public AudioClip m_shootSound;

    private AudioSource m_AudioSource;
    bool m_canShoot;
   
    // Use this for initialization
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        enableShooting();
    }

    private void PlayShootSound()
    {
        m_AudioSource.clip = m_shootSound;
        m_AudioSource.Play();
    }

    void enableShooting()
    {
        pyssy.GetComponent<Animator>().SetBool("ShotFired", false);
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
        // Gun smoke
        //  Transform spawnPos = GameObject.FindGameObjectWithTag("GunSmokeSpawn").transform;
        //  Transform go = Instantiate(smokeParticle, spawnPos.position, spawnPos.rotation).transform;
        //  go.parent = go;
        smokeParticle.Play();
        PlayShootSound();
        pyssy.GetComponent<Animator>().SetBool("ShotFired", true);

       // Vector3 offset = Quaternion.Inverse(characterDirection.rotation) * new Vector3(0.4f, -0.4f, 0.0f);
        Instantiate(bulletPrefab, pyssy.position, pyssy.rotation);
        RaycastHit hit;
        if (Physics.Raycast(characterDirection.position, characterDirection.forward, out hit, 100f) )
        {
            if (hit.transform.tag == "Enemy")
            {
            	Instantiate(hitParticle, hit.point, Quaternion.Inverse(Quaternion.LookRotation(hit.normal))).transform.SetParent(hit.transform, true);
                hit.transform.gameObject.SendMessage("applyDamage", m_gunDamageAmount);
            }
            else if (hit.transform.tag == "Head")
            {
                Debug.Log("Head shot!!");
                if (GameObject.FindGameObjectWithTag("HeadShotText"))
                {
                    GameObject.FindGameObjectWithTag("HeadShotText").GetComponent<TextNotifier>().show();
                }
                Instantiate(hitParticle, hit.point, Quaternion.identity);
                Instantiate(hitParticle, hit.point + new Vector3(0.1f, 0.1f,  0.1f), Quaternion.identity);
                Instantiate(hitParticle, hit.point + new Vector3(-0.1f, -0.1f, -0.1f), Quaternion.identity);
                hit.transform.parent.SendMessage("applyDamage", 5.0f*m_gunDamageAmount);
            }
        }
    }

    void doMelee()
    {

    }
}
