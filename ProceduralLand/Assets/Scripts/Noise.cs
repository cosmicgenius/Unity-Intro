﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

  public static float[,] GenerateNoiseMap (int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
    float[,] noiseMap = new float[width, height];

    System.Random prng = new System.Random (seed);
    Vector2[] octaveOffsets = new Vector2[octaves];

    for (int i = 0; i < octaves; i++) {
      float offsetX = prng.Next(-100000, 100000) + offset.x;
      float offsetY = prng.Next(-100000, 100000) + offset.y;

      octaveOffsets[i] = new Vector2 (offsetX, offsetY);
    }

    if (scale <= 0) {
      scale = 0.0001f;
    }

    float minH = float.MaxValue, maxH = float.MinValue;    

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        float amplitude = 1, frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++) {
          float sampleX = (x - width / 2f) / scale * frequency + octaveOffsets[i].x;
          float sampleY = (y - height / 2f) / scale * frequency + octaveOffsets[i].y;

          float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
          noiseHeight += perlinValue * amplitude;

          amplitude *= persistance;
          frequency *= lacunarity;
        }

        minH = Mathf.Min (minH, noiseHeight);
        maxH = Mathf.Max (maxH, noiseHeight);
        noiseMap[x, y] = noiseHeight;
      }
    }

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        noiseMap[x, y] = Mathf.InverseLerp (minH, maxH, noiseMap[x, y]);
      }
    }

    return noiseMap;
  }
}