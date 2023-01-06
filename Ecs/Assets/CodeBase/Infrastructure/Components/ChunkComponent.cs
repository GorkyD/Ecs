using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Chunks
{
    public struct ChunkComponent
    {
        public List<Transform> Chunks;
        public Stack<Transform> ChunkPool;
        public GameObject GroundPrefab;
        public List<Vector3> SpawnDirections;
        public float ChunkWidth;
        public float Diagonal;
    }
}