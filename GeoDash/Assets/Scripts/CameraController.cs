using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  GameObject player;
  public float offset;

  void Start () {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  void Update () {
    transform.position = new Vector3(player.transform.position.x + offset, 0, -1);
  }
}
