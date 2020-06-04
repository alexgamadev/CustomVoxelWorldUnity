using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Profiling;

public static class MeshGenerator 
{
    public static MeshData GenerateBlockData(Chunk chunk, Block block, int x, int y, int z, MeshData meshData)
    {
        int3 worldPos = chunk.ChunkToWorldPos(x, y, z);

        //Don't render blocks containing No Render tag
        if(block.HasComponent<NoRenderBlockTag>())
        {
            return meshData;
        }

        meshData.useRenderDataForCol = true;

        if(DrawFace(block, WorldManager.GetNeighbour(worldPos, Direction.UP)))
        {
            meshData = MeshGenerator.FaceDataUp(block, x, y, z, meshData);
        }
        if(DrawFace(block, WorldManager.GetNeighbour(worldPos, Direction.DOWN)))
        {
            meshData = MeshGenerator.FaceDataDown(block, x, y, z, meshData);
        }
        if(DrawFace(block, WorldManager.GetNeighbour(worldPos, Direction.NORTH)))
        {
            meshData = MeshGenerator.FaceDataNorth(block, x, y, z, meshData);
        }
        if(DrawFace(block, WorldManager.GetNeighbour(worldPos, Direction.EAST)))
        {
            meshData = MeshGenerator.FaceDataEast(block, x, y, z, meshData);
        }
        if(DrawFace(block, WorldManager.GetNeighbour(worldPos, Direction.SOUTH)))
        {
            meshData = MeshGenerator.FaceDataSouth(block, x, y, z, meshData);
        }
        if(DrawFace(block, WorldManager.GetNeighbour(worldPos, Direction.WEST)))
        {
            meshData = MeshGenerator.FaceDataWest(block, x, y, z, meshData);
        }

        return meshData;
    }

    public static bool DrawFace(Block current, Block neighbour)
    {
        if(neighbour.HasComponent<TransparencyBlockData>() && current.HasComponent<TransparencyBlockData>())
        {
            return false;
        }
        return neighbour.HasComponent<NoRenderBlockTag>() || neighbour.HasComponent<TransparencyBlockData>();
    }
    public static MeshData FaceDataNorth(Block block, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + (block.scale.y - 0.5f), z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + (block.scale.y - 0.5f), z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
  
        meshData.AddQuadTriangles(block.HasComponent<TransparencyBlockData>());
        meshData.uv.AddRange(FaceUVs(block, Direction.NORTH));
        return meshData;
    }
  
    public static MeshData FaceDataEast(Block block, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + (block.scale.y - 0.5f), z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + (block.scale.y - 0.5f), z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
  
        meshData.AddQuadTriangles(block.HasComponent<TransparencyBlockData>());
        meshData.uv.AddRange(FaceUVs(block, Direction.EAST));
        return meshData;
    }
  
    public static MeshData FaceDataSouth(Block block, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + (block.scale.y - 0.5f), z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + (block.scale.y - 0.5f), z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
  
        meshData.AddQuadTriangles(block.HasComponent<TransparencyBlockData>());
        meshData.uv.AddRange(FaceUVs(block, Direction.SOUTH));
        return meshData;
    }
  
    public static MeshData FaceDataWest(Block block, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + (block.scale.y - 0.5f), z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + (block.scale.y - 0.5f), z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles(block.HasComponent<TransparencyBlockData>());
        meshData.uv.AddRange(FaceUVs(block, Direction.WEST));
        return meshData;
    }

    public static MeshData FaceDataUp(Block block, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y + (block.scale.y - 0.5f), z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + (block.scale.y - 0.5f), z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + (block.scale.y - 0.5f), z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + (block.scale.y - 0.5f), z - 0.5f));
        
        meshData.AddQuadTriangles(block.HasComponent<TransparencyBlockData>());
        meshData.uv.AddRange(FaceUVs(block, Direction.UP));
        return meshData;
    }
  
    public static MeshData FaceDataDown(Block block, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles(block.HasComponent<TransparencyBlockData>());
        meshData.uv.AddRange(FaceUVs(block, Direction.DOWN));
        return meshData;
    }

    public static Vector2[] FaceUVs(Block block, Direction direction)
     {
         Vector2[] UVs = new Vector2[4];
         float tileSize = BlockManager.tileSize;
         
         TextureTile tilePos = block.tiles[(int)direction];
 
         UVs[0] = new Vector2(tileSize * tilePos.x + tileSize,
             tileSize * tilePos.y);
         UVs[1] = new Vector2(tileSize * tilePos.x + tileSize,
             tileSize * tilePos.y + tileSize);
         UVs[2] = new Vector2(tileSize * tilePos.x,
             tileSize * tilePos.y + tileSize);
         UVs[3] = new Vector2(tileSize * tilePos.x,
             tileSize * tilePos.y);
 
         return UVs;
     }
  
}
