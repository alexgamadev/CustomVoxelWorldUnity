using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public static class BlockTemplates
{
    public static Dictionary<int, TextureTile[]> blockTileDicts = new Dictionary<int, TextureTile[]>()
    {
        { (int)BlockType.GRASS, new TextureTile[] 
            {
                new TextureTile(0, 0), //North
                new TextureTile(0, 0), //East
                new TextureTile(0, 0), //South
                new TextureTile(0, 0), //West
                new TextureTile(0, 0), //Up
                new TextureTile(0, 0)  //Down
            }
        },
        { (int)BlockType.DIRT, new TextureTile[] 
            {
                new TextureTile(2, 0), //North
                new TextureTile(2, 0), //East
                new TextureTile(2, 0), //South
                new TextureTile(2, 0), //West
                new TextureTile(2, 0), //Up
                new TextureTile(2, 0)  //Down
            }
        },
        { (int)BlockType.SAND, new TextureTile[] 
            {
                new TextureTile(3, 0), //North
                new TextureTile(3, 0), //East
                new TextureTile(3, 0), //South
                new TextureTile(3, 0), //West
                new TextureTile(3, 0), //Up
                new TextureTile(3, 0)  //Down
            }
        },
        { (int)BlockType.WATER_OCEAN, new TextureTile[] 
            {
                new TextureTile(4, 0), //North
                new TextureTile(4, 0), //East
                new TextureTile(4, 0), //South
                new TextureTile(4, 0), //West
                new TextureTile(4, 0), //Up
                new TextureTile(4, 0)  //Down
            }
        },
        { (int)BlockType.ROCK, new TextureTile[] 
            {
                new TextureTile(5, 0), //North
                new TextureTile(5, 0), //East
                new TextureTile(5, 0), //South
                new TextureTile(5, 0), //West
                new TextureTile(5, 0), //Up
                new TextureTile(5, 0)  //Down
            }
        },
        { (int)BlockType.FOREST, new TextureTile[] 
            {
                new TextureTile(6, 0), //North
                new TextureTile(6, 0), //East
                new TextureTile(6, 0), //South
                new TextureTile(6, 0), //West
                new TextureTile(6, 0), //Up
                new TextureTile(6, 0)  //Down
            }
        },
        { (int)BlockType.VOID, new TextureTile[] 
            {
                new TextureTile(7, 7), //North
                new TextureTile(7, 7), //East
                new TextureTile(7, 7), //South
                new TextureTile(7, 7), //West
                new TextureTile(7, 7), //Up
                new TextureTile(7, 7)  //Down
            }
        },
    };
    public static Dictionary<int, List<IBlockComponentData>> blockComponentsDict = new Dictionary<int, List<IBlockComponentData>>()
    {
        { (int)BlockType.WATER_OCEAN, new List<IBlockComponentData> 
            {
                new TransparencyBlockData(0.5f),
                new TextureBlockData(
                    new TextureTile[]{
                        new TextureTile(4, 0)
                    }
                ),
            }
        },
        { (int)BlockType.AIR, new List<IBlockComponentData> 
            {
                new NoRenderBlockTag()
            }
        },
        { (int)BlockType.VOID, new List<IBlockComponentData> 
            {
                new TransparencyBlockData(0.5f),
                new TextureBlockData(
                    new TextureTile[]{
                        new TextureTile(7, 7)
                    }
                ),
            }
        },
    };
}
