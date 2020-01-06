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

    // This declares a new kind of job, which is a unit of work to do.
    // The job is declared as an IJobForEach<Translation, Rotation>,
    // meaning it will process all entities in the world that have both
    // Translation and Rotation components. Change it to process the component
    // types you want.
    //
    // The job is also tagged with the BurstCompile attribute, which means
    // that the Burst compiler will optimize it for the best performance.
    [BurstCompile]
    struct PlayerMoveSystemJob : IJobForEach<PhysicsVelocity, PlayerMovementData>
    {
        public TouchInputComponent inputComponent;
        public void Execute(ref PhysicsVelocity physicsVelocity, ref PlayerMovementData playerMovementData)
        {
            physicsVelocity.Linear = playerMovementData.sensitivity * playerMovementData.id == 0 ? inputComponent.keyboardPositionDelta : inputComponent.touchPositionDelta;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new PlayerMoveSystemJob();

        job.inputComponent = this.GetSingleton<TouchInputComponent>();
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}