using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

  public GameObject fallingBlockPrefab;
  public Vector2 spawnDelayMinMax = new Vector2 (0.25f, 1);
  float nextSpawnTime;
  Vector2 halfSize;

  public Vector2 spawnSizeMinMax = new Vector2 (0.5f, 1.5f);
  public float spawnAngleMax = 10;

  void Start () {
    halfSize = new Vector2 (Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
  }

  void Update () {
    if (Time.time > nextSpawnTime) {
      float spawnDelay = Mathf.Lerp (spawnDelayMinMax.y, spawnDelayMinMax.x, Difficulty.GetDifficultyPercent ());
      print (spawnDelay);
      nextSpawnTime = Time.time + spawnDelay;

      float spawnAngle = Random.Range (-spawnAngleMax, spawnAngleMax);
      float spawnSize = Random.Range (spawnSizeMinMax.x, spawnSizeMinMax.y);

      Vector2 spawnPos = new Vector2 (Random.Range (-halfSize.x + spawnSize / 2, halfSize.x - spawnSize / 2), halfSize.y + spawnSize);

      GameObject newBlock = (GameObject) Instantiate (fallingBlockPrefab, spawnPos, Quaternion.Euler (Vector3.forward * spawnAngle));
      newBlock.transform.localScale = Vector3.one * spawnSize;
    }
  }
}
