using Unity.Entities;
using UnityEngine;

namespace NathanThus.MassDefence.Spawning
{
    public class WaveAuthoring : MonoBehaviour
    {
        // [SerializeField] private Transform _start;
        // [SerializeField] private Transform _goal;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _enemies;

        class Baker : Baker<WaveAuthoring>
        {
            public override void Bake(WaveAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new WaveConfig
                {
                    // Start = authoring._start.position,
                    // Goal = authoring._start.position,
                    EnemyPrefab = GetEntity(authoring._enemyPrefab, TransformUsageFlags.Dynamic),
                    Enemies = authoring._enemies
                });
            }
        }
    }

    public struct WaveConfig : IComponentData
    {
        public Vector3 Start;
        public Vector3 Goal;
        public Entity EnemyPrefab;
        public int Enemies;
    }
}
