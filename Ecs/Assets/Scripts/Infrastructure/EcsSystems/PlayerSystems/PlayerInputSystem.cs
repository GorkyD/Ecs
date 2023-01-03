using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsFilter<PlayerInputData> _filter;
    private EcsFilter<JoyStick> _joyStickFilter;
    
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var input = ref _filter.Get1(i);
            ref var joystick = ref _joyStickFilter.Get1(i);
            float x = joystick.UltimateJoystick.GetHorizontalAxis();
            float z = joystick.UltimateJoystick.GetVerticalAxis();
            joystick.Direction = new Vector3(x, 0, z);
            input.moveInput = joystick.Direction;
        }
    }
}