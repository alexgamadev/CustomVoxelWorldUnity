using UnityEngine;

public struct TerrainData
{
    public float elevation;
    public float temperature;
    public float moisture;

    public BlockType GetBlockType(int height)
    {
        if(height == 0)
        {
            return BlockType.SAND;
        }
        if(elevation >= 0f && elevation < 0.5f && height <= 2)
        {
            return BlockType.WATER_OCEAN;
        }
        if(elevation >= 0.5f && elevation < 0.55f && height <= 2)
        {
            return BlockType.SAND;
        }
<<<<<<< Updated upstream
        if(elevation >= 0.55f && elevation < 0.75 && height <= 2)
        {
            return BlockType.GRASS;
        }
        else if(elevation >= 0.75f && height == 3)
        {
            return BlockType.ROCK;
=======
        if(elevation >= 0.55f && elevation < 0.75&& height <= 3)
        {
            return BlockType.GRASS;
        }
        else if(elevation >= 0.75f && height <= 3)
        {
            return BlockType.FOREST;
>>>>>>> Stashed changes
        }
        else
        {
            return BlockType.AIR;
        }
    }
}
