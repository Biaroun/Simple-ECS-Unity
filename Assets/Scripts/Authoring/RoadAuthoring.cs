using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

//Définition du buffer element pour stocker les points
public struct RoadPoint : IBufferElementData
{
    public float3 Position;
}

public class RoadAuthoring : MonoBehaviour
{
    public Transform[] Waypoints;

    // Dessin dans la scène pour visualiser la route
    private void OnDrawGizmos()
    {
        if (Waypoints == null || Waypoints.Length < 2)
            return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < Waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(Waypoints[i].position, Waypoints[i + 1].position);
        }
    }

    public class RoadBaker : Baker<RoadAuthoring>
    {
        public override void Bake(RoadAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            // Ajout du buffer pour stocker les points
            DynamicBuffer<RoadPoint> buffer = AddBuffer<RoadPoint>(entity);

            // Conversion des waypoints Unity en float3 ECS
            foreach (var waypoint in authoring.Waypoints)
            {
                buffer.Add(new RoadPoint { Position = waypoint.position });
            }
        }
    }
}


public struct Road : IComponentData
{

}
