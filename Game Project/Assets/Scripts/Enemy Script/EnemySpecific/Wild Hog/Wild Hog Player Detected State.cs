using UnityEngine;

public class WildHogPlayerDetectedState : PlayerDetectedState
{
    private WildHog wildHog;

    public WildHogPlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, WildHog wildHog) : base(entity, stateMachine, animBoolName, stateData)
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

        Debug.Log("Player Detected State Active");

        if (performLongRangeAction)
        {
            stateMachine.ChangeState(wildHog.chargestate);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(wildHog.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
