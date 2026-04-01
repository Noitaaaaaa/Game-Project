using UnityEngine;

public class WildHogIdleState : IdleState
{
    private WildHog wildHog;

    public WildHogIdleState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_IdleState stateData,
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

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(wildHog.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}