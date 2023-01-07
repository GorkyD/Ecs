using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsFilter<PlayerInputData> _filter;
    private EcsFilter<JoyStickComponent> _joyStickFilter;

    private float _xAxis;
    private float _zAxis;
    
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var input = ref _filter.Get1(i);
            ref var joystick = ref _joyStickFilter.Get1(i);
            
            _xAxis = joystick.UltimateJoystick.GetHorizontalAxis();
            _zAxis = joystick.UltimateJoystick.GetVerticalAxis();

            joystick.Direction = new Vector3(_xAxis, 0f, _zAxis);
            input.MoveInput = joystick.Direction;
        }
    }
}