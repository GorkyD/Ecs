using Infrastrucure.Components;
using Leopotam.Ecs;

namespace Infrastructure.EcsSystems.CameraSystems
{
    public class CameraRotationSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;

        private EcsFilter<TransformComponent, CameraComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var cameraTransform = ref _filter.Get1(i);
                ref var cameraComponent = ref _filter.Get2(i);

                cameraTransform.Transform.LookAt(cameraComponent.FollowTransform.position);
            }
        }
    }
}