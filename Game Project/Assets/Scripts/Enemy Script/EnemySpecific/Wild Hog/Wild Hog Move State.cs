using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildHogMoveState : MoveState
{
    private WildHog wildHog;

    public WildHogMoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_MoveState stateData,
        WildHog wildHog
    ) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wildHog = wildHog;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(wildHog.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            wildHog.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(wildHog.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        entity.SetVelocity(stateData.movementSpeed);
    }
}