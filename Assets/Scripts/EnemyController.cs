using System.Collections;
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
    float m_oldSpeed;
   
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        m_playerObject = GameObject.Find("FPSController");
    }

    // Update is called once per frame
    void Update()
    {
        const float recalcThreshold = 0.5f;
        const float shootThreshold = 2.35f;

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
                Invoke("playEnemy", 2.35f);
                Invoke("shootBullet", 1.4f);
                m_shootTimer -= shootThreshold;
            }
        }

        if (m_oldSpeed > 0.0f)
        {
            Vector3 toPlayer = m_playerObject.transform.position - transform.position;
            gameObject.transform.rotation =
                Quaternion.LookRotation(toPlayer.normalized);
        }
    }

    void shootBullet()
    {
        RaycastHit hit;
        Vector3 toPlayer = m_playerObject.transform.position - transform.position;
        if (Physics.Raycast(transform.position, toPlayer, out hit, toPlayer.magnitude) &&
            hit.transform.name == "FPSController")
            Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject)), transform.position, transform.rotation);
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
        if (m_oldSpeed > 0.0f)
        {
            GetComponent<NavMeshAgent>().speed = m_oldSpeed;
            m_oldSpeed = 0.0f;
        }
    }

    void applyDamage(float dmg)
    {
      //  Debug.Log("Apply animation damage");
        stopEnemy();
        animator.SetBool("TakeDamage",true);
        Invoke("disableDamageAnimation", 1.25f);
    }

    void disableDamageAnimation()
    {
        playEnemy();
        animator.SetBool("TakeDamage", false);
    }

    void dying()
    {
        stopEnemy();
        playDead();
    }

    void playDead()
    {
        animator.SetBool("KilledByBullet", true);
    }
}
