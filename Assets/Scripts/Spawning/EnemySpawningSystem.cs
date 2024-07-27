using Unity.Burst;
using Unity.Entities;
using NathanThus.MassDefence.Enemy;
using Unity.Transforms;
using Unity.Mathematics;
using System.Numerics;
using UnityEngine;

namespace NathanThus.MassDefence.Spawning
{
    public partial struct EnemySpawningSystem : ISystem
    {
        private WaveConfig _waveConfig;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WaveConfig>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false; // Run Only Once.
            _waveConfig = SystemAPI.GetSingleton<WaveConfig>();
            for (int i = 0; i < _waveConfig.Enemies; i++)
            {
                state.EntityManager.Instantiate(_waveConfig.EnemyPrefab);
            }

            int posOffset = 0;

            Demo(posOffset, ref state);
        }

        private void Demo(int posOffset, ref SystemState state)
        {
            foreach (var (transform, entity) in
                    SystemAPI.Query<RefRW<LocalTransform>>()
                    .WithAll<Enemy.Enemy>()
                    .WithEntityAccess()
                    )

            {
                // Update the LocalTransform.
                transform.ValueRW.Position = new float3
                (
                    // _waveConfig.Start.x + posOffset,
                    // 0,
                    // _waveConfig.Start.z + posOffset
                    posOffset,
                    1,
                    posOffset
                );

                posOffset++;
            }

        }
    }
}
