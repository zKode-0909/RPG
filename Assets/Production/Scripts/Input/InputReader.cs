using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using static PlayerInput;


//[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
public class PlayerInputReader : MonoBehaviour,IPlayerActions
{

    private PlayerInput playerInputActions;

    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInput();
            playerInputActions.Player.SetCallbacks(this);
        }


        playerInputActions.Player.Enable();

    }

    private void OnDisable()
    {
        playerInputActions.Player.SetCallbacks(null);
        playerInputActions.Disable();
    }





    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Debug.Log("Click Started");
            EventBus<InputClickStartEvent>.Raise(new InputClickStartEvent(mousePos));
        }
        else if (context.phase == InputActionPhase.Canceled) {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Debug.Log("Click Ended");
            EventBus<InputClickEndEvent>.Raise(new InputClickEndEvent(mousePos));
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Input received");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Input received");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Input received");
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Input received");
        }
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Input received");
        }
    }



    public void OnNext(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Input received");
        }
    }

 

 




    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Input received");
        }
    }





    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnOpenQuestLog(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) {
            EventBus<QuestLogEvent>.Raise(new QuestLogEvent());
        }
    }
}
