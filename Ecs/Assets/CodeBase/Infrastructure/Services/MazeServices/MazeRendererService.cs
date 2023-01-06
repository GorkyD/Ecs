using Infrastructure.EcsSystems.MazeSystems;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.MazeGenerate
{
    public class MazeRendererService
    {
        private readonly Transform _wall;
        private Transform _wallsParent;
        private MazeRendererService()
        {
            AssetProvider assetProvider = new AssetProvider();
            _wall = assetProvider.Load<Transform>(AssetPath.Wall);
            _wallsParent = Object.FindObjectOfType<WallComponent>().transform;
        }

        public void Draw(WallState[,] maze, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    WallState cell = maze[i, j];
                    Vector3 position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                    if (cell.HasFlag(WallState.Up))
                    {
                        Transform topWall = Object.Instantiate(_wall, _wallsParent);
                        topWall.position = position + new Vector3(0, 0, 0.5f);
                        topWall.localScale = new Vector3(1f, topWall.localScale.y, topWall.localScale.z);
                    }
                    
                    if (cell.HasFlag(WallState.Left))
                    {
                        Transform leftWall = Object.Instantiate(_wall, _wallsParent);
                        leftWall.position = position + new Vector3(-0.5f, 0, 0);
                        leftWall.localScale = new Vector3(1f, leftWall.localScale.y, leftWall.localScale.z);
                        leftWall.eulerAngles = new Vector3(0, 90, 0);
                    }

                    if (i == width - 1)
                    {
                        if (cell.HasFlag(WallState.Right))
                        {
                            Transform rightWall = Object.Instantiate(_wall, _wallsParent);
                            rightWall.position = position + new Vector3(0.5f, 0, 0);
                            rightWall.localScale = new Vector3(1f, rightWall.localScale.y, rightWall.localScale.z);
                            rightWall.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }

                    if (j != 0) continue;
                    if (!cell.HasFlag(WallState.Down)) continue;
                    Transform downWall = Object.Instantiate(_wall, _wallsParent);
                    downWall.position = position + new Vector3(0, 0, -0.5f);
                    downWall.localScale = new Vector3(1f, downWall.localScale.y, downWall.localScale.z);
                }
            }
        }
    }
}