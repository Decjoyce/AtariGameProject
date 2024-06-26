using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum TurretStates
{
    idle,
    aggro
}

public abstract class EnemyStates_Turret
{
    public abstract void EnterState(Enemy_Turret controller);
    public abstract void ExitState(Enemy_Turret controller);
    public abstract void FrameUpdate(Enemy_Turret controller);
    public abstract void PhysicsUpdate(Enemy_Turret controller);

    public virtual void OnRoomEnter(Enemy_Turret controller, GameObject player) { }
    public virtual void OnRoomExit(Enemy_Turret controller, GameObject player) { }

    public virtual void OnTriggerEnter(Enemy_Turret controller, Collider other) { }
    public virtual void OnTriggerExit(Enemy_Turret controller, Collider other) { }
}

public class TurretState_Idle : EnemyStates_Turret
{
    public override void EnterState(Enemy_Turret controller)
    {


    }

    public override void ExitState(Enemy_Turret controller)
    {

    }

    public override void FrameUpdate(Enemy_Turret controller)
    {

    }

    public override void PhysicsUpdate(Enemy_Turret controller)
    {
/*        if(controller.x_pivot.localRotation.x != 0 || controller.y_pivot.localRotation.y != 0)
        {        
            float newYAngle = Mathf.LerpAngle(controller.y_pivot.localEulerAngles.y, 0, 0.005f);
            controller.y_pivot.rotation = Quaternion.Euler(0, newYAngle, 0);
            float newXAngle = Mathf.LerpAngle(controller.x_pivot.localEulerAngles.x, 0, 0.005f);
            controller.x_pivot.rotation = Quaternion.Euler(newXAngle, 0, 0);
        }*/
    }

    public override void OnRoomEnter(Enemy_Turret controller, GameObject player)
    {
            controller.SwitchState("AGGRO");
    }

    public override void OnRoomExit(Enemy_Turret controller, GameObject player)
    {

    }

    public override void OnTriggerEnter(Enemy_Turret controller, Collider other)
    {
/*        if (other.CompareTag("Player"))
        {
            controller.SwitchState("AGGRO");
        }*/
    }

    public override void OnTriggerExit(Enemy_Turret controller, Collider other)
    {

    }
}

public class TurretState_Aggro : EnemyStates_Turret
{
    bool canShoot;
    float shootDelay;

    public override void EnterState(Enemy_Turret controller)
    {
        shootDelay = 0;
        canShoot = true;
    }

    public override void ExitState(Enemy_Turret controller)
    {

    }

    public override void FrameUpdate(Enemy_Turret controller)
    {
        //Need to fix it so it doesnt rotate on y axis when player is on same lane
        if (controller.currentTarget != null)
        {
            if (shootDelay <= 0)
            {
                RaycastHit hit;
                if(Physics.Raycast(controller.firePoint.position, controller.firePoint.up, out hit, Mathf.Infinity, controller.ignoreLayers))
                {
                    Debug.Log("HIT " + hit.transform.name);
                    if (hit.transform.CompareTag("Player"))
                    {
                        controller.HelpInstantiate(controller.firePoint.position, controller.firePoint.rotation);
                        shootDelay = controller.shoot_delay;
                        controller.source.PlayOneShot(controller.shootClip);
                    }
                }
            }
            else
                shootDelay -= Time.deltaTime;
        }
    }

    public override void PhysicsUpdate(Enemy_Turret controller)
    {
        if (controller.currentTarget)
        {


            if (!controller.wallMounted)
            {            
                Vector3 direction = (controller.currentTarget.transform.position + (Vector3.up * 2f)) - controller.y_pivot.position;
                Vector3 rot = Quaternion.LookRotation(direction).eulerAngles;

                float newYAngle = Mathf.LerpAngle(controller.y_pivot.localEulerAngles.y, rot.y, controller.turnSpeed);
                controller.y_pivot.rotation = Quaternion.Euler(0, newYAngle, 0);

                controller.x_pivot.localRotation = Quaternion.Euler(rot.x + 90, 0, 0);
            }
            else
            {
                Vector3 direction = (controller.currentTarget.transform.position + (Vector3.up * 0.5f)) - controller.y_pivot.position;
                Vector3 rot = Quaternion.LookRotation(direction).eulerAngles;
                controller.x_pivot.localRotation = Quaternion.Euler(rot.x, 0, 0);
            }
        }
    }

    public override void OnRoomEnter(Enemy_Turret controller, GameObject player)
    {
        controller.NextTarget();
    }

    public override void OnRoomExit(Enemy_Turret controller, GameObject player)
    {
        if (controller.targets.Contains(player.transform))
            controller.targets.RemoveAt(controller.targets.IndexOf(player.transform));
        if (controller.currentTarget = player.transform)
        {
            controller.NextTarget();
        }
    }

    public override void OnTriggerEnter(Enemy_Turret controller, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.NextTarget();
        }
    }

    public override void OnTriggerExit(Enemy_Turret controller, Collider other)
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
