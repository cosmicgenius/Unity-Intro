using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

  public enum DrawMode {
    noiseMap,
    colorMap,
    Mesh
  }
  public DrawMode drawMode;

  const int chunkSize = 241;
  [Range (0, 6)]
  public int levelOfDetail;
  public float noiseScale;

  public int octaves;

  [Range (0, 1)]
  public float persistence;
  public float lacunarity;

  public int seed;
  public Vector2 offset;

  public float meshHeightScale;
  public AnimationCurve meshHeightCurve;

  public bool autoUpdate;

  public TerrainType[] regions;

  public void GenerateMap () {
    float[,] noiseMap = Noise.GenerateNoiseMap (chunkSize, chunkSize, seed, noiseScale, octaves, persistence, lacunarity, offset);

    Color[] colorMap = new Color[chunkSize * chunkSize];
    for (int y = 0; y < chunkSize; y++) {
      for (int x = 0; x < chunkSize; x++) {
        float currentHeight = noiseMap[x, y];
        for (int i = 0; i < regions.Length; i++) {
          if (currentHeight <= regions[i].height) {
            colorMap[y * chunkSize + x] = regions[i].color;
            break;
          }
        }
      }
    }

    MapDisplay display = FindObjectOfType<MapDisplay> ();
    if (drawMode == DrawMode.noiseMap) {
      display.DrawTexture (TextureGenerator.TextureFromHeightMap (noiseMap));
    } else if (drawMode == DrawMode.colorMap) {
      display.DrawTexture (TextureGenerator.TextureFromColorMap (colorMap, chunkSize, chunkSize));
    } else if (drawMode == DrawMode.Mesh) {
      display.DrawMesh (MeshGenerator.GenerateTerrainMesh (noiseMap, meshHeightScale, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap (colorMap, chunkSize, chunkSize));
    }
  }

  public void OnValidate () {
    if (lacunarity < 1) {
      lacunarity = 1;
    }
    if (octaves < 0) {
      octaves = 0;
    }

  }
}

[System.Serializable]
public struct TerrainType {

  public string name;
  public float height;
  public Color color;

}