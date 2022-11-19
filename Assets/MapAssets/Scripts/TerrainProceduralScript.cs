using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainProceduralScript : MonoBehaviour
{  
    public int depth;

    public int width = 100;     
    public int height = 200;

    public float scale = 3;

    [SerializeField] public float offsetX = 100f;
    [SerializeField] public float offsetY = 100f;

    void Start() {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    void Update() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData) {
        
        terrainData.heightmapResolution = height + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    // float[,] GenerateHeights() {
    //     float[,] heights = new float[256, 256];
    //     for (int x=0; x < 256; x++) {
    //         for (int y = 0; y < 256; y++) {
    //             heights[x, y] = CalculateHeight(x, y);
    //         }
    //     }

    //     return heights;
    // }

    float[,] GenerateHeights() {
        float[,] heights = new float[256, 256];
        
        for (int x=0; x < 256; x++) {
            for (int y = 0; y < 256; y++) {    
                if(x <= 70 || x >= 186) {
                    heights[x, y] = 0.8f;
                } else {
                    heights[x, y] = CalculateHeight(x, y);
                }
            }       
        }
        return heights;
    }
    
    float CalculateHeight(int x, int y) {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
        
    }
}
