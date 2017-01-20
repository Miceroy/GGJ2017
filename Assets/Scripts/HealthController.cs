using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    public float m_health = 100;

    public void applyDamage(float damage)
    {
        m_health -= damage;
        if (m_health <= 0.0f)
        {
            Invoke("deleteMe", 0.0f);
        }
    }

    // Update is called once per frame
    void deleteMe()
    {
        Destroy(gameObject);
    }
}
