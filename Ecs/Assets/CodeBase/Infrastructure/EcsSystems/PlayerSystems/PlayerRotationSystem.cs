using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerRotationSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent> _filter;
    private EcsFilter<JoyStickComponent> _joystickFilter;
    private SceneData _sceneData;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var player = ref _filter.Get1(i);
            ref var joystick = ref _joystickFilter.Get1(i);
            if (joystick.Direction != Vector3.zero)
            {
                player.PlayerTransform.forward = joystick.Direction;
            }
        }
    }
}