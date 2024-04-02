using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum BulldozerStates
{
    idle,
    aggro,
    charge,
}

public abstract class EnemyStates_Bulldozer
{
    public abstract void EnterState(Enemy_Bulldozer controller);
    public abstract void ExitState(Enemy_Bulldozer controller);
    public abstract void FrameUpdate(Enemy_Bulldozer controller);
    public abstract void PhysicsUpdate(Enemy_Bulldozer controller);

    public virtual void OnRoomEnter(Enemy_Bulldozer controller, GameObject player) { }
    public virtual void OnRoomExit(Enemy_Bulldozer controller, GameObject player) { }

    public virtual void OnTriggerEnter(Enemy_Bulldozer controller, Collider other) { }
    public virtual void OnTriggerExit(Enemy_Bulldozer controller, Collider other) { }

    public virtual void OnAttackTriggerEnter(Enemy_Bulldozer controller, Collider other) { }
}

public class BulldozerState_Idle : EnemyStates_Bulldozer
{
    public override void EnterState(Enemy_Bulldozer controller)
    {


    }

    public override void ExitState(Enemy_Bulldozer controller)
    {

    }

    public override void FrameUpdate(Enemy_Bulldozer controller)
    {

    }

    public override void PhysicsUpdate(Enemy_Bulldozer controller)
    {

    }

    public override void OnRoomEnter(Enemy_Bulldozer controller, GameObject player)
    {
        controller.SwitchState("CHARGE");
    }

    public override void OnRoomExit(Enemy_Bulldozer controller, GameObject player)
    {

    }

    public override void OnTriggerEnter(Enemy_Bulldozer controller, Collider other)
    {

    }

    public override void OnAttackTriggerEnter(Enemy_Bulldozer controller, Collider other)
    {

    }

    public override void OnTriggerExit(Enemy_Bulldozer controller, Collider other)
    {

    }
}

public class BulldozerState_Aggro : EnemyStates_Bulldozer
{
    bool canCharge;
    float chargeDelay;

    public override void EnterState(Enemy_Bulldozer controller)
    {
        chargeDelay = controller.charge_delay;
        canCharge = false;
    }

    public override void ExitState(Enemy_Bulldozer controller)
    {

    }

    public override void FrameUpdate(Enemy_Bulldozer controller)
    {
        if(canCharge && chargeDelay <= 0)
        {
            controller.SwitchState("CHARGE");
        }
        else
        {
            chargeDelay -= Time.deltaTime;
        }
    }

    public override void PhysicsUpdate(Enemy_Bulldozer controller)
    {

    }

    public override void OnRoomEnter(Enemy_Bulldozer controller, GameObject player)
    {
        controller.NextTarget();
    }

    public override void OnRoomExit(Enemy_Bulldozer controller, GameObject player)
    {
        if (controller.targets.Contains(player.transform))
            controller.targets.RemoveAt(controller.targets.IndexOf(player.transform));
        if (controller.currentTarget = player.transform)
        {
            controller.NextTarget();
        }
    }

    public override void OnTriggerEnter(Enemy_Bulldozer controller, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.NextTarget();
        }
    }

    public override void OnTriggerExit(Enemy_Bulldozer controller, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (controller.currentTarget = other.transform)
                controller.NextTarget();
            else if (controller.targets.Contains(other.transform))
                controller.targets.RemoveAt(controller.targets.IndexOf(other.transform));
        }
    }
}

public class BulldozerState_Charge : EnemyStates_Bulldozer
{
    bool canCharge;
    bool isCharging;
    float chargeDelay;

    public override void EnterState(Enemy_Bulldozer controller)
    {
        chargeDelay = 5f;
        canCharge = true;
    }

    public override void ExitState(Enemy_Bulldozer controller)
    {

    }

    public override void FrameUpdate(Enemy_Bulldozer controller)
    {
        if (canCharge && chargeDelay <= 0)
        {
            isCharging = true;
        }
        else
        {
            chargeDelay -= Time.deltaTime;
        }
    }

    public override void PhysicsUpdate(Enemy_Bulldozer controller)
    {
        if (isCharging)
        {
            Vector3 vel = controller.transform.right * controller.chargeForce;
            controller.rb.AddForce(vel);
        }
    }

    public override void OnRoomEnter(Enemy_Bulldozer controller, GameObject player)
    {
        controller.NextTarget();
    }

    public override void OnRoomExit(Enemy_Bulldozer controller, GameObject player)
    {
        if (controller.targets.Contains(player.transform))
            controller.targets.RemoveAt(controller.targets.IndexOf(player.transform));
        if (controller.currentTarget = player.transform)
        {
            controller.NextTarget();
        }

    }

    public override void OnTriggerEnter(Enemy_Bulldozer controller, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.NextTarget();
        }
    }

    public override void OnTriggerExit(Enemy_Bulldozer controller, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (controller.currentTarget = other.transform)
                controller.NextTarget();
            else if (controller.targets.Contains(other.transform))
                controller.targets.RemoveAt(controller.targets.IndexOf(other.transform));
        }
    }

    public override void OnAttackTriggerEnter(Enemy_Bulldozer controller, Collider other)
    {
        if (isCharging)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(controller.chargeDamage * controller.rb.velocity.magnitude);
                Debug.Log(controller.chargeDamage * controller.rb.velocity.magnitude);
            }
        }
    }
}