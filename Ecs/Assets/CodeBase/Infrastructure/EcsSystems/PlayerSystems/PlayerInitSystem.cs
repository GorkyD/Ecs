using CodeBase.Infrastructure.Services.Chunks;
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
        AssetProvider assetProvider = new AssetProvider();
        ref var player = ref playerEntity.Get<Player>();
        playerEntity.Get<PlayerInputData>();
        
        GameObject playerGo = Object.Instantiate(assetProvider.Load<GameObject>(AssetPath.Player), _sceneData.playerSpawnPoint.position, Quaternion.identity);
        player.characterController = playerGo.GetComponent<CharacterController>();
        player.animator = playerGo.GetComponentInChildren<Animator>();
        player.playerSpeed = _staticData.playerSpeed;
        player.playerTransform = playerGo.transform;
        player.hands = playerGo.GetComponentsInChildren<Hand>();
    }
}