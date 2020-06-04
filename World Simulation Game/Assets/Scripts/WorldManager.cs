using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Profiling;

public static class WorldManager
{
    public static Dictionary<WorldPos, Chunk> WorldChunks = new Dictionary<WorldPos, Chunk>();

    public static int3 ChunkDimensions;
    public static int3 WorldDimensionsChunks;

    public static Chunk GetChunk(int x, int y, int z)
    {
        int xPos = Mathf.FloorToInt(x / ChunkDimensions.x) * ChunkDimensions.x;
        int yPos = Mathf.FloorToInt(y / ChunkDimensions.y) * ChunkDimensions.y;
        int zPos = Mathf.FloorToInt(z / ChunkDimensions.z) * ChunkDimensions.z;
        Chunk temp = null;

        bool successful = WorldChunks.TryGetValue(new WorldPos(xPos, yPos, zPos), out temp);

        return temp;
    }

    public static Block GetBlock(int x, int y, int z)
    {
        Chunk chunk = GetChunk(x, y, z);
        if(chunk != null)
        {
            Block block = chunk.GetBlock(x - chunk.WorldPos.x, y - chunk.WorldPos.y, z - chunk.WorldPos.z);
            return block;
        }
        else
        {
            return new Block(new int3(-1, -1, -1), new int3(1, 1, 1), BlockType.VOID);
        }
    }

    public static Block GetBlock(int x, int y, int z, out Chunk chunk)
    {
        chunk = GetChunk(x, y, z);
        if(chunk != null)
        {
            Block block = chunk.GetBlock(x - chunk.WorldPos.x, y - chunk.WorldPos.y, z - chunk.WorldPos.z);
            return block;
        }
        else
        {
            return new Block(new int3(-1, -1, -1), new int3(1, 1, 1), BlockType.VOID);
        }
    }

    public static void SetBlock(int x, int y, int z, Block block)
    {
        Chunk chunk = GetChunk(x, y, z);

        if(chunk != null)
        {
            chunk.SetBlock(x - chunk.WorldPos.x, y - chunk.WorldPos.y, z - chunk.WorldPos.z, block);
            chunk.update = true;
        }
    }

    public static Block GetNeighbour(int3 position, Direction dir)
    {
        int checkX = position.x + BlockManager.neighbourOffsets[(int)dir].x;
        int checkY = position.y + BlockManager.neighbourOffsets[(int)dir].y;
        int checkZ = position.z + BlockManager.neighbourOffsets[(int)dir].z;

        if( checkX < 0 || checkX >= WorldDimensionsChunks.x * ChunkDimensions.x || 
            checkY < 0 || checkY >= WorldDimensionsChunks.y * ChunkDimensions.y ||
            checkZ < 0 || checkZ >= WorldDimensionsChunks.z * ChunkDimensions.z )
        {
            return new Block(new int3(-1, -1, -1), new int3(1, 1, 1), BlockType.AIR);
            
        }
        else
        {
            return GetBlock(checkX,checkY, checkZ);
        }
    }

    public static Vector3 WorldDimensions()
    {
        return new Vector3(
            WorldDimensionsChunks.x * ChunkDimensions.x,
            WorldDimensionsChunks.y * ChunkDimensions.y,
            WorldDimensionsChunks.z * ChunkDimensions.z
        );
    }

    public static void ClearWorldChunks()
    {
        if(WorldChunks.Count > 0)
        {
            foreach(KeyValuePair<WorldPos, Chunk> entry in WorldChunks)
            {
                entry.Value.DestroyChunk();
            }
        }
        WorldChunks.Clear();
    }
}
