using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsFilter<PlayerInputData> _filter;
  
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var input = ref _filter.Get1(i);
            input.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
    }
}