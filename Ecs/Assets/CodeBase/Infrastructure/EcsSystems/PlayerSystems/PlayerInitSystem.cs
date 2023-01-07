using CodeBase.Infrastructure.Components;
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
        ref var playerHealth = ref playerEntity.Get<HealthComponent>();
        playerEntity.Get<PlayerInputData>();
        
        GameObject playerGameObject = Object.Instantiate(assetProvider.Load<GameObject>(AssetPath.Player), _sceneData.playerSpawnPoint.position, Quaternion.identity);
        player.Animator = playerGameObject.GetComponentInChildren<Animator>();
        player.Rigidbody = playerGameObject.GetComponent<Rigidbody>();
        player.PlayerSpeed = _staticData.playerSpeed;
        player.PlayerTransform = playerGameObject.transform;
        player.Hands = playerGameObject.GetComponentsInChildren<Hand>();
        playerHealth.Health = _staticData.playerHealth;
    }
}