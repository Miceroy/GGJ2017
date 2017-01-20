using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Props
    [SerializeField] private float m_MinimumInterval = 1f;
    [SerializeField] private float m_MaximumInterval = 5f;

    private float m_timer = 0f;
    private float m_nextSpawnAt = 0f;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
        
        if (m_nextSpawnAt <= (m_timer += Time.deltaTime))
        {
            m_nextSpawnAt = Random.Range(m_MinimumInterval, m_MaximumInterval);
            m_timer = 0f;

            // Spawn enemy
            GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/EnemyCharacter", typeof(GameObject)), transform.position, transform.rotation);
        }
	}
}
