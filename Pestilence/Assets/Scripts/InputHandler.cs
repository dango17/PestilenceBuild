using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DO
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool a_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool jump_Input;
        public bool lockOnInput;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right; 

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag; 
        public float rollInputTimer; 

        public bool isInteracting; 
       

        PlayerControls inputActions;
        CameraHandler cameraHandler;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput; 

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            cameraHandler = FindObjectOfType<CameraHandler>(); 
        }

        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                
                inputActions.PlayerActions.RB.performed += i => rb_Input = true;
                inputActions.PlayerActions.RT.performed += i => rt_Input = true;
                inputActions.PlayerActions.A.performed += i => a_Input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
                inputActions.PlayerQuickSlots.DPadRight.performed += inputActions => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += inputActions => d_Pad_Left = true;
                inputActions.PlayerActions.LockOn.performed += inputActions => lockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += inputActions => right_Stick_Right_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += inputActions => right_Stick_Left_Input = true; 
            }

            inputActions.Enable();
        } 

        private void OnDisable()
        {
            inputActions.Disable();
        } 

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotsInput();
            HandleLockOnInput();
            //HandleInteractingButtonInput(); 
            
        } 

        public void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        } 

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            sprintFlag = b_Input;

            if (b_Input)
            {
                rollInputTimer += delta;    
            } 
            else
            {
                if(rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true; 
                }
                rollInputTimer = 0;
            }
        } 

        private void HandleAttackInput(float delta)
        {
           

            if(rb_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;
                    if (playerManager.canDoCombo)
                        return;
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }               
            }

            if(rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        } 

        private void HandleQuickSlotsInput()
        {
            
            if (d_Pad_Right)
            {
                playerInventory.ChangeRightWeapon(); 
            } 
            else if(d_Pad_Left)
            {
               playerInventory.ChangeLeftWeapon(); 
            }
        }  

        private void HandleLockOnInput()
        {
            if(lockOnInput && lockOnFlag == false)
            {                
                lockOnInput = false;            
                cameraHandler.HandleLockOn();  
                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true; 
                }
            } 
            else if(lockOnInput && lockOnFlag)
            {
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets(); 
            } 
            if(lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn(); 
                if(cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget; 
                }
            } 

            if(lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn(); 
                if(cameraHandler.RightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.RightLockTarget; 
                }
            }

            cameraHandler.SetCameraHeight(); 
        } 
    }
}

