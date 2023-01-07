using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Components;
using Leopotam.Ecs;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CodeBase.Infrastructure.EcsSystems.EnemySystems
{
    public class EnemyAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent> _enemyFilter;
        private StaticData _staticData;
        
        private readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void Run()
        {
            foreach (int i in _enemyFilter)
            {
                ref var enemy = ref _enemyFilter.Get1(i);
                enemy.Animator.SetFloat(_speed,enemy.Rigidbody.velocity.magnitude);
                if (_enemyFilter.GetEntity(i).Has<AttackComponent>())
                {
                    enemy.Animator.SetFloat(_speed,0f);
                    if (!enemy.IsAttack) continue;
                    enemy.Animator.SetTrigger(Attack);
                    _enemyFilter.GetEntity(i).Del<AttackComponent>();
                    enemy.IsAttack = false;
                }
                else
                {
                    enemy.Animator.SetFloat(_speed,enemy.Rigidbody.velocity.magnitude);
                }
                
            }
        }

    }
}