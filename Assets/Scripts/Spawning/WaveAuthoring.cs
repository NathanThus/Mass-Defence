using NathanThus.MassDefence.MapGeneration;
using Unity.Entities;
using UnityEngine;

namespace NathanThus.MassDefence.Spawning
{
    public class WaveAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _enemies;
        [SerializeField] private WaveScriptableObject _waveObject;

        private WaveConfig _waveConfig;

        class Baker : Baker<WaveAuthoring>
        {
            public override void Bake(WaveAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new WaveConfig
                {
                    WaveObject = authoring._waveObject,
                    EnemyPrefab = GetEntity(authoring._enemyPrefab, TransformUsageFlags.Dynamic),
                    Enemies = authoring._enemies
                });
            }
        }
    }

    public struct WaveConfig : IComponentData
    {
        public WaveScriptableObject WaveObject;
        public Entity EnemyPrefab;
        public int Enemies;
    }
}
