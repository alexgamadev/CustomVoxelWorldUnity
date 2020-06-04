using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine.Profiling;
using System;
using System.Threading;

public class Chunk
{
    Block[ , , ] _blocks;
    [SerializeField] private int3 _chunkDimensions;
    public bool update = false;
    [SerializeField] GameObject meshGameObject;
    [SerializeField] MeshFilter filter;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] MeshCollider meshCol;
    [SerializeField] private WorldPos _worldPos;
    int _seed = 0;

    public int3 ChunkDimensions { get => _chunkDimensions; }
    public WorldPos WorldPos { get => _worldPos; set => _worldPos = value; }

<<<<<<< Updated upstream
    public void Init(WorldPos pos, Vector3 position, Material[] materials)
=======
    public void Init(WorldPos pos, Vector3 position, Material[] materials, Transform parent, int seed)
>>>>>>> Stashed changes
    {
        meshGameObject = new GameObject("Region Chunk");
        filter = meshGameObject.AddComponent<MeshFilter>();
        meshCol = meshGameObject.AddComponent<MeshCollider>();
        meshRenderer = meshGameObject.AddComponent<MeshRenderer>();
        meshRenderer.materials = materials;
        meshGameObject.transform.position = position;
        _seed = seed;

        WorldPos = pos;
        _chunkDimensions = WorldManager.ChunkDimensions;
    }

    public void CreateChunk(TerrainData[,] terrainData)
    {
        _blocks = new Block[ChunkDimensions.x, ChunkDimensions.y, ChunkDimensions.z];
<<<<<<< Updated upstream
=======
        _worldObjects = new int[ChunkDimensions.x, ChunkDimensions.z];
        System.Random r = new System.Random(_seed);
>>>>>>> Stashed changes
        
        for (int x = 0; x < ChunkDimensions.x; x++)
        {
            for (int y = 0; y < ChunkDimensions.y; y++)
            {
                for (int z = 0; z < ChunkDimensions.z; z++)
                {
                    BlockType type = terrainData[WorldPos.x + x, WorldPos.z + z].GetBlockType(y);
                    float3 scale = new float3(1f, 1f, 1f);
                    int3 position = ChunkToWorldPos(x, y, z);

                    if((type == BlockType.WATER_OCEAN) && position.y == 2)
                    {
                        scale = new float3(1f, 0.75f, 1f);
                    }
                    _blocks[x, y, z] = new Block(position, scale, type);
<<<<<<< Updated upstream
=======

                    if(type == BlockType.FOREST)
                    {
                        if(r.Next(0, 1000) <= 50)
                        {
                            _worldObjects[x, z] = 1;
                        }
                        else
                        {
                            _worldObjects[x, z] = 0;
                        }
                    }
                    else if(type == BlockType.GRASS)
                    {
                        if(r.Next(0, 1000) <= 15)
                        {
                            _worldObjects[x, z] = 1;
                        }
                        else
                        {
                            _worldObjects[x, z] = 0;
                        }
                    }
                    else
                    {
                        _worldObjects[x, z] = 0;
                    }
>>>>>>> Stashed changes
                }
            }
        }

        //UpdateChunkMesh();
    }

    public void DestroyChunk()
    {
        meshGameObject.SetActive(false);
    }

    public Block GetBlock(int x, int y, int z)
    {
        if(ValidPosition(new int3(x, y, z)))
        {
             return _blocks[x, y, z];
        }
        else
        {
            return new Block(new int3(-1, -1, -1), new int3(1, 1, 1), BlockType.VOID);
        }
       
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        if(ValidPosition(new int3(x, y, z)))
        {
            _blocks[x, y, z] = block;
        }
    }

    public int3 ChunkToWorldPos(int x, int y, int z)
    {
        int worldX = _worldPos.x + x;
        int worldY = _worldPos.y + y;
        int worldZ = _worldPos.z + z;

        return new int3(worldX, worldY, worldZ);
    }

    public bool ValidPosition(int3 position)
    {
        if((position.x >= 0 && position.x < _chunkDimensions.x) && 
            (position.y >= 0 && position.y < _chunkDimensions.y) &&
            (position.z >= 0 && position.z < _chunkDimensions.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public MeshData GetMeshData()
    {
        MeshData meshData = new MeshData();
        for (int x = 0; x < ChunkDimensions.x; x++)
        {
            for (int y = 0; y < ChunkDimensions.y; y++)
            {
                for (int z = 0; z < ChunkDimensions.z; z++)
                {
                    meshData = BlockManager.GetMeshData(this, _blocks[x, y, z], x, y, z, meshData);
                }
            }
        }

        return meshData;
    }

    public void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.subMeshCount = 2;
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.SetTriangles(meshData.triangles.ToArray(), 0);
        filter.mesh.SetTriangles(meshData.transparentTriangles.ToArray(), 1);
        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();

        //Collision mesh
        meshCol.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();

        meshCol.sharedMesh = mesh;
    }
}