using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Chunks;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Chunks
{
	public class ChunkPositionerInitSystem : IEcsInitSystem
	{
		private EcsWorld _ecsWorld;
		private EcsFilter<ChunkComponent> _filter;

		public void Init()
		{
			EcsEntity chunkEntity= _ecsWorld.NewEntity();
			ref var chunk = ref chunkEntity.Get<ChunkComponent>();
			chunk.GroundPrefab = Resources.Load<GameObject>(AssetPath.Ground);
			chunk.ChunkWidth = GetChunkWidth();
			chunk.Diagonal = chunk.ChunkWidth * Mathf.Sqrt(2);
			chunk.SpawnDirections = GetSpawnDirections();
			chunk.Chunks = new List<Transform>();
			chunk.Chunks.Add(Object.FindObjectOfType<Chunk>().transform);
			chunk.ChunkPool = new Stack<Transform>();
			FillChunkPool();
		}
		
		private List<Vector3> GetSpawnDirections()
		{
			List<Vector3> spawnPoints = new List<Vector3>();
			for (int x = -1; x <= 1; x++)
			{
				for (int z = -1; z <= 1; z++)
				{
					spawnPoints.Add(new Vector3(x, 0, z).normalized);
				}
			}

			return spawnPoints;
		}

		private void FillChunkPool()
		{
			for (int i = 0; i < 30; i++)
			{
				GameObject chunk = Object.Instantiate(_filter.Get1(1).GroundPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
				chunk.SetActive(false);
				_filter.Get1(1).ChunkPool.Push(chunk.transform);
			}
		}

		private float GetChunkWidth() => 
			_filter.Get1(1).GroundPrefab.GetComponentsInChildren<MeshRenderer>().Select(x => x.bounds.size.x).Max();
	}
}
