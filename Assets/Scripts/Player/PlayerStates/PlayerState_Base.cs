using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates
{
    frozen,
    Neutral,
    Extracted,
    Death,
}

public abstract class PlayerState_Base
{
    public abstract void EnterState(PlayerController controller);
    public abstract void ExitState(PlayerController controller);

    public virtual void FrameUpdate(PlayerController controller)
    {

    }

    public virtual void PhysicsUpdate(PlayerController controller)
    {

    }

    public virtual void OnMove(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnLook(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnLayerDown(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnLayerUp(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnJump(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnCrouch(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnShoot(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnAction(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnInteract(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public virtual void OnReload(PlayerController controller, InputAction.CallbackContext ctx)
    {
        
    }
}
