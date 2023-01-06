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
        ref var player = ref playerEntity.Get<PlayerComponent>();
        playerEntity.Get<PlayerInputData>();
        
        GameObject playerGo = Object.Instantiate(assetProvider.Load<GameObject>(AssetPath.Player), _sceneData.playerSpawnPoint.position, Quaternion.identity);
        player.CharacterController = playerGo.GetComponent<CharacterController>();
        player.Animator = playerGo.GetComponentInChildren<Animator>();
        player.PlayerSpeed = _staticData.playerSpeed;
        player.PlayerTransform = playerGo.transform;
        player.Hands = playerGo.GetComponentsInChildren<Hand>();
    }
}