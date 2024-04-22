using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Extracted : PlayerState_Base
{
    public override void EnterState(PlayerController controller)
    {
        //controller.gameObject.SetActive(false);
        //controller.graphicsPivot.gameObject.SetActive(false);
        controller.interaction.RemoveeAllInteractions();
        controller.rb.constraints = RigidbodyConstraints.FreezeAll;
        controller.col.enabled = false;
    }
    public override void ExitState(PlayerController controller)
    {
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
