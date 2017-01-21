using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float m_Speed = 20f;
    public float m_LifeTime = 10f;

    private float m_timer = 0f;
    private Vector3 m_direction;
    
	// Use this for initialization
	void Start()
    {
        m_direction = (GameObject.Find("FPSController").transform.position - transform.position).normalized;
    }
	
	// Update is called once per frame
	void Update()
    {
        Vector3 nextStep = m_direction * m_Speed * Time.deltaTime;
        bool destroy = false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, nextStep, out hit, nextStep.magnitude))
        {
            HealthController health = hit.transform.GetComponent<HealthController>();
            if (health)
                health.applyDamage(5f);

            destroy = true;
        }
        else
            transform.localPosition += nextStep;

        if ((m_timer += Time.deltaTime) >= m_LifeTime)
            destroy = true;

        if (destroy)
            Destroy(gameObject);
    }
}
