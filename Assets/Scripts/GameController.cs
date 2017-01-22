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
  //  bool m_inMainMenu = true;

    public float getStamina()
    {
        return m_stamina;
    }

    public void emptyStaminaIfHalf()
    {
        if (m_stamina >= m_maxStamina * 0.5f)
        {
            m_stamina -= m_maxStamina * 0.5f;

        }
        else
            FindObjectOfType<FirstPersonController>().resetAttackMult();
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
        Cursor.visible = true;
        gameOverObject.SetActive(true);
        Invoke("loadMainMenu", 5.0f);
    }

    public void onEnemyDestroyed()
    {
        ++m_score;
        scoreText.text = "Score: " + m_score.ToString();

        
    }

    public void loadMainMenu()
    {
  //      m_inMainMenu = true;
        Cursor.visible = true;
        SceneManager.LoadScene("Scenes/MainMenuScene");
        Cursor.visible = true;
    }

    public void startGame()
    {
 //       m_inMainMenu = false;
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void exit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Application.loadedLevelName == "MainMenuScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.loadedLevelName == "MainMenuScene")
            {
                Cursor.visible = true;
                exit();
            }
            else
            {
                Debug.Log("Quitting game");
                onGameOver();
            }
        }

        if (GameObject.FindGameObjectWithTag("Player"))
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
}
