using Leopotam.Ecs;
using UnityEngine;

public class PlayerRotationSystem : IEcsRunSystem
{
    private EcsFilter<Player> _filter;
    private SceneData _sceneData;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var player = ref _filter.Get1(i);

            Plane playerPlane = new Plane(Vector3.up, player.playerTransform.position);
            Ray ray = _sceneData.mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!playerPlane.Raycast(ray, out var hitDistance)) continue;

            player.playerTransform.forward = ray.GetPoint(hitDistance) - player.playerTransform.position;
        }
    }
}