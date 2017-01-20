using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float m_recalcTimer = 0f;
    private float m_shootTimer = 0f;
    private GameObject m_playerObject;

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
}
