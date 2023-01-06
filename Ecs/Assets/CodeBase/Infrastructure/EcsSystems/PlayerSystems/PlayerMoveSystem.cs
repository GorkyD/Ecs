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

            Vector3 smoothMoveVelocity = new Vector3();
            Vector3 moveAmount = new Vector3();
            Vector3 targetMoveAmount = input.moveInput * player.playerSpeed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
            player.characterController.Move(moveAmount);
        }
    }
}