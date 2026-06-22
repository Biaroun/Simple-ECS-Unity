using Unity.Entities;
using UnityEngine;

public class EntitiesReferencesAuthoring : MonoBehaviour
{
    public GameObject botCarRPrefabGameObject;
    public GameObject botCarGPrefabGameObject;
    public GameObject botCarBPrefabGameObject;
    public class Baker : Baker<EntitiesReferencesAuthoring>
    {
        public override void Bake(EntitiesReferencesAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntitiesReferences
            {
                botCarRPrefabEntity = GetEntity(authoring.botCarRPrefabGameObject, TransformUsageFlags.Dynamic),
                botCarGPrefabEntity = GetEntity(authoring.botCarGPrefabGameObject, TransformUsageFlags.Dynamic),
                botCarBPrefabEntity = GetEntity(authoring.botCarBPrefabGameObject, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct EntitiesReferences : IComponentData
{
    public Entity botCarRPrefabEntity;
    public Entity botCarGPrefabEntity;
    public Entity botCarBPrefabEntity;
}
