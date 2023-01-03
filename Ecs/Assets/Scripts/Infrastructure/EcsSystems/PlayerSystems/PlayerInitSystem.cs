using Infrastrucure.Components;
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
        playerEntity.Get<PlayerInputData>();
        
        GameObject playerGo = Object.Instantiate(_staticData.playerPrefab, _sceneData.playerSpawnPoint.position, Quaternion.identity);
        player.characterController = playerGo.GetComponent<CharacterController>();
        player.playerSpeed = _staticData.playerSpeed;
        player.playerTransform = playerGo.transform;
    }
}