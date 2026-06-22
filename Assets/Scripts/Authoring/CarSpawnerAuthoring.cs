using Unity.Entities;
using UnityEngine;

public class CarSpawnerAuthoring : MonoBehaviour
{
    public float timerMax;
    public GameObject Road;
    public float botMoveSpeed;
    public float botRotationSpeed;
    public class Baker : Baker<CarSpawnerAuthoring>
    {
        public override void Bake(CarSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CarSpawner
            {
                timerMax = authoring.timerMax,
                Road = GetEntity(authoring.Road, TransformUsageFlags.None),
                botMoveSpeed = authoring.botMoveSpeed,
                botRotationSpeed = authoring.botRotationSpeed,
            });
        }
    }
}

public struct CarSpawner : IComponentData
{
    public float timer;
    public float timerMax;

    public float botMoveSpeed;
    public float botRotationSpeed;
    public Entity Road;
}
