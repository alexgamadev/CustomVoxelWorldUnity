using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] int numChunksRendered = 0;
    Queue<ChunkThreadInfo<Chunk>> chunkThreadQueue = new Queue<ChunkThreadInfo<Chunk>>();
    Queue<ChunkMeshThreadInfo<MeshData, Chunk>> chunkMeshDataThreadQueue = new Queue<ChunkMeshThreadInfo<MeshData, Chunk>>();
<<<<<<< Updated upstream
    public Chunk CreateChunk(WorldPos pos, Vector3 position, Material[] materials)
    {
        Chunk chunk = new Chunk();

        chunk.Init(pos, position, materials);
=======
    bool isFinished = false;

    public Chunk CreateChunk(WorldPos pos, Vector3 position, Material[] materials, Transform parent, int seed)
    {
        Chunk chunk = new Chunk();

        chunk.Init(pos, position, materials, parent, seed);
>>>>>>> Stashed changes

        return chunk;
    }

    public void RenderChunk()
    {

    }

    public void RequestNewChunk(Action<Chunk> callback, Chunk chunk, TerrainData[,] terrainData)
    {
        ThreadStart threadStart = delegate {NewChunkThread(callback, chunk, terrainData);};

        new Thread(threadStart).Start();
    }

    void NewChunkThread(Action<Chunk> callback, Chunk chunk, TerrainData[,] terrainData)
    {
        chunk.CreateChunk(terrainData);
        lock(chunkThreadQueue)
        {
            chunkThreadQueue.Enqueue(new ChunkThreadInfo<Chunk>(callback, chunk));
        }
        
    }

    public void RequestChunkMeshData(Action<MeshData, Chunk> callback, Chunk chunk)
    {
        ThreadStart threadStart = delegate {NewChunkMeshDataThread(callback, chunk);};

        new Thread(threadStart).Start();
    }

    void NewChunkMeshDataThread(Action<MeshData, Chunk> callback, Chunk chunk)
    {
        MeshData meshData = chunk.GetMeshData();
        lock(chunkThreadQueue)
        {
            chunkMeshDataThreadQueue.Enqueue(new ChunkMeshThreadInfo<MeshData, Chunk>(callback, meshData, chunk));
        }
        
    }

    public void Update()
    {
        if(chunkThreadQueue.Count > 0)
        {
            int numThreads = chunkThreadQueue.Count > 4 ? 4 : chunkThreadQueue.Count;
            //Debug.Log(numThreads);
            for(int i = 0; i < numThreads; i++)
            {
                ChunkThreadInfo<Chunk> chunkInfo = chunkThreadQueue.Dequeue();
                chunkInfo.callback(chunkInfo.parameter);
            }
        }

        if(chunkMeshDataThreadQueue.Count > 0)
        {
            int numThreads = chunkMeshDataThreadQueue.Count > 4 ? 4 : chunkMeshDataThreadQueue.Count;
            for(int i = 0; i < numThreads; i++)
            {
                ChunkMeshThreadInfo<MeshData, Chunk> meshDataInfo = chunkMeshDataThreadQueue.Dequeue();
                meshDataInfo.callback(meshDataInfo.meshData, meshDataInfo.chunk);
                numChunksRendered++;
            }
        }
    }

    struct ChunkThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public ChunkThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }
    }

    struct ChunkMeshThreadInfo<MeshData, Chunk>
    {
        public readonly Action<MeshData, Chunk> callback;
        public readonly MeshData meshData;
        public readonly Chunk chunk;

        public ChunkMeshThreadInfo(Action<MeshData, Chunk> callback, MeshData meshData, Chunk chunk)
        {
            this.callback = callback;
            this.meshData = meshData;
            this.chunk = chunk;
        }
    }
}