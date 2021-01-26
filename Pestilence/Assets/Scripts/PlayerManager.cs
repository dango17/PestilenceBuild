using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DO
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerMovement playerMovement; 

        public bool isInteracting; 

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo; 

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>(); 
        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerMovement = GetComponent<PlayerMovement>(); 
        }

      void Update()
      {
            float delta = Time.deltaTime; 
            inputHandler.isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            anim.SetBool("isInAir", isInAir); 

            inputHandler.TickInput(delta);
            playerMovement.HandleRollingAndSprinting(delta);
            playerMovement.HandleJumping();
                 
            CheckForInteractableObject();
      }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            playerMovement.HandleMovement(delta);           
            playerMovement.HandleFalling(delta, playerMovement.moveDirection);          
        } 

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
         
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false;
            inputHandler.jump_Input = false;

            float delta = Time.deltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            if (isInAir)
            {
                playerMovement.inAirTimer = playerMovement.inAirTimer * Time.deltaTime; 
            }
        } 

        public void CheckForInteractableObject()
        {
            RaycastHit hit; 

            if(Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 2f, cameraHandler.ignoreLayers))
            {
                if(hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        
                        if(inputHandler.a_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
        }
         
    }
}