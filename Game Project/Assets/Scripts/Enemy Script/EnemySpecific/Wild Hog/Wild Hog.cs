using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildHog : Entity
{
    public WildHogIdleState idleState { get; private set; }
    public WildHogMoveState moveState { get; private set; }
    public WildHogPlayerDetectedState playerDetectedState { get; private set; }
    public WildHogChargeState chargestate { get; private set;}
    public WildHogLookForPlayerState lookForPlayerState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;

    public override void Start()
    {
        base.Start();

        idleState = new WildHogIdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new WildHogMoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new WildHogPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargestate = new WildHogChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new WildHogLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);

        stateMachine.Initialize(moveState);
        
    }
}