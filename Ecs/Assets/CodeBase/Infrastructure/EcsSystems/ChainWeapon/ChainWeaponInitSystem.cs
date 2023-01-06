using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure.EcsSystems.ChainWeapon
{
    public class ChainWeaponInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<Player> _playerFilter;

        public void Init()
        {
            AssetProvider assetProvider = new AssetProvider();
            ref var playerComponent = ref _playerFilter.Get1(1);
            foreach (var hand in playerComponent.hands)
            {
                EcsEntity weaponEntity = _ecsWorld.NewEntity();
                ref var chainWeapon = ref weaponEntity.Get<ChainWeaponComponent>();
                GameObject chainWeaponGo = Object.Instantiate(assetProvider.Load<GameObject>(AssetPath.ChainWeapon),hand);
                chainWeapon.ChainLine = chainWeaponGo.GetComponentInChildren<LineRenderer>();
                //chainWeapon.ChainPoints = new[] { };
                chainWeapon.WeaponController = chainWeaponGo.transform.GetChild(0).GetChild(1);
                chainWeapon.WeaponModel = chainWeapon.WeaponController.GetChild(1);
            }
        }
    }
}