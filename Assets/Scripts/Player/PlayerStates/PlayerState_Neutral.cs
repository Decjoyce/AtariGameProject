using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerState_Neutral : PlayerState_Base
{
    float movementInput;
    Vector2 lookInput;
    bool lerpedRotation;
    bool isCrouched;
    bool isJumping;
    bool checkForGround;
    Rigidbody rb;

    Vector3 boxSize = new(0.5f, 0.1f, 0.5f);

    public override void EnterState(PlayerController controller)
    {
        rb = controller.rb;
    }

    public override void ExitState(PlayerController controller)
    {

    }

    public override void FrameUpdate(PlayerController controller)
    {
            int numCollisions = Physics.OverlapBox(controller.transform.position - Vector3.up * controller.checkOffset, boxSize, Quaternion.Euler(Vector3.zero), controller.playerLayer).Length;
            Debug.Log(numCollisions);
            Debug.Log(controller.isGrounded);
            controller.isGrounded = numCollisions > 0;
            if (controller.isGrounded && isJumping)
            {
                isJumping = false;
            }
    }

    public override void PhysicsUpdate(PlayerController controller)
    {

            rb.AddForce(Vector3.up * (Physics.gravity.y * controller.gravityScale * rb.mass));
 

        if (movementInput > 0.1 || movementInput < -0.1)
        {
            //rb.AddForce(Vector3.right * controller.currentSpeed * movementInput * Time.fixedDeltaTime, ForceMode.VelocityChange);
            Vector3 vel = Vector3.right * controller.currentSpeed * movementInput * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + vel);
        }


        float angle = Mathf.Atan2(lookInput.x, -lookInput.y) * Mathf.Rad2Deg;
        if (lookInput.magnitude > 0.7)
        {
            float newAngle = ExtensionMethods.ModularClamp(angle, -110f, 110f);
            Quaternion newRot;
            if (lerpedRotation)
                newRot = Quaternion.Lerp(controller.pivot.rotation, Quaternion.Euler(newAngle, 0, 0), controller.lerpedAimSpeed * Time.fixedDeltaTime);
            else
                newRot = Quaternion.Euler(0, 0, newAngle);
            controller.pivot.rotation = newRot;
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
            string s = "";
            foreach(Collider col in Physics.OverlapBox(controller.transform.position, Vector3.one * 0.1f))
            {
                s += col.name + " + ";
            }
            Debug.Log(s);
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

    void Jump(PlayerController controller)
    {
        Debug.Log("Jumped");
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
            controller.crouchGraphics.SetActive(true);
            controller.standGraphics.SetActive(false);
        }
        else
        {
            controller.currentSpeed = controller.walkSpeed;
            controller.currentHeight = controller.normalHeight;
            controller.crouchGraphics.SetActive(false);
            controller.standGraphics.SetActive(true);
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
