using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChunkData
{
    private Block[,,] blocks;
    public WorldPos worldPosition;
    public MeshData meshData;
    public bool isDirty;

    public ChunkData(WorldPos worldPos)
    {
        blocks = new Block[WorldManager.ChunkDimensions.x, WorldManager.ChunkDimensions.y, WorldManager.ChunkDimensions.z];
        worldPosition = worldPos;
        meshData = new MeshData();
        isDirty = false;
    }

    public void SetBlock(Block block, int x, int y, int z)
    {
        blocks[x, y, z] = block;
    }

    public Block GetBlock(int x, int y, int z)
    {
        return blocks[x, y, z];
    }

}
