using UnityEngine;

public class WildHogChargeState : ChargeState
{
    private WildHog wildHog;
    public WildHogChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, WildHog wildHog) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wildHog = wildHog;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if(!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(wildHog.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(wildHog.playerDetectedState);
            }
            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
