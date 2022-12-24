using Leopotam.Ecs;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private StaticData _staticData;
    private SceneData _sceneData;

    public void Init()
    {
        EcsEntity playerEntity = _ecsWorld.NewEntity();
        
        ref var player = ref playerEntity.Get<Player>();
        ref var inputData = ref playerEntity.Get<PlayerInputData>();
        
        GameObject playerGo = Object.Instantiate(_staticData.playerPrefab, _sceneData.playerSpawnPoint.position, Quaternion.identity);
        player.PlayerRigidbody = playerGo.GetComponent<Rigidbody>();
        player.PlayerSpeed = _staticData.playerSpeed;
        player.PlayerTransform = playerGo.transform; 
    }
}