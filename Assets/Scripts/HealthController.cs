using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthController : MonoBehaviour
{
    public GameObject bloodTexture;

    public float m_health = 100;

    private float m_maxHealth;
    private SceneController m_scene;

    void Start()
    {
        m_scene = FindObjectOfType<SceneController>();
        m_maxHealth = m_health;
    }


    void disableBlood()
    {
        bloodTexture.SetActive(false);
    }

    public void applyDamage(float damage)
    {
        m_health -= damage;
        
        if (m_health <= 0.0f)
        {
            m_health = 0.0f;
            Invoke("deleteMe", 0.0f);
        }

        if (bloodTexture != null)
        {
            bloodTexture.SetActive(true);
            Invoke("disableBlood", 0.2f + ((1.0f-(m_health/m_maxHealth)) * 1.0f));
        }

        if (gameObject.tag == "Player")
        {
            GameObject hb = GameObject.FindGameObjectWithTag("HealthBar");
            if(hb != null)
            {
                HealthBarController hbc = hb.GetComponent<HealthBarController>();
                hbc.setValue(m_health, m_maxHealth);
            }
        }
    }

    public bool dead()
    {
        return m_health > 0f;
    }

    // Update is called once per frame
    void deleteMe()
    {   if (gameObject.tag == "Player")
        {
            GetComponent<WeaponController>().enabled = false;
            GetComponent<FirstPersonController>().enabled = false;
            GameController.instance().onGameOver();
        }
        else
        {
            GameController.instance().onEnemyDestroyed();
            SendMessage("dying");
            Invoke("destroyMe", 3.0f);
        }
    }

    void destroyMe()
    {
        Destroy(gameObject);
        m_scene.checkWaveEnd();
    }
}
