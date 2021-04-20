using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Life Variables")]
    public float health;
    public float maxHealth;
    public float stamina = 100;
    public float maxStamina;
    public float mana = 100;
    public float maxMana;
    [Header("UI Elements")]
    public Image healthBar;
    public Image staminaBar;
    public Image manaBar;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            health = health - 10;
            if (health < 0)
            {
                health = 0;
            }
            float fraction = health / maxHealth;
            healthBar.fillAmount = fraction;
        }

    }

}
