using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Death : PlayerState_Base
{
    public override void EnterState(PlayerController controller)
    {
        controller.attack.DropWeapon(true);
        controller.col.enabled = false;
        controller.rb.constraints = RigidbodyConstraints.FreezeAll;
        controller.interaction.ClearInventory();
        controller.anim.SetBool("isDead", true);
        controller.anim.SetLayerWeight(1, 0f);
    }
    public override void ExitState(PlayerController controller)
    {
        controller.anim.SetBool("isDead", false);
        controller.anim.SetLayerWeight(1, 1f);

        controller.col.enabled = true;
        controller.rb.constraints = RigidbodyConstraints.None;
        controller.rb.constraints |= RigidbodyConstraints.FreezeRotation;
        controller.rb.constraints |= RigidbodyConstraints.FreezePositionZ;
    }

    public override void FrameUpdate(PlayerController controller)
    {

    }

    public override void PhysicsUpdate(PlayerController controller)
    {

    }

    public override void OnMove(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnLook(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnLayerDown(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnLayerUp(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnJump(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnCrouch(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnShoot(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnAction(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if(controller.debuggingMode && ctx.performed)
        {
            controller.health.Heal(100000);
            controller.SwitchState("NEUTRAL");
            controller.col.enabled = true;
            controller.rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            controller.rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }

    public override void OnInteract(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnReload(PlayerController controller, InputAction.CallbackContext ctx)
    {
        
    }
}
