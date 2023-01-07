using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure.EcsSystems.AnimationSystems
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent,PlayerInputData> _playerFilter;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref PlayerComponent playerComponent = ref _playerFilter.Get1(i);
                ref PlayerInputData playerInputData = ref _playerFilter.Get2(i);
                
                playerComponent.Animator.SetFloat(Speed,playerInputData.MoveInput.magnitude);

            }
        }
    }
}