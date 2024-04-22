using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerState_CharacterSelect : PlayerState_Base
{
    int selectedCharacter;


    public override void EnterState(PlayerController controller)
    {
        selectedCharacter = 0;
        //FakeMenuManager.instance.ChangeDisplayCard(controller.playerNum, selectedCharacter);
    }
    public override void ExitState(PlayerController controller)
    {
        controller.col.enabled = true;
        controller.rb.constraints = RigidbodyConstraints.None;
        controller.rb.constraints |= RigidbodyConstraints.FreezeRotation;
        controller.rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        GameManager.instance.SelectCharacter(controller.playerNum, selectedCharacter);
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
        if (ctx.performed)
        {
            selectedCharacter--;
            if (selectedCharacter < 0)
            {
                selectedCharacter = GameManager.instance.playerCharacters[controller.playerNum - 1].Count - 1;
            }
            //FakeMenuManager.instance.ChangeDisplayCard(controller.playerNum, selectedCharacter);
        }
    }

    public override void OnCrouch(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnShoot(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            selectedCharacter++;
            if (selectedCharacter >= GameManager.instance.playerCharacters[controller.playerNum - 1].Count)
                selectedCharacter = 0;
            //FakeMenuManager.instance.ChangeDisplayCard(controller.playerNum, selectedCharacter);
        }
    }

    public override void OnAction(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnInteract(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }

    public override void OnReload(PlayerController controller, InputAction.CallbackContext ctx)
    {
        
    }
}
