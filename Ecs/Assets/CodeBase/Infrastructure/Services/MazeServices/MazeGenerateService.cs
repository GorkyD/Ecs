using System;
using System.Collections.Generic;
using Infrastructure.EcsSystems.MazeSystems;

namespace Infrastructure.MazeGenerate
{
    public class MazeGenerateService
    {
        private WallState GetOppositeWall(WallState state)
        {
            switch (state)
            {
                case WallState.Right: return WallState.Left;
                case WallState.Left: return WallState.Right;
                case WallState.Up: return WallState.Down;
                case WallState.Down: return WallState.Up;
                default: return WallState.Left;
            }
        }

        private WallState[,] ApplyRecursiveBackTracker(WallState[,] maze,int width, int height)
        {
            Random range = new Random();
            Stack<Position> positionStack = new Stack<Position>();
            Position position = new Position { X = range.Next(0, width), Y = range.Next(0,height)};

            maze[position.X, position.Y] |= WallState.Visited;
            positionStack.Push(position);

            while (positionStack.Count > 0)
            {
                Position current = positionStack.Pop();
                List<Neighbour> neighbours = GetUnvisitedNeighbours(current, maze, width, height);

                if (neighbours.Count > 0)
                {
                    positionStack.Push(current);

                    int randomIndex = range.Next(0, neighbours.Count);
                    Neighbour randomNeighbour = neighbours[randomIndex];

                    Position neighbourPosition = randomNeighbour.Position;
                    maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                    maze[neighbourPosition.X, neighbourPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

                    maze[neighbourPosition.X, neighbourPosition.Y] |= WallState.Visited;
                    
                    positionStack.Push(neighbourPosition);
                }
            }
            
            return maze;
        }

        private List<Neighbour> GetUnvisitedNeighbours(Position position, WallState[,] maze, int width, int height)
        {
            List<Neighbour> list = new List<Neighbour>();

            if (position.X > 0)
            {
                if (!maze[position.X - 1, position.Y].HasFlag(WallState.Visited))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = position.X - 1,
                            Y = position.Y
                        },
                        SharedWall = WallState.Left
                    });
                }
            }
            
            if (position.Y > 0)
            {
                if (!maze[position.X, position.Y - 1].HasFlag(WallState.Visited))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = position.X,
                            Y = position.Y - 1
                        },
                        SharedWall = WallState.Down
                    });
                }
            }
            
            if (position.Y < height - 1)
            {
                if (!maze[position.X, position.Y + 1].HasFlag(WallState.Visited))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = position.X,
                            Y = position.Y + 1
                        },
                        SharedWall = WallState.Up
                    });
                }
            }
            
            if (position.X < width - 1)
            {
                if (!maze[position.X + 1, position.Y].HasFlag(WallState.Visited))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = position.X + 1,
                            Y = position.Y
                        },
                        SharedWall = WallState.Right
                    });
                }
            }

            return list;
        }

        public WallState[,] Generate(int width, int height)
        {
            WallState[,] maze = new WallState[width, height];

            WallState initial = WallState.Left | WallState.Right | WallState.Up | WallState.Down;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    maze[i, j] = initial;
                }
            }
            return ApplyRecursiveBackTracker(maze, width, height);
        }
    }
}