using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

partial struct CarSpawnerSystem : ISystem
{
    private Unity.Mathematics.Random random;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        random = new Unity.Mathematics.Random((uint)SystemAPI.Time.ElapsedTime + 1);
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntitiesReferences entitiesReferences = SystemAPI.GetSingleton<EntitiesReferences>();

        foreach ((
            RefRO<LocalTransform> localtransform,
            RefRW<CarSpawner> carSpawner)
            in SystemAPI.Query<
                RefRO<LocalTransform>,
                RefRW<CarSpawner>>())
        {

            carSpawner.ValueRW.timer -= SystemAPI.Time.DeltaTime;
            if (carSpawner.ValueRO.timer > 0f)
            {
                continue;
            }
            carSpawner.ValueRW.timer = carSpawner.ValueRO.timerMax;

            Entity carEntity;
            Entity prefabEntity;
            int randIndex = random.NextInt(0, 3);
            if (randIndex == 0)
                prefabEntity = entitiesReferences.botCarRPrefabEntity;
            else if (randIndex == 1)
                prefabEntity = entitiesReferences.botCarGPrefabEntity;
            else
                prefabEntity = entitiesReferences.botCarBPrefabEntity;

            carEntity = state.EntityManager.Instantiate(prefabEntity);
            SystemAPI.SetComponent(carEntity, LocalTransform.FromPosition(localtransform.ValueRO.Position));


            // Assigner la route au composant BotMover de la voiture
            if (state.EntityManager.Exists(carSpawner.ValueRO.Road))
            {
                if (state.EntityManager.HasComponent<BotMover>(carEntity))
                {
                    var botMover = state.EntityManager.GetComponentData<BotMover>(carEntity);
                    botMover.Road = carSpawner.ValueRO.Road;
                    botMover.moveSpeed = carSpawner.ValueRO.botMoveSpeed;
                    botMover.rotationSpeed = carSpawner.ValueRO.botRotationSpeed;
                    state.EntityManager.SetComponentData(carEntity, botMover);
                        
                }
                else
                {
                    UnityEngine.Debug.LogWarning("CarSpawnerSystem: Spawned car does not have a BotMover component.");
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning("CarSpawnerSystem: No valid Road entity found for spawned car.");
            }
        }
    }
}
