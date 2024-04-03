using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SecurityBotStates
{
    idle,
    machineGun,
    railgun,
    beamGun,
}
public abstract class EnemyStates_SecurityBot
{
    public abstract void EnterState(Enemy_SecurityBot controller);
    public abstract void ExitState(Enemy_SecurityBot controller);
    public abstract void FrameUpdate(Enemy_SecurityBot controller);
    public abstract void PhysicsUpdate(Enemy_SecurityBot controller);

    public virtual void OnRoomEnter(Enemy_SecurityBot controller, GameObject player) { }
    public virtual void OnRoomExit(Enemy_SecurityBot controller, GameObject player) { }

    public virtual void OnTriggerEnter(Enemy_SecurityBot controller, Collider other) { }
    public virtual void OnTriggerExit(Enemy_SecurityBot controller, Collider other) { }
}

public class SecurityBot_Idle : EnemyStates_SecurityBot
{
    public override void EnterState(Enemy_SecurityBot controller)
    {
        
    }

    public override void ExitState(Enemy_SecurityBot controller)
    {
        
    }

    public override void FrameUpdate(Enemy_SecurityBot controller)
    {
        
    }

    public override void OnRoomEnter(Enemy_SecurityBot controller, GameObject player)
    {
        controller.GetRandomState();
    }

    public override void OnRoomExit(Enemy_SecurityBot controller, GameObject player)
    {
        
    }

    public override void OnTriggerEnter(Enemy_SecurityBot controller, Collider other)
    {
        
    }

    public override void OnTriggerExit(Enemy_SecurityBot controller, Collider other)
    {
        
    }

    public override void PhysicsUpdate(Enemy_SecurityBot controller)
    {
        
    }
}

public class SecurityBot_MachineGun : EnemyStates_SecurityBot
{
    float timeBeforeNextShot;

    public override void EnterState(Enemy_SecurityBot controller)
    {
        controller.StartRandomStateDelay();

        timeBeforeNextShot = controller.machineGunAttackSpeed;
    }

    public override void ExitState(Enemy_SecurityBot controller)
    {

    }

    public override void FrameUpdate(Enemy_SecurityBot controller)
    {
        if(timeBeforeNextShot <= 0f)
        {
            controller.ShootMachineGun();
            timeBeforeNextShot = controller.machineGunAttackSpeed;
        }
        else
        {
            timeBeforeNextShot -= Time.deltaTime;
        }
    }

    public override void OnRoomEnter(Enemy_SecurityBot controller, GameObject player)
    {

    }

    public override void OnRoomExit(Enemy_SecurityBot controller, GameObject player)
    {

    }

    public override void OnTriggerEnter(Enemy_SecurityBot controller, Collider other)
    {

    }

    public override void OnTriggerExit(Enemy_SecurityBot controller, Collider other)
    {

    }

    public override void PhysicsUpdate(Enemy_SecurityBot controller)
    {

    }
}

public class SecurityBot_Railgun : EnemyStates_SecurityBot
{
    float timeBeforeNextShot;

    public override void EnterState(Enemy_SecurityBot controller)
    {
        controller.StartRandomStateDelay();

        timeBeforeNextShot = controller.railgunAttackSpeed;
    }

    public override void ExitState(Enemy_SecurityBot controller)
    {

    }

    public override void FrameUpdate(Enemy_SecurityBot controller)
    {
        if (timeBeforeNextShot <= 0f)
        {
            controller.ShootRailgun();
            timeBeforeNextShot = controller.railgunAttackSpeed;
        }
        else
        {
            timeBeforeNextShot -= Time.deltaTime;
        }
    }

    public override void OnRoomEnter(Enemy_SecurityBot controller, GameObject player)
    {

    }

    public override void OnRoomExit(Enemy_SecurityBot controller, GameObject player)
    {

    }

    public override void OnTriggerEnter(Enemy_SecurityBot controller, Collider other)
    {

    }

    public override void OnTriggerExit(Enemy_SecurityBot controller, Collider other)
    {

    }

    public override void PhysicsUpdate(Enemy_SecurityBot controller)
    {

    }
}

public class SecurityBot_BeamGun : EnemyStates_SecurityBot
{
    float timeBeforeNextShot;

    public override void EnterState(Enemy_SecurityBot controller)
    {
        controller.StartRandomStateDelay();

        timeBeforeNextShot = controller.beamGunAttackSpeed;
    }

    public override void ExitState(Enemy_SecurityBot controller)
    {

    }

    public override void FrameUpdate(Enemy_SecurityBot controller)
    {
        if (timeBeforeNextShot <= 0f)
        {
            controller.ShootBeamGun();
            timeBeforeNextShot = controller.beamGunAttackSpeed;
        }
        else
        {
            timeBeforeNextShot -= Time.deltaTime;
        }
    }

    public override void OnRoomEnter(Enemy_SecurityBot controller, GameObject player)
    {

    }

    public override void OnRoomExit(Enemy_SecurityBot controller, GameObject player)
    {

    }

    public override void OnTriggerEnter(Enemy_SecurityBot controller, Collider other)
    {

    }

    public override void OnTriggerExit(Enemy_SecurityBot controller, Collider other)
    {

    }

    public override void PhysicsUpdate(Enemy_SecurityBot controller)
    {

    }
}
