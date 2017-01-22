using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public float m_WaveWaitTime = 10f;

    private bool m_waiting = true;
    private float m_WaveTimer = 0f;
    private EnemySpawner[] m_spawners;

	// Use this for initialization
	void Start ()
    {
        m_spawners = FindObjectsOfType<EnemySpawner>();
        m_WaveTimer = m_WaveWaitTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (m_waiting)
        {
            Text inboundText = GameObject.Find("WaveInboundText").GetComponent<Text>();

            const float fadeTime = 1f;
            const float half = fadeTime * 0.5f;

            if ((m_WaveTimer -= Time.deltaTime) <= fadeTime)
            {
                inboundText.enabled = true;
                inboundText.text = "Wave inbound!";

                Color col = inboundText.color;
                col.a = m_WaveTimer * half;
                inboundText.color = col;
            }
            if (m_WaveTimer <= 0f)
            {
                foreach (EnemySpawner spawner in m_spawners)
                    spawner.spawning = true;

                m_waiting = false;
                inboundText.enabled = false;
            }
        }

        Text text = GameObject.Find("WaveTimerText").GetComponent<Text>();

        if (text.enabled = m_waiting)
        {
            text.text = "Next wave: " + m_WaveTimer.ToString("0.0");
        }
	}

    public void checkWaveEnd()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 1)
            return;

        foreach (EnemySpawner spawner in m_spawners)
        {
            if (spawner.spawning)
                return;
        }

        m_waiting = true;
        m_WaveTimer = m_WaveWaitTime;
    }
}
