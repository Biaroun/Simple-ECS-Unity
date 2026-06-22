using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class BotMoverAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject Road;

    public class Baker : Baker<BotMoverAuthoring> // Définit une classe interne pour convertir le MonoBehaviour en données ECS
    {
        public override void Bake(BotMoverAuthoring authoring) // Méthode appelée pour convertir les données du MonoBehaviour en composant ECS
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); // Crée une entité ECS avec des transformations dynamiques (position, rotation, etc.)
            AddComponent(entity, new BotMover // Ajoute le composant BotMover à l'entité
            {

            });
        }
    }
}


public struct BotMover : IComponentData // Définit une structure pour le composant ECS, qui contient les données de déplacement
{
    public float moveSpeed;
    public float rotationSpeed;
    public Entity Road;
    public int currentWaypointIndex;
}
