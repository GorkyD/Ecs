using Infrastrucure.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure.EcsSystems.Joystick
{
    public class JoystickInit : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;

        public void Init()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                EcsEntity joystickEntity = _ecsWorld.NewEntity();
                AssetProvider assetProvider = new AssetProvider();
                ref var joyStick = ref joystickEntity.Get<JoyStick>();
                GameObject joyStickGameObject = Object.Instantiate(assetProvider.Load<GameObject>(AssetPath.JoyStick),_sceneData.mainCanvas.transform);
                joyStick.UltimateJoystick = joyStickGameObject.GetComponent<UltimateJoystick>();
            }
        }
    }
}