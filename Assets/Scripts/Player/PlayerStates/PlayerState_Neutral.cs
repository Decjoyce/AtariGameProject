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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(0);
        }

        if (movementInput > 0.1 || movementInput < -0.1)
        {
            controller.rb.velocity = new Vector3(0, controller.rb.velocity.y, controller.currentSpeed * movementInput * Time.fixedDeltaTime);
        }


        float angle = Mathf.Atan2(lookInput.x, -lookInput.y) * Mathf.Rad2Deg;
        if (lookInput.magnitude > 0.7)
        {
            float newAngle = ModularClamp(-angle, -110f, 110f);
            Quaternion newRot;
            if (lerpedRotation)
                newRot = Quaternion.Lerp(controller.pivot.rotation, Quaternion.Euler(newAngle, 0, 0), controller.lerpedAimSpeed * Time.fixedDeltaTime);
            else
                newRot = Quaternion.Euler(newAngle, 0, 0);
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

        //Code for crouchingn here

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
