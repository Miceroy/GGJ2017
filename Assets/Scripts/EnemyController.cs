﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float m_shootRange = 20.0f;
    private Animator animator;
    private float m_recalcTimer = 0f;
    private float m_shootTimer = 0f;
    private GameObject m_playerObject;
    private bool m_pushed = false;
    float m_oldSpeed;
    private Vector3 m_pushMovement;
    bool m_enableShooting;
    private float m_pushTimer = 2f;
   
    // Use this for initialization
    void Start()
    {
        m_enableShooting = true;
        animator = GetComponent<Animator>();
        m_playerObject = GameObject.Find("FPSController");
    }

    // Update is called once per frame
    void Update()
    {
        const float recalcThreshold = 0.5f;
        const float shootThreshold = 2.35f;

        if (m_pushed)
        {
            m_pushMovement.y -= Time.fixedDeltaTime;
            m_pushMovement.x *= 0.75f * Time.fixedDeltaTime;
            m_pushMovement.z *= 0.75f * Time.fixedDeltaTime;

            m_pushTimer += Time.fixedDeltaTime;

            if (Physics.Raycast(transform.position, Vector3.down * 2f, 2f))
            {
                if (m_pushTimer >= 0.5f)
                {
                    m_pushed = false;
                    GetComponent<HealthController>().applyDamage(25f);
                    applyDamage(25f);

                    GetComponent<NavMeshAgent>().enabled = true;
                }
            }
            if (Physics.Raycast(transform.position, Vector3.up, 1f))
                m_pushMovement.y = 0f;

            Vector3 pos = transform.position;
            pos += m_pushMovement;

            if (!m_pushed)
                pos.y = 1.5f;

            transform.position = pos;

            return;
        }

        if (transform.position.y < -1000f)
            Destroy(gameObject);

        if ((m_recalcTimer += Time.deltaTime) >= recalcThreshold)
        {
            GetComponent<NavMeshAgent>().SetDestination(m_playerObject.transform.position);
            m_recalcTimer -= recalcThreshold;
        }

        if ((m_shootTimer += Time.deltaTime) >= shootThreshold)
        {
            if (isInRange() && m_oldSpeed == 0.0f )
            {
                stopEnemy();
                //Invoke("playEnemy", 2.35f);
                Invoke("shootBullet", 1.4f);
                m_shootTimer -= shootThreshold;
            }
        }

        if (m_oldSpeed > 0.0f)
        {
            Vector3 toPlayer = m_playerObject.transform.position - transform.position;
            toPlayer.y = 0;
            gameObject.transform.rotation = Quaternion.LookRotation(toPlayer.normalized);
        }
    }

    void shootBullet()
    {
        if (m_enableShooting)
        {
            RaycastHit hit;
            Vector3 toPlayer = m_playerObject.transform.position - transform.position;
            if (Physics.Raycast(transform.position, toPlayer, out hit, toPlayer.magnitude) &&
                hit.transform.name == "FPSController")
                Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject)), transform.position, transform.rotation);
        }
    }

    bool isInRange()
    {
        Vector3 toPlayer = m_playerObject.transform.position - transform.position;
        bool res = toPlayer.magnitude <= m_shootRange;
        animator.SetBool("EnemyInRange", res);
        return res;
    }

    void stopEnemy()
    {
        if (m_oldSpeed == 0.0f)
        {
            m_oldSpeed = GetComponent<NavMeshAgent>().speed;
            GetComponent<NavMeshAgent>().speed = 0.0f;
        }
    }

    void playEnemy()
    {
        m_enableShooting = true;
        if (m_oldSpeed > 0.0f)
        {
            GetComponent<NavMeshAgent>().speed = m_oldSpeed;
            m_oldSpeed = 0.0f;
        }
    }

    public void pushBack(Vector3 amount)
    {
        m_pushed = true;
        m_pushTimer = 0f;

        GetComponent<NavMeshAgent>().enabled = false;
        
        m_pushMovement = amount;
    }

    void applyDamage(float dmg)
    {
        //  Debug.Log("Apply animation damage");
        m_enableShooting = false;
        stopEnemy();
        animator.SetBool("TakeDamage",true);
        Invoke("disableDamageAnimation", 1.25f);
    }

    void disableDamageAnimation()
    {
        //playEnemy();
        m_enableShooting = true;
        animator.SetBool("TakeDamage", false);
    }

    void dying()
    {
        stopEnemy();
        playDead();
    }

    void playDead()
    {
        GetComponent<NavMeshAgent>().speed = 0.0f;
        animator.SetBool("KilledByBullet", true);
    }
}

