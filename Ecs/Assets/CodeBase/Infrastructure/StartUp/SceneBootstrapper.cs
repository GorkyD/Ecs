using Infrastructure.MazeGenerate;
using Leopotam.Ecs;
using Zenject;

namespace Infrastructure.StartUp
{
    public class SceneBootstrapper : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(new EcsWorld());
            Container.BindInterfacesAndSelfTo<MazeGenerateService>().AsSingle();
            Container.BindInterfacesAndSelfTo<MazeRendererService>().AsSingle();
        }
        
    }
}