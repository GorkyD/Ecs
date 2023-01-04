using Infrastructure.EcsSystems.MazeSystems;

namespace Infrastructure.MazeGenerate
{
    public class MazeGenerateService
    {
        public WallState[,] Generate(int width, int height)
        {
            WallState[,] maze = new WallState[width, height];

            WallState initial = WallState.Left | WallState.Right | WallState.Up | WallState.Down;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    maze[i, j] = initial;

                    maze[i, j].HasFlag(WallState.Right);
                }
            }
            return maze;
        }
    }
}