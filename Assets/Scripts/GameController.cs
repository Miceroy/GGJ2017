using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour {
    public GameObject gameOverObject;
    public Text scoreText;
    public float m_maxStamina = 5.0f;
    public float m_staminaIncreaseSpeed = 0.1f;
    int m_score;
    float m_stamina;

    public float getStamina()
    {
        return m_stamina;
    }

    public void decreaseStamina(float speed)
    {
        // Increase stamina
        GameObject hb = GameObject.FindGameObjectWithTag("StaminaBar");
        if (hb != null)
        {
            HealthBarController hbc = hb.GetComponent<HealthBarController>();
            m_stamina -= speed * Time.deltaTime;
            if (m_stamina < 0.0f)
                m_stamina = 0.0f;
            hbc.setValue(m_stamina, m_maxStamina);
        }
    }

	public static GameController instance () {
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        return obj.GetComponent<GameController>();
	}

    // Called, when player has died
    public void onGameOver()
    {
        gameOverObject.SetActive(true);
        Invoke("startGame", 5.0f);
    }

    public void onEnemyDestroyed()
    {
        ++m_score;
        scoreText.text = "Score: " + m_score.ToString();

        
    }

    void startGame()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    void Update()
    {
        bool isGrounded = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().isGrounded;
        if (isGrounded)
        {
            // Increase stamina
            GameObject hb = GameObject.FindGameObjectWithTag("StaminaBar");
            if (hb != null)
            {
                HealthBarController hbc = hb.GetComponent<HealthBarController>();
                m_stamina += m_staminaIncreaseSpeed * Time.deltaTime;
                if (m_stamina > m_maxStamina)
                    m_stamina = m_maxStamina;
                hbc.setValue(m_stamina, m_maxStamina);
            }
        }
    }
}
