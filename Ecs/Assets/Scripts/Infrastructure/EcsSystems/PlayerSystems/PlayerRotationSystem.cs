using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerRotationSystem : IEcsRunSystem
{
    private EcsFilter<Player> _filter;
    private EcsFilter<JoyStick> _joystickFilter;
    private SceneData _sceneData;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var player = ref _filter.Get1(i);
            ref var joystick = ref _joystickFilter.Get1(i);
            if (joystick.Direction != Vector3.zero)
            {
                player.playerTransform.forward = joystick.Direction;
            }
        }
    }
}