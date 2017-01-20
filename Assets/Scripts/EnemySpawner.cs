﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float m_MinimumInterval = 1f;
    public float m_MaximumInterval = 5f;

    private float m_timer = 0f;
    private float m_nextSpawnAt = 0f;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
        
        if ((m_timer += Time.deltaTime) >= m_nextSpawnAt)
        {
            m_timer -= m_nextSpawnAt;
            m_nextSpawnAt = Random.Range(m_MinimumInterval, m_MaximumInterval);

            // Spawn enemy
            Instantiate(Resources.Load("Prefabs/EnemyCharacter", typeof(GameObject)), transform.position, transform.rotation);
        }
	}
}
