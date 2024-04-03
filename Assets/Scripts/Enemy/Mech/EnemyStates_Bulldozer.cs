using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulldozerStates
{
    idle,
    aggro,
    windup,
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
        Debug.Log("Idle");
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
        controller.SwitchState("WINDUP");
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
        canCharge = true;
        controller.FaceSomething(controller.currentTarget.position);
        Debug.Log("Aggro");
    }

    public override void ExitState(Enemy_Bulldozer controller)
    {

    }

    public override void FrameUpdate(Enemy_Bulldozer controller)
    {
        if(canCharge && chargeDelay <= 0)
        {
            controller.FaceSomething(controller.currentTarget.position);
            controller.SwitchState("WINDUP");
            canCharge = false;
        }
        else
        {
            chargeDelay -= Time.deltaTime;
        }
    }

    public override void PhysicsUpdate(Enemy_Bulldozer controller)
    {
        if(chargeDelay > 0)
        {
            if(controller.currentTarget.position.z == controller.transform.position.z)
            {
                Debug.Log("oooh");
                controller.FaceSomething(controller.currentTarget.position);
                Vector3 vel = controller.transform.right * controller.speed * Time.fixedDeltaTime;
                controller.rb.MovePosition(controller.rb.position + vel);
            }
            
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
}

public class BulldozerState_Windup : EnemyStates_Bulldozer
{
    bool canCharge;
    float windupDelay;
    float chargeDelay;

    public override void EnterState(Enemy_Bulldozer controller)
    {
        windupDelay = 2f;
        canCharge = true;
        chargeDelay = 2f;
        controller.infoText.text = "Winding Up";
    }

    public override void ExitState(Enemy_Bulldozer controller)
    {

    }

    public override void FrameUpdate(Enemy_Bulldozer controller)
    {
        if (canCharge && windupDelay <= 0)
        {
            if(chargeDelay > 0)
            {
                controller.infoText.text = "Ready to Charge";
                RaycastHit hit;
                if (Physics.Raycast(controller.firePoint.position, controller.firePoint.right, out hit, Mathf.Infinity, controller.ignoreLayers))
                {
                    Debug.Log("HIT " + hit.transform.name);
                    if (hit.transform.CompareTag("Player"))
                    {
                        controller.SwitchState("CHARGE");
                    }
                }
                chargeDelay -= Time.deltaTime;
            }
            else
            {
                controller.SwitchState("CHARGE");
            }
        }
        else
        {
            windupDelay -= Time.deltaTime;
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

    public override void OnAttackTriggerEnter(Enemy_Bulldozer controller, Collider other)
    {
    
    }
}

public class BulldozerState_Charge : EnemyStates_Bulldozer
{
    float chargingTime;

    public override void EnterState(Enemy_Bulldozer controller)
    {
        chargingTime = 3f;
        controller.SetAttackTrigger(true);
    }

    public override void ExitState(Enemy_Bulldozer controller)
    {

    }

    public override void FrameUpdate(Enemy_Bulldozer controller)
    {
        if(chargingTime <= 0)
        {
            controller.SwitchState("AGGRO");
            controller.SetAttackTrigger(false);
        }
        else
        {
            chargingTime -= Time.deltaTime;
        }
    }

    public override void PhysicsUpdate(Enemy_Bulldozer controller)
    {
        Vector3 vel = controller.transform.right * controller.chargeForce;
        controller.rb.AddForce(vel, ForceMode.Acceleration);
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
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(controller.chargeDamage * controller.rb.velocity.magnitude);
            Debug.Log(controller.chargeDamage * controller.rb.velocity.magnitude);
        }
    }
}