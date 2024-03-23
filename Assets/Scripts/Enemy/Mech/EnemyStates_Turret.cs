using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStates_Turret
{
    public abstract void EnterState(Enemy_Turret controller);
    public abstract void ExitState(Enemy_Turret controller);
    public abstract void FrameUpdate(Enemy_Turret controller);
    public abstract void PhysicsUpdate(Enemy_Turret controller);

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

    }

    public override void OnTriggerEnter(Enemy_Turret controller, Collider other)
    {

    }

    public override void OnTriggerExit(Enemy_Turret controller, Collider other)
    {

    }
}
