using Infrastrucure.Components;
using Leopotam.Ecs;

namespace Infrastructure.EcsSystems.CameraSystems
{
    public class CameraInitSystem : IEcsInitSystem
    {
        private EcsFilter<Player> _playerFilter;

        private EcsWorld _ecsWorld;
        private StaticData _staticData;
        private SceneData _sceneData;

        public void Init()
        {
            EcsEntity cameraEntity = _ecsWorld.NewEntity();
            
            ref Player player = ref _playerFilter.Get1(1);
            ref var cameraComponent = ref cameraEntity.Get<CameraComponent>();
            ref var transformComponent = ref cameraEntity.Get<TransformComponent>();

            transformComponent.transform = _sceneData.mainCamera.transform;
            cameraComponent.followTransform = player.playerTransform;
        }
    }
}