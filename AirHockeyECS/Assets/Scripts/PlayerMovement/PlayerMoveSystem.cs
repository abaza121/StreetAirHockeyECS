using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using static Unity.Mathematics.math;
using UnityEngine;

public class PlayerMoveSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        this.RequireSingletonForUpdate<TouchInputComponent>();
    }

    [BurstCompile]
    struct PlayerMoveSystemJob : IJobForEach<PhysicsVelocity, PlayerMovementData>
    {
        public TouchInputComponent inputComponent;
        public void Execute(ref PhysicsVelocity physicsVelocity, ref PlayerMovementData playerMovementData)
        {
            physicsVelocity.Linear = playerMovementData.sensitivity * playerMovementData.id == 0 ? inputComponent.player1Input : inputComponent.player2Input;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new PlayerMoveSystemJob();
        job.inputComponent = this.GetSingleton<TouchInputComponent>();
        return job.Schedule(this, inputDependencies);
    }
}