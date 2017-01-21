using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float m_recalcTimer = 0f;
    private float m_shootTimer = 0f;
    private GameObject m_playerObject;
    private bool m_pushed = false;
    private Vector3 m_pushMovement;
    private float m_pushTimer = 2f;

	// Use this for initialization
	void Start()
    {
        m_playerObject = GameObject.Find("FPSController");
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        const float recalcThreshold = 0.5f;
        const float shootThreshold = 1f;

        if (m_pushed)
        {
            m_pushMovement.y -= Time.fixedDeltaTime;
            m_pushMovement.x *= 0.75f * Time.fixedDeltaTime;
            m_pushMovement.z *= 0.75f * Time.fixedDeltaTime;

            m_pushTimer += Time.fixedDeltaTime;
            
            if (Physics.Raycast(transform.position, Vector3.down, 1f))
            {
                if (m_pushTimer >= 1.0f)
                {
                    m_pushed = false;
                    GetComponent<HealthController>().applyDamage(25f);
                    GetComponent<NavMeshAgent>().enabled = true;
                }
            }

            Vector3 pos = transform.position;
            pos += m_pushMovement;

            if (!m_pushed)
                pos.y = 1.3f;

            transform.position = pos;

            return;
        }

        if ((m_recalcTimer += Time.deltaTime) >= recalcThreshold)
        {
            GetComponent<NavMeshAgent>().SetDestination(m_playerObject.transform.position);
            m_recalcTimer -= recalcThreshold;
        }

        if ((m_shootTimer += Time.deltaTime) >= shootThreshold)
        {
            Vector3 toPlayer = m_playerObject.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, toPlayer, out hit, toPlayer.magnitude) &&
                hit.transform.name == "FPSController")
                Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject)), transform.position, transform.rotation);

            m_shootTimer -= shootThreshold;
        }
    }

    public void pushBack(Vector3 amount)
    {
        m_pushed = true;
        m_pushTimer = 0f;

        GetComponent<NavMeshAgent>().enabled = false;
        
        m_pushMovement = amount;
    }
}
