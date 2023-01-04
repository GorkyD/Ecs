using Infrastructure.EcsSystems.MazeSystems;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.MazeGenerate
{
    public class MazeRendererService
    {
        private readonly Transform _wall;
        private MazeRendererService()
        {
            AssetProvider assetProvider = new AssetProvider();
            _wall = assetProvider.Load<Transform>(AssetPath.Wall);
        }

        public bool Draw(WallState[,] maze, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var cell = maze[i, j];
                    var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                    if (cell.HasFlag(WallState.Up))
                    {
                        Transform topWall = Object.Instantiate(_wall);
                        topWall.position = position + new Vector3(0, 0, 0.5f);
                        topWall.localScale = new Vector3(1f, topWall.localScale.y, topWall.localScale.z);
                    }
                    
                    if (cell.HasFlag(WallState.Left))
                    {
                        Transform leftWall = Object.Instantiate(_wall);
                        leftWall.position = position + new Vector3(-0.5f, 0, 0);
                        leftWall.localScale = new Vector3(1f, leftWall.localScale.y, leftWall.localScale.z);
                        leftWall.eulerAngles = new Vector3(0, 90, 0);
                    }

                    if (i == width - 1)
                    {
                        if (cell.HasFlag(WallState.Right))
                        {
                            Transform rightWall = Object.Instantiate(_wall);
                            rightWall.position = position + new Vector3(0.5f, 0, 0);
                            rightWall.localScale = new Vector3(1f, rightWall.localScale.y, rightWall.localScale.z);
                            rightWall.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }
                    
                    if (j == 0)
                    {
                        if (cell.HasFlag(WallState.Down))
                        {
                            Transform downWall = Object.Instantiate(_wall);
                            downWall.position = position + new Vector3(0, 0, -0.5f);
                            downWall.localScale = new Vector3(1f, downWall.localScale.y, downWall.localScale.z);
                        }
                    }
                }
            }

            return true;
        }
    }
}