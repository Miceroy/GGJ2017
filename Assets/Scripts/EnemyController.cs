using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float m_timer = 0f;
    private GameObject m_playerObject;

	// Use this for initialization
	void Start ()
    {
        m_playerObject = GameObject.Find("FPSController");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        const float threshold = 0.5f;

        if ((m_timer += Time.deltaTime) >= threshold)
        {
            GetComponent<NavMeshAgent>().SetDestination(m_playerObject.transform.position);
            m_timer -= threshold;
        }
    }
}
