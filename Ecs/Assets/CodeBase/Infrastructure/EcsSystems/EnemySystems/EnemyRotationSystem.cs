using CodeBase.Infrastructure.Components;
using Infrastrucure.Components;
using Leopotam.Ecs;

namespace CodeBase.Infrastructure.EcsSystems.EnemySystems
{
    public class EnemyRotationSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent,EnemyComponent> _enemyFilter;
        private EcsFilter<PlayerComponent> _playerFilter;

        public void Run()
        {
            foreach (int i in _enemyFilter)
            {
                ref var player = ref _playerFilter.Get1(1);
                ref var enemyTransform = ref _enemyFilter.Get1(i);
                enemyTransform.Transform.LookAt(player.PlayerTransform);
            }
        }
    }
}