
using CodeBase.Infrastructure.Components;
using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.EcsSystems.EnemySystems
{
    public class EnemyInitSystem : IEcsInitSystem
    {
        private EcsFilter<EnemyComponent> _filter;
        private StaticData _staticData;
        private EcsWorld _ecsWorld;

        public void Init()
        {
            AssetProvider assetProvider = new AssetProvider();
            GameObject enemyReference = assetProvider.Load<GameObject>(AssetPath.Enemy);
            for (int i = 0; i < 15; i++)
            {   
                EcsEntity enemyEntity = _ecsWorld.NewEntity();
                ref var enemyComponent = ref enemyEntity.Get<EnemyComponent>();
                ref var enemyTransform = ref enemyEntity.Get<TransformComponent>();
                ref var enemyHealth = ref enemyEntity.Get<HealthComponent>();
                GameObject enemyGameObject = Object.Instantiate(enemyReference, new Vector3(0f, 0f, 0f), Quaternion.identity);
                enemyGameObject.SetActive(false);
                enemyComponent.Animator = enemyGameObject.GetComponent<Animator>();
                enemyComponent.Rigidbody = enemyGameObject.GetComponent<Rigidbody>();
                enemyTransform.Transform = enemyGameObject.transform;
                enemyHealth.Health = _staticData.enemyHealth;
            }
        }
    }
}