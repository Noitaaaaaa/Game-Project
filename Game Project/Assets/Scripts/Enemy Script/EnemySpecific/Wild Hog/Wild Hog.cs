using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildHog : Entity
{
    public WildHogIdleState idleState { get; private set; }
    public WildHogMoveState moveState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();

        idleState = new WildHogIdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new WildHogMoveState(this, stateMachine, "move", moveStateData, this);

        stateMachine.Initialize(idleState); // 🔥 REQUIRED
    }
}