using CodeBase.Infrastructure.Components;
using Leopotam.Ecs;

namespace CodeBase.Infrastructure.EcsSystems.EnemySystems
{
    public class EnemyAttackSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, AttackComponent> _enemyAttackFilter;

        public void Run()
        {
            foreach (int i in _enemyAttackFilter)
            {
                ref var enemy = ref _enemyAttackFilter.Get1(i);
                enemy.IsAttack = true;
            }
        }
    }
}