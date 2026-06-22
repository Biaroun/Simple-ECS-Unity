using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

partial struct CarMoverSystem : ISystem 
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        carMoverJob carMoverJob = new carMoverJob
        {
            deltaTime = SystemAPI.Time.DeltaTime, 
        };
        carMoverJob.ScheduleParallel(); 
    }
}

[BurstCompile]
public partial struct carMoverJob : IJobEntity
{
    public float deltaTime;

    public void Execute(ref LocalTransform localTransform, in CarMover carMover, ref PhysicsVelocity physicsVelocity)
    {
        float3 moveDirection = carMover.targetPosition - localTransform.Position; 

        float reachedTargetDistanceSq = 2f;
        if (math.lengthsq(moveDirection) < reachedTargetDistanceSq)
        {
            physicsVelocity.Linear = float3.zero; 
            physicsVelocity.Angular = float3.zero;
            return;
        }

        moveDirection = math.normalize(moveDirection); 


        localTransform.Rotation =
            math.slerp(localTransform.Rotation, // Rotation actuelle
                        quaternion.LookRotation(moveDirection, math.up()),
                        deltaTime * carMover.rotationSpeed); 

        physicsVelocity.Linear = moveDirection * carMover.moveSpeed;
        physicsVelocity.Angular = float3.zero;
    }
}
