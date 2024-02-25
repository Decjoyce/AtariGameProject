using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerState_Neutral : PlayerState_Base
{
    float movementInput;
    Vector2 lookInput;
    bool lerpedRotation;
    bool isCrouched;
    bool isJumping;
    Rigidbody rb;

    float standColiderHeight = 2f;
    Vector3 standColideroffset = new(0f, 1f, 0f);
    Vector3 crouchColideroffset = new(0f, 0.65f, 0f);
    float crouchColiderHeight = 1.3f;

    Vector3 checkBoxSize = new(0.5f, 0.1f, 0.5f);

    public override void EnterState(PlayerController controller)
    {
        rb = controller.rb;
    }

    public override void ExitState(PlayerController controller)
    {

    }

    public override void FrameUpdate(PlayerController controller)
    {
        int numCollisions = Physics.OverlapBox(controller.transform.position - Vector3.up * controller.checkOffset, checkBoxSize, Quaternion.Euler(Vector3.zero), controller.groundLayers).Length;
        controller.isGrounded = numCollisions > 0;
        if (controller.isGrounded && isJumping)
        {
            isJumping = false;
        }

        float angle = Mathf.Atan2(lookInput.x, -lookInput.y) * Mathf.Rad2Deg;
        if (lookInput.magnitude > 0.7)
        {
            float newAngle = ExtensionMethods.ModularClamp(angle, -110f, 110f);
            Quaternion newRot;
            if (lerpedRotation)
                newRot = Quaternion.Lerp(controller.pivot.rotation, Quaternion.Euler(newAngle, 0, 0), controller.lerpedAimSpeed * Time.deltaTime);
            else
                newRot = Quaternion.Euler(0, 0, newAngle);
            controller.pivot.rotation = newRot;
        }
    }

    public override void PhysicsUpdate(PlayerController controller)
    {
        rb.AddForce(Vector3.up * (Physics.gravity.y * controller.gravityScale * rb.mass));

        if (movementInput > 0.1 || movementInput < -0.1)
        {
            Vector3 vel = Vector3.right * controller.currentSpeed * movementInput * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + vel);
        }
    }

    public override void OnMove(PlayerController controller, InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<float>();
    }

    public override void OnLook(PlayerController controller, InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    public override void OnJump(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (controller.isGrounded && ctx.performed)
        {
            Jump(controller);
        }
    }

    public override void OnCrouch(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            Crouch(controller);
    }

    public override void OnShoot(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            Shoot(controller);
    }

    public override void OnInteract(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            controller.interaction.Interact();
    }



    void Jump(PlayerController controller)
    {
        float jumpForce = Mathf.Sqrt(controller.jumpHeight * -2 * (Physics.gravity.y * controller.gravityScale * rb.mass));
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isJumping = true;
    }

    void Crouch(PlayerController controller)
    {
        isCrouched = !isCrouched;
        if (isCrouched)
        {
            controller.currentSpeed = controller.crouchSpeed;
            controller.currentHeight = controller.crouchHeight;

            controller.col.center = crouchColideroffset;
            controller.col.height = crouchColiderHeight;

            controller.crouchGraphics.SetActive(true); //Temp
            controller.standGraphics.SetActive(false); //Temp
        }
        else
        {
            controller.currentSpeed = controller.walkSpeed;
            controller.currentHeight = controller.normalHeight;

            controller.col.center = standColideroffset;
            controller.col.height = standColiderHeight;

            controller.crouchGraphics.SetActive(false); //Temp
            controller.standGraphics.SetActive(true); //Temp
        }
        Vector3 newPos = new Vector3(0, controller.currentHeight, 0);
        controller.pivot.localPosition = newPos;
    }

    //Here until functionality for weapons is added
    void Shoot(PlayerController controller)
    {
        GameObject bullet = controller.HelpInstantiate(controller.bulletPrefab, controller.firePoint.transform.position, controller.pivot.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(controller.firePoint.transform.up * controller.bulletForce, ForceMode.Impulse);
    }

}
