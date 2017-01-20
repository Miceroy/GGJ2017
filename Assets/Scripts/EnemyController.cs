using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject m_playerObject;

	// Use this for initialization
	void Start ()
    {
        m_playerObject = GameObject.Find("FPSController");
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 enemyToPlayer = (m_playerObject.transform.position - transform.position);

        transform.Translate(enemyToPlayer.normalized * Time.deltaTime * 8f);

        if (enemyToPlayer.magnitude < 5f)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        
    }
}
