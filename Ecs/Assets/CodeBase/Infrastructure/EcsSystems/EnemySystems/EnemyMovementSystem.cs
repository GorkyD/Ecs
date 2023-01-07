using CodeBase.Infrastructure.Components;
using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.EcsSystems.EnemySystems
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent,EnemyComponent> _enemyFilter;
        private EcsFilter<PlayerComponent> _playerFilter;
        private StaticData _staticData;
        
        public void Run()
        {
            foreach (int i in _enemyFilter)
            {
                ref var enemyComponent = ref _enemyFilter.Get2(i);
                ref var enemyTransform = ref _enemyFilter.Get1(i);
                Vector3 targetPosition = _playerFilter.Get1(1).PlayerTransform.position;
                
                if (Vector3.Distance(enemyTransform.Transform.position, targetPosition) <= _staticData.distanceToStop)
                {
                    _enemyFilter.GetEntity(i).Get<AttackComponent>();
                }
                else
                {
                    Vector3 direction = (targetPosition - enemyTransform.Transform.position).normalized * _staticData.enemySpeed;
                    enemyComponent.Rigidbody.velocity = direction;
                }
            }
        }
    }
}