using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class FlightMode : MonoBehaviour
{
    public float m_staminaDecreaseOnStart = 2.0f;
    public float m_staminaDecreaseSpeed = 1.0f;
    public bool m_flying = false;

    void Update()
    {
        GameController game = GameController.instance();

        if (CrossPlatformInputManager.GetButtonDown("Fire3"))
        {
            m_flying = !m_flying;
            FirstPersonController fpsCtrl = GetComponent<FirstPersonController>();
            fpsCtrl.setFlying(m_flying);
            game.decreaseStamina(m_staminaDecreaseOnStart/Time.deltaTime);
        }

        if (m_flying)
        {
            game.decreaseStamina(m_staminaDecreaseSpeed);
            if (game.getStamina() <= 0.0f)
            {
                m_flying = false;
                FirstPersonController fpsCtrl = GetComponent<FirstPersonController>();
                fpsCtrl.setFlying(m_flying);
            }
        }
        else
        {
        }
    }
}
