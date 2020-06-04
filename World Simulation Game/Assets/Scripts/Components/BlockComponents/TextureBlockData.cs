using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TextureBlockData : IBlockComponentData
{
    public TextureTile[] textureTiles;

    public TextureBlockData(TextureTile[] textures)
    {
        textureTiles = new TextureTile[6];
        if(textures.GetLength(0) == 1)
        {
            for (int i = 0; i < 6; i++)
            {
                textureTiles[i] = textures[0];
            }
        }
        textureTiles = textures;
    }
}
