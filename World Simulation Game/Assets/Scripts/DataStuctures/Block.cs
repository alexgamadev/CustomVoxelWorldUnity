using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
public struct Block
{
    public int3 position;
    public float3 scale;
    public BlockType type;
    public TextureTile[] tiles;
    public List<IBlockComponentData> components;

    public Block(int3 position, float3 scale, BlockType type)
    {
        this.position = position;
        this.scale = scale;
        this.type = type;

        tiles = new TextureTile[(int)Direction.LENGTH];
        BlockTemplates.blockTileDicts.TryGetValue((int)type, out tiles);

        components = new List<IBlockComponentData>();
        BlockTemplates.blockComponentsDict.TryGetValue((int)type, out components);
    }

    public bool HasComponent<IBlockComponent>()
    {
        if(components == null || components.Count<=0)
        {
            return false;
        }
        
        for (int i = 0; i < components.Count; i++)
        {
            if(components[i] is IBlockComponent)
            {
                return true;
            }
        }
        return false;
    }
}

public enum BlockType
{
    VOID,
    AIR,
    WATER_OCEAN,
    SAND,
    GRASS,
    DIRT,
    ROCK,
    FOREST
}

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST,
    UP,
    DOWN,
    LENGTH
}

public struct TextureTile 
{ 
    public int x; public int y;

    public TextureTile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
}