using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

  void Start () {
    transform.position = new Vector3 (0, 0, transform.position.z);
    transform.localScale = new Vector3 (0.2f * Camera.main.aspect * Camera.main.orthographicSize + 0.01f, 0.2f * Camera.main.orthographicSize + 0.01f, transform.localScale.z);
  }
}
