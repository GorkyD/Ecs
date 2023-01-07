using System.Collections.Generic;
using CodeBase.Infrastructure.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.EcsSystems.EnemySystems
{
    public class EnemyInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<EnemyComponent> _filter;
        
        public void Init()
        {
            EcsEntity enemy = _ecsWorld.NewEntity();
            AssetProvider assetProvider = new AssetProvider();
            ref var enemyComponent = ref enemy.Get<EnemyComponent>();
            GameObject enemyGameObject = Object.Instantiate(assetProvider.Load<GameObject>(AssetPath.Enemy));
            enemyGameObject.SetActive(false);
            enemyComponent.EnemyPrefab = enemyGameObject;
            enemyComponent.EnemyPool = new Stack<GameObject>();
            
            FillEnemyPool();
        }
        
        private void FillEnemyPool()
        {
            for (int i = 0; i < 15; i++)
            {
                GameObject enemy = Object.Instantiate(_filter.Get1(1).EnemyPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                enemy.SetActive(false);
                _filter.Get1(1).EnemyPool.Push(enemy);
            }
        }
    }
}