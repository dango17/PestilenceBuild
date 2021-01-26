using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DO
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10; 
        public int maxStamina;
        public int currentStamina; 

        AnimationHandler animationHandler;
        HealthBar healthBar;
        StaminaBar staminaBar; 

        private void Awake()
        {
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>(); 
            animationHandler = GetComponentInChildren<AnimationHandler>(); 
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);
        }

        private int SetMaxHealthFromHealthLevel()
        {  
            maxHealth = healthLevel * 10;  
            return maxHealth;  
        }

        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animationHandler.PlayTargetAnimation("Hurt", true); 

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animationHandler.PlayTargetAnimation("Death", true); 
            }
        } 

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }
    } 
}

