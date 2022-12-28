using Infrastrucure.EcsSystems.CameraSystems;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

public class EcsStartUp : MonoBehaviour
{
    public StaticData configuration;
    public SceneData sceneData;
    
    private EcsWorld _ecsWorld;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems; 
    private EcsSystems _lateUpdateSystems; 
 
    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _updateSystems = new EcsSystems(_ecsWorld);
        _fixedUpdateSystems = new EcsSystems(_ecsWorld);
        _lateUpdateSystems = new EcsSystems(_ecsWorld);
        
        RuntimeData runtimeData = new RuntimeData();

        _updateSystems
            .ConvertScene()
            .Add(new PlayerInputSystem())
            .Add(new PlayerInitSystem())
            .Add(new PlayerRotationSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData);

        _fixedUpdateSystems
            .ConvertScene()
            .Add(new PlayerMoveSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData);

        _lateUpdateSystems
            .ConvertScene()
            .Add(new CameraInitSystem())
            .Add(new CameraFollowSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData);
        
        _updateSystems.Init();
        _fixedUpdateSystems.Init();
        _lateUpdateSystems.Init();
    }
 
    private void Update()
    {
        _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems?.Run();
    }

    private void LateUpdate()
    {
        _lateUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        _updateSystems?.Destroy();
        _updateSystems = null;
        
        _fixedUpdateSystems?.Destroy();
        _fixedUpdateSystems = null;
        
        _lateUpdateSystems?.Destroy();
        _lateUpdateSystems = null;
        
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}