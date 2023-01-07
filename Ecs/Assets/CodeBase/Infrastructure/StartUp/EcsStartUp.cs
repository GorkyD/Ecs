using CodeBase.Infrastructure.EcsSystems.EnemySystems;
using CodeBase.Infrastructure.Services.Chunks;
using Infrastructure.EcsSystems.AnimationSystems;
using Infrastructure.EcsSystems.CameraSystems;
using Infrastructure.EcsSystems.Joystick;
using Infrastructure.MazeGenerate;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

public class EcsStartUp : MonoBehaviour
{
    public StaticData configuration;
    public SceneData sceneData;
    
    private EcsWorld _ecsWorld;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems; 
    private EcsSystems _lateUpdateSystems;
    private EcsSystems _testSystems;

    private MazeGenerateService _mazeGenerateService;
    private MazeRendererService _mazeRendererService;

    [Inject]
    private void Construct(MazeGenerateService mazeGenerateService, MazeRendererService mazeRendererService)
    {
        _mazeGenerateService = mazeGenerateService;
        _mazeRendererService = mazeRendererService;
    }
    
    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _updateSystems = new EcsSystems(_ecsWorld);
        _fixedUpdateSystems = new EcsSystems(_ecsWorld);
        _lateUpdateSystems = new EcsSystems(_ecsWorld);
        _testSystems = new EcsSystems(_ecsWorld);
        
        RuntimeData runtimeData = new RuntimeData();

        _testSystems
            .ConvertScene()
            .Inject(_mazeGenerateService)
            .Inject(_mazeRendererService)
            .Add(new EnemyInitSystem());
            //.Add(new ChainWeaponInitSystem())
            //.Add(new ChainWeaponSystem());
            //.Add(new MazeGenerateInitSystem());
            
            
        _updateSystems
            .ConvertScene()
            .Add(new JoystickInit())
            .Add(new PlayerInitSystem())
            .Add(new PlayerRotationSystem())
            .Add(new AnimationSystem())
            .Add(new ChunkPositionerInitSystem())
            .Add(new ChunkPositionerRunSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData);
        
        _fixedUpdateSystems
            .ConvertScene()
            .Add(new PlayerInputSystem())
            .Add(new PlayerMoveSystem())
            .Add(new CameraRotationSystem())
            .Add(new CameraFollowSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData);

        _lateUpdateSystems
            .ConvertScene()
            .Add(new CameraInitSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData);
        
        _updateSystems.Init();
        _fixedUpdateSystems.Init();
        _lateUpdateSystems.Init();
        _testSystems.Init();
    }
 
    private void Update()
    {
        _updateSystems?.Run();
        _testSystems?.Run();
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
        
        _testSystems?.Destroy();
        _testSystems = null;
        
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}