using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class CarMoverManager : MonoBehaviour //met à jour targetPosition de toutes les entités possédant un composant CarMover
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = MouseWorldPosition.Instance.GetPosition();

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<CarMover>().Build(entityManager);

            NativeArray<Entity> entityArray = entityQuery.ToEntityArray(Allocator.Temp);

            NativeArray<CarMover> carMoverArray = entityQuery.ToComponentDataArray<CarMover>(Allocator.Temp);
            for (int i = 0; i < carMoverArray.Length; i++)
            {
                CarMover carMover = carMoverArray[i];
                carMover.targetPosition = mouseWorldPosition;
                carMoverArray[i] = carMover;
            }
            entityQuery.CopyFromComponentDataArray(carMoverArray);
        }
    }
}
