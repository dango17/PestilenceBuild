using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DO
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimationHandler animationHandler;
        InputHandler inputHandler; 
        public string lastAttack;
        WeaponSlotManager weaponSlotManager; 

        private void Awake()
        {
            animationHandler = GetComponentInChildren<AnimationHandler>();
            inputHandler = GetComponent<InputHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>(); 
        } 

        public void HandleWeaponCombo(WeaponItem weapon)
        { 
            if(inputHandler.comboFlag)
            {
                animationHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack ==  weapon.OH_Light_Attack_1)
                {
                    animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
            }       
        }
         
        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon; 
            animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
            lastAttack = weapon.OH_Light_Attack_1;
        } 

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
          
        }
    }

}
