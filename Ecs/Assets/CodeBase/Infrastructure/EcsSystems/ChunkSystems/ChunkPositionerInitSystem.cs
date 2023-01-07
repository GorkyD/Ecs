using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Chunks
{
	public class ChunkPositionerInitSystem : IEcsInitSystem
	{
		private EcsFilter<ChunkComponent> _filter;
		private EcsWorld _ecsWorld;

		public void Init()
		{
			EcsEntity chunkEntity= _ecsWorld.NewEntity();
			ref var chunk = ref chunkEntity.Get<ChunkComponent>();
			AssetProvider assetProvider = new AssetProvider();
			GameObject chunkReference = assetProvider.Load<GameObject>(AssetPath.Ground);
			chunk.ChunkWidth = GetChunkWidth(chunkReference);
			chunk.Diagonal = chunk.ChunkWidth * Mathf.Sqrt(2);
			chunk.SpawnDirections = GetSpawnDirections();
			chunk.Chunks = new List<Transform>();
			chunk.Chunks.Add(Object.FindObjectOfType<Chunk>().transform);
			chunk.ChunkPool = new Stack<Transform>();
			FillChunkPool(chunkReference);
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

		private void FillChunkPool(GameObject chunkReference)
		{
			for (int i = 0; i < 30; i++)
			{
				GameObject chunk = Object.Instantiate(chunkReference,new Vector3(0f, 0f, 0f), Quaternion.identity);
				chunk.SetActive(false);
				_filter.Get1(1).ChunkPool.Push(chunk.transform);
			}
		}

		private float GetChunkWidth(GameObject chunkReference) => 
			chunkReference.GetComponentsInChildren<MeshRenderer>().Select(x => x.bounds.size.x).Max();
	}
}
