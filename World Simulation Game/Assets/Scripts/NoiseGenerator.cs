using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static float SquareGradient(int x, int y, float squareSize)
    {
        float gradient =  Mathf.Max(Mathf.Abs(x - (squareSize) / 2), Mathf.Abs(y - squareSize/2)) / (squareSize / 2);
        return gradient * gradient * gradient * gradient * gradient;
    }

    public static void GenerateElevationMap(int seed, ref TerrainData[,] terrainData, PerlinAttributes attributes)
    {
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
        float maxDistance = float.MinValue;
        float minDistance = float.MaxValue;

        int width = terrainData.GetLength(0);
        int height = terrainData.GetLength(1);

        //Initialise pseudo random generator with selected seed
        System.Random randomGen = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[attributes.octaves];

        //Create offsets for each octave, generated from seeded random generator
        for (int i = 0; i < attributes.octaves; i++)
        {
            octaveOffsets[i].x = randomGen.Next(-100000, 100000);
            octaveOffsets[i].y = randomGen.Next(-100000, 100000);
        }

        //Create elevation, gradient and distance maps 
        float[,] elevationMap = new float[width, height];
        float[,] gradientMap = new float[width, height];
        float[,] distances = new float[width, height];

        //Get middle of world
        Vector2 middle = new Vector2(Mathf.FloorToInt(width / 2), Mathf.FloorToInt(height / 2));

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Generate elevation map from seeded offsets and perlin attributes
                elevationMap[x, y] = GeneratePerlin(x, y, attributes, octaveOffsets);

                //Get distance from centre of world
                distances[x, y] = Vector2.Distance(new Vector2(x, y), middle);

                //Create square gradient map to ensure land is localised nearer the centre of world
                gradientMap[x,y] = SquareGradient(x, y, width);

                //Need to get min height and max height values to normalise elevation back to 0, -1
                if(elevationMap[x, y] > maxNoiseHeight) { maxNoiseHeight = elevationMap[x, y]; }
                else if(elevationMap[x, y] < minNoiseHeight) { minNoiseHeight = elevationMap[x, y]; }

                //Need to get min height and max height values to normalise elevation back to 0, -1
                if(distances[x,y] > maxDistance) { maxDistance = distances[x,y]; }
                else if(distances[x,y] < minDistance) {minDistance = distances[x,y]; }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Ensure elevation mapped between 0 and 1
                float elevation = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, elevationMap[x, y]);

                //Apply attribute power (Higher power = more extreme terrain - highs becomes higher, lows become lower)
                elevation = Mathf.Pow(elevation, attributes.pow);

                float gradient = gradientMap[x, y];

                //Minus elevation from square gradient to ensure edges of world are water
                terrainData[x, y].elevation = elevation;
            }
        }
    }

    public static float GeneratePerlin(int x, int y, PerlinAttributes attributes, Vector2[] octaveOffsets)
    {
        //Frequency decreases every octave so each successive octave is more bunched up
        float frequency = 1f;
        //Amplitude increases every octave so that each successive octave makes less difference to the final elevation
        float amplitude = 1f;

        float finalNoise = 0;

        //Iterate through octaves, creating perlin from sample values adjusted by octave offsets (Seed)
        for (int i = 0; i < attributes.octaves; i++)
        {
                    float sampleX = x / attributes.scale * frequency + octaveOffsets[i].x;
                    float sampleY = y / attributes.scale * frequency + octaveOffsets[i].y;

                    float noise = Mathf.PerlinNoise(sampleX, sampleY);

                    finalNoise += noise * amplitude;

                    amplitude *= 0.5f;
                    frequency *= 2f;
        }

        return finalNoise;
    }
}

[System.Serializable]
public class PerlinAttributes
{
    [Range(1f, 1000f)] public float scale = 40f;
    [Range(1, 15)] public int octaves = 4;
    [Range(0f, 5f)] public float pow = 1f;

}
