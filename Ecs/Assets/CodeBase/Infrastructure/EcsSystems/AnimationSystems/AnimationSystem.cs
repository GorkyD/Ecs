using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure.EcsSystems.AnimationSystems
{
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Player,PlayerInputData> _playerFilter;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref Player player = ref _playerFilter.Get1(i);
                ref PlayerInputData playerInputData = ref _playerFilter.Get2(i);
                
                player.animator.SetFloat(Speed,playerInputData.moveInput.magnitude);

            }
        }
    }
}