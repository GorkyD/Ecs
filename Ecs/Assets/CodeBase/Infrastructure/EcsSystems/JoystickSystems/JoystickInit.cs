using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure.EcsSystems.Joystick
{
    public class JoystickInit : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;
        private StaticData _staticData;

        public void Init()
        {
            EcsEntity joystickEntity = _ecsWorld.NewEntity();
            AssetProvider assetProvider = new AssetProvider();
            ref var joyStick = ref joystickEntity.Get<JoyStickComponent>();
            GameObject joyStickGameObject = Object.Instantiate(assetProvider.Load<GameObject>(AssetPath.JoyStick),_sceneData.mainCanvas.transform);
            joyStick.UltimateJoystick = joyStickGameObject.GetComponent<UltimateJoystick>();
        }
    }
}