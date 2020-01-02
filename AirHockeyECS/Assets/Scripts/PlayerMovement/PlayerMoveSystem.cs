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
        public float3 vector;
        public void Execute(ref PhysicsVelocity physicsVelocity, ref PlayerMovementData playerMovementData)
        {
            physicsVelocity.Linear += playerMovementData.sensitivity * vector;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        this.RequireSingletonForUpdate<TouchInputComponent>();
        var job = new PlayerMoveSystemJob();

        // Assign values to the fields on your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        //     job.deltaTime = UnityEngine.Time.deltaTime;

        job.vector = this.GetSingleton<TouchInputComponent>().touchPositionDelta;
        
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}