using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DO
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);
        } 

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerMovement playerMovement;
            AnimationHandler animationHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerMovement = playerManager.GetComponent<PlayerMovement>();
            animationHandler = playerManager.GetComponentInChildren<AnimationHandler>(); 

            playerMovement.rigidbody.velocity = Vector3.zero;
            animationHandler.PlayTargetAnimation("PickUpItem", true);
            playerInventory.weaponsInventory.Add(weapon);
            Destroy(gameObject);
        }
    }
}

