using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class CarMoverAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    public class Baker : Baker<CarMoverAuthoring>
    {
        public override void Bake(CarMoverAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CarMover
            {
                moveSpeed = authoring.moveSpeed,
                rotationSpeed = authoring.rotationSpeed,
            });
        }
    }
}


public struct CarMover : IComponentData
{
    public float moveSpeed;
    public float rotationSpeed;
    public float3 targetPosition;
}
