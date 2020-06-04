using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;
using System.Threading;
using UnityEngine.Profiling;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private PerlinAttributes attributes;
    [SerializeField] private int seed = 0;
    [SerializeField] Material[] blocksMats;
    [SerializeField] ChunkManager chunkManager;

     [SerializeField] private int3 _chunkDimensions = new int3(16, 2, 16);
    [SerializeField] private int3 _worldDimensions = new int3(12, 1, 12);

    public void Start()
    {
        Profiler.enabled = false;
        chunkManager = this.gameObject.GetComponent<ChunkManager>();
        GenerateMap();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void GenerateMap()
    {
        float startTime = Time.realtimeSinceStartup;
        WorldManager.ClearWorldChunks();
        WorldManager.ChunkDimensions = _chunkDimensions;
        WorldManager.WorldDimensionsChunks = _worldDimensions;

<<<<<<< Updated upstream
=======
        //Reset and initialise relevant data
        WorldSetup();
        float processingTime = (Time.realtimeSinceStartup - startTime);
        Debug.Log("WorldSetuo Time: " + string.Format("{0:0.000}", processingTime) + " seconds");

        //Generate elevation map from perlin noise
>>>>>>> Stashed changes
        TerrainData[,] terrainData = new TerrainData[_worldDimensions.x * _chunkDimensions.x, _worldDimensions.z * _chunkDimensions.z];
        NoiseGenerator.GenerateElevationMap(seed, ref terrainData, attributes);
<<<<<<< Updated upstream
        Debug.Log("tiles: " + terrainData.GetLength(0) * terrainData.GetLength(1));

        for (int x = 0; x < _worldDimensions.x; x++)
        {
            for (int y = 0; y < _worldDimensions.y; y++)
            {
                for (int z = 0; z < _worldDimensions.z; z++)
                {
                    CreateChunk(x * _chunkDimensions.x, y * _chunkDimensions.y, z * _chunkDimensions.z, terrainData);
                }
            }
        }

        float processingTime = (Time.realtimeSinceStartup - startTime);
        Debug.Log("WorldGen Time: " + processingTime * 1000f + "ms");
=======
        processingTime = (Time.realtimeSinceStartup - startTime);
        Debug.Log("ElevationMap Time: " + string.Format("{0:0.000}", processingTime) + " seconds");

        //Create world chunks
        CreateChunks(terrainData);
        processingTime = (Time.realtimeSinceStartup - startTime);
        Debug.Log("ChunkCreation Time: " + string.Format("{0:0.000}", processingTime) + " seconds");

        //Update pathfinding grid with new world data
        Pathfinding.UpdatePathfindingGrid();

        processingTime = (Time.realtimeSinceStartup - startTime);
>>>>>>> Stashed changes
        Debug.Log("WorldGen Time: " + string.Format("{0:0.000}", processingTime) + " seconds");
    }

    public void OnSeedChanged(string seed)
    {
        this.seed = Convert.ToInt32(seed);
    }

    public void CreateChunk(int x, int y, int z, TerrainData[,] terrainData, int seed)
    {
        WorldPos worldPos = new WorldPos(x, y, z);

        Chunk newChunk = new Chunk();
        WorldManager.WorldChunks.Add(worldPos, newChunk);
<<<<<<< Updated upstream
        newChunk.Init(worldPos, new Vector3(x, y, z), blocksMats);
=======
        newChunk.Init(worldPos, new Vector3(x, y, z), blocksMats, this.transform, seed);
>>>>>>> Stashed changes
        chunkManager.RequestNewChunk(OnChunkRecieved, newChunk, terrainData);
    }

    void OnChunkRecieved(Chunk chunk)
    {
        chunkManager.RequestChunkMeshData(OnMeshDataRecieved, chunk);
    }

    void OnMeshDataRecieved(MeshData meshData, Chunk chunk)
    {
        chunk.RenderMesh(meshData);
    }
<<<<<<< Updated upstream
=======

    void WorldSetup()
    {
        chunkManager.ResetChunkCount();
        WorldManager.ClearWorldChunks();
        WorldManager.Active = true;
        WorldManager.ChunkDimensions = _chunkDimensions;
        WorldManager.WorldDimensionsChunks = _worldDimensions;
    }

    void CreateChunks(TerrainData[,] terrainData)
    {
        for (int x = 0; x < _worldDimensions.x; x++)
        {
            for (int y = 0; y < _worldDimensions.y; y++)
            {
                for (int z = 0; z < _worldDimensions.z; z++)
                {
                    CreateChunk(x * _chunkDimensions.x, y * _chunkDimensions.y, z * _chunkDimensions.z, terrainData, seed);
                }
            }
        }
    }
>>>>>>> Stashed changes
}
