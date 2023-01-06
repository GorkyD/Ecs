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
            if (Application.platform == RuntimePlatform.Android)
            {
                EcsEntity joystickEntity = _ecsWorld.NewEntity();
            
                ref var joyStick = ref joystickEntity.Get<JoyStick>();
                GameObject joyStickGameObject = Object.Instantiate(_staticData.joystickPrefab,_sceneData.mainCanvas.transform);
                joyStick.UltimateJoystick = joyStickGameObject.GetComponent<UltimateJoystick>();
            }
        }
    }
}