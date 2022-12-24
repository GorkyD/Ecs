using System;
using Leopotam.Ecs;
using UnityEngine;

public class EcsStartUp : MonoBehaviour
{
    public StaticData configuration;
    public SceneData sceneData;
    
    private EcsWorld _ecsWorld;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems; 
 
    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _updateSystems = new EcsSystems(_ecsWorld);
        _fixedUpdateSystems = new EcsSystems(_ecsWorld);
        
        RuntimeData runtimeData = new RuntimeData();

        _updateSystems
            .Add(new PlayerInitSystem())
            .Add(new PlayerRotationSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData);

        _fixedUpdateSystems
            .Add(new PlayerMoveSystem()); 
        
        _updateSystems.Init();
        _fixedUpdateSystems.Init();
    }
 
    private void Update()
    {
        _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        _updateSystems?.Destroy();
        _updateSystems = null;
        
        _fixedUpdateSystems?.Destroy();
        _fixedUpdateSystems = null;
        
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}