using Unity.Entities;
using UnityEngine;

namespace NathanThus.MassDefence.Enemy
{
    public class EnemyAuthoring : MonoBehaviour
    {
        class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new Enemy{});
            }
        }
    }

    public struct Enemy : IComponentData
    {
    }
}
