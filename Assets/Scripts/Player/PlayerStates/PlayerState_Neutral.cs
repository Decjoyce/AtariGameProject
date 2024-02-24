using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerState_Neutral : PlayerState_Base
{
    float movementInput;
    Vector2 lookInput;
    bool lerpedRotation;
    bool isCrouched;

    public override void EnterState(PlayerController controller)
    {

    }

    public override void ExitState(PlayerController controller)
    {

    }

    public override void FrameUpdate(PlayerController controller)
    {

    }

    public override void PhysicsUpdate(PlayerController controller)
    {

        if (movementInput > 0.1 || movementInput < -0.1)
        {
            //Vector3 velocity = new(controller.currentSpeed * movementInput * Time.fixedDeltaTime, controller.rb.velocity.y, 0f);
            //controller.rb.velocity = velocity;
            controller.rb.AddForce(Vector3.right * controller.currentSpeed * movementInput * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }


        float angle = Mathf.Atan2(lookInput.x, -lookInput.y) * Mathf.Rad2Deg;
        if (lookInput.magnitude > 0.7)
        {
            float newAngle = ModularClamp(angle, -110f, 110f);
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
        if (ctx.performed)
            Jump(controller);
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
        //Code for jumping here
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

    //Here temp
    public float ModularClamp(float val, float min, float max, float rangemin = -180f, float rangemax = 180f)
    {
        var modulus = Mathf.Abs(rangemax - rangemin);
        if ((val %= modulus) < 0f) val += modulus;
        return Mathf.Clamp(val + Mathf.Min(rangemin, rangemax), min, max);
    }

}
