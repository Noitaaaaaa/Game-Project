using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildHog : Entity
{
    public WildHogIdleState idleState { get; private set; }
    public WildHogMoveState moveState { get; private set; }
    public WildHogPlayerDetectedState playerDetectedState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedData;

    public override void Start()
    {
        base.Start();

        idleState = new WildHogIdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new WildHogMoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new WildHogPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);

        stateMachine.Initialize(moveState);
        stateMachine.Initialize(idleState); 
    }
}