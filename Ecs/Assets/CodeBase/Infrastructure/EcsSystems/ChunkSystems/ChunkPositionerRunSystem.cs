using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Chunks
{
    public class ChunkPositionerRunSystem : IEcsRunSystem
    {
	    private EcsFilter<ChunkComponent> _chunkFilter;
	    private EcsFilter<PlayerComponent> _playerFilter;

	    public void Run()
	    {
		    Enable();
	    }

	    private void Enable()
	    {
		    SpawnInEightDimensions();
		    DisableFarChunks();
	    }

	    private void Disable()
	    {
		    RemoveExcessChunks();
	    }
        
	    private void RemoveExcessChunks()
	    {
		    _chunkFilter.Get1(1).Chunks.ForEach(x => _chunkFilter.Get1(1).ChunkPool.Push(x));
		    _chunkFilter.Get1(1).Chunks.Clear();
		    Transform target = _chunkFilter.Get1(1).ChunkPool.Pop();
		    target.position = Vector3.zero;
		    _chunkFilter.Get1(1).Chunks.Add(target);
	    }
        
	    private void DisableFarChunks()
	    {
		    
		    List<Transform> chunksForDisable = _chunkFilter.Get1(1).Chunks
			    .Where(x => Vector3.Distance(x.position, _playerFilter.Get1(1).PlayerTransform.position) > _chunkFilter.Get1(1).Diagonal * 2)
			    .ToList();
		    chunksForDisable.ForEach(chunk =>
		    {
			    chunk.gameObject.SetActive(false);
			    _chunkFilter.Get1(1).ChunkPool.Push(chunk);
		    });
		    _chunkFilter.Get1(1).Chunks = _chunkFilter.Get1(1).Chunks.Except(chunksForDisable).ToList();
	    }
        
	    private void SpawnInEightDimensions()
	    {
		    List<Vector3> spawnPoints = GetSpawnPoints();
		    List<Vector3> freeSpawnPoints = GetFreeSpawnPoints(spawnPoints);
		    SpawnChunks(freeSpawnPoints);
	    }
        
	    private void SpawnChunks(List<Vector3> freeSpawnPoints)
	    {
		    freeSpawnPoints.ForEach(spawnPoint =>
		    { 
			    Transform target = _chunkFilter.Get1(1).ChunkPool.Pop(); 
			    target.position = spawnPoint; 
			    _chunkFilter.Get1(1).Chunks.Add(target); 
			    target.gameObject.SetActive(true);
		    });
	    }
	    private List<Vector3> GetSpawnPoints() 
	    { 
		    Vector3 nearestChunkPosition = _chunkFilter.Get1(1).Chunks.OrderBy(x=> Vector3.Distance(x.position, _playerFilter.Get1(1).PlayerTransform.position)).First().position; 
		    List<Vector3> spawnPoints = new List<Vector3>(_chunkFilter.Get1(1).SpawnDirections); 
		    for (var i = 0; i < spawnPoints.Count; i++) 
		    {
			    Vector3 vectorTarget = spawnPoints[i];
			    if (Mathf.Abs(Mathf.Abs(vectorTarget.x) - Mathf.Abs(vectorTarget.z)) < .01f)
			    {
				    vectorTarget *= _chunkFilter.Get1(1).Diagonal;
			    }
			    else
			    {
				    vectorTarget *= _chunkFilter.Get1(1).ChunkWidth;
			    }
			    spawnPoints[i] = vectorTarget + nearestChunkPosition;
		    }

		    return spawnPoints;
	    }
	    private List<Vector3> GetFreeSpawnPoints(List<Vector3> spawnPoints) => 
	                spawnPoints.Where(x => _chunkFilter.Get1(1).Chunks.All(z => Vector3.Distance(z.position, x) > 1)).ToList();
    }
}