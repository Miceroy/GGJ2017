using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public uint m_InitialWaveAmount = 1;
    public float m_WaveMultiplier = 1.5f;
    public float m_MinimumSpawnInterval = 1f;
    public float m_MaximumSpawnInterval = 5f;

    private float m_timer = 0f;
    private float m_nextSpawnAt = 0f;
    private float m_currentSpawnAmount = 0f;
    private uint m_spawnsRemaining = 0;
    private bool m_spawning = false;

	// Use this for initialization
	void Start ()
    {
        m_currentSpawnAmount = m_InitialWaveAmount;
        m_nextSpawnAt = m_MinimumSpawnInterval;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_spawning)
        {
            if ((m_timer += Time.deltaTime) >= m_nextSpawnAt)
            {
                m_timer -= m_nextSpawnAt;
                m_nextSpawnAt = Random.Range(m_MinimumSpawnInterval, m_MaximumSpawnInterval);

                // Spawn enemy
                Instantiate(Resources.Load("Prefabs/EnemyCharacter", typeof(GameObject)), transform.position, transform.rotation);

                m_spawning = (--m_spawnsRemaining != 0);
            }
        }
	}

    public bool spawning
    {
        get { return m_spawning; }
        set
        {
            if (m_spawning)
                return;

            m_spawnsRemaining = (uint)Mathf.Ceil(m_currentSpawnAmount *= m_WaveMultiplier);
            Debug.Log(m_spawnsRemaining);
            m_spawning = true;
        }
    }
}
