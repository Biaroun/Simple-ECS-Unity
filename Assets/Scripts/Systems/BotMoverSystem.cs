
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using System.ComponentModel;
using Unity.Collections;

partial struct BotMoverSystem : ISystem
{
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        BotMoverJob botMoverJob = new BotMoverJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            roadLookup = SystemAPI.GetBufferLookup<RoadPoint>(true),
            ecb = ecb.AsParallelWriter()
        };
        botMoverJob.ScheduleParallel();

        
        state.Dependency.Complete(); // attendre la fin du job
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

[BurstCompile]
public partial struct BotMoverJob : IJobEntity 
{
    public float deltaTime;
    [Unity.Collections.ReadOnly] public BufferLookup<RoadPoint> roadLookup;
    public EntityCommandBuffer.ParallelWriter ecb;

    public void Execute([EntityIndexInQuery] int entityInQueryIndex,
                        Entity entity,
                        ref LocalTransform localTransform,
                        ref BotMover botMover,
                        ref PhysicsVelocity physicsVelocity)
    {
        if (!roadLookup.HasBuffer(botMover.Road))
            return;

        DynamicBuffer<RoadPoint> routeBuffer = roadLookup[botMover.Road];
        if (botMover.currentWaypointIndex >= routeBuffer.Length)
        {
            // Plus de points → stop
            ecb.DestroyEntity(entityInQueryIndex, entity);
            return;
        }

        // Prochaine cible
        float3 targetPosition = routeBuffer[botMover.currentWaypointIndex].Position;
        float3 moveDirection = targetPosition - localTransform.Position;

        float reachedTargetDistanceSq = 2f;
        if (math.lengthsq(moveDirection) < reachedTargetDistanceSq)
        {
            physicsVelocity.Linear = float3.zero;
            physicsVelocity.Angular = float3.zero;
            botMover.currentWaypointIndex++;
            return;
        }

        moveDirection = math.normalize(moveDirection);

        localTransform.Rotation =
            math.slerp(localTransform.Rotation,
                        quaternion.LookRotation(moveDirection, math.up()),
                        deltaTime * botMover.rotationSpeed);

        physicsVelocity.Linear = moveDirection * botMover.moveSpeed;
        physicsVelocity.Angular = float3.zero;
    }
}
