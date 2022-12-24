using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private EcsFilter<Player, PlayerInputData> _filter;
    
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var player = ref _filter.Get1(i);
            ref var input = ref _filter.Get2(i);

            Vector3 direction = (Vector3.forward * input.MoveInput.z + Vector3.right * input.MoveInput.x).normalized;
            player.PlayerRigidbody.AddForce(direction * player.PlayerSpeed);
        }
    }
}