using Infrastructure.MazeGenerate;
using Leopotam.Ecs;

namespace Infrastructure.EcsSystems.MazeSystems
{
    public class MazeGenerateInitSystem : IEcsInitSystem
    {
        private MazeGenerateService _mazeGenerateService;
        private MazeRendererService _mazeRendererService;
        public void Init()
        {
            WallState[,] maze = _mazeGenerateService.Generate(10,10);
            _mazeRendererService.Draw(maze,10,10);
        }
    }
}