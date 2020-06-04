using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Profiling;

public static class BlockManager
{
    public static float tileSize = 0.125f;
    public static MeshData GetMeshData(Chunk chunk, Block block, int x, int y, int z, MeshData meshData)
    {
         return MeshGenerator.GenerateBlockData(chunk, block, x, y, z, meshData);
    }

    public static int3[] neighbourOffsets =
    {
        new int3(0, 0, 1),  //North
        new int3(1, 0, 0),  //East
        new int3(0, 0, -1), //South
        new int3(-1, 0, 0), //West
        new int3(0, 1, 0),  //Up
        new int3(0, -1, 0),  //Down
    };
}
