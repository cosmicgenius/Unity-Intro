using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

  void Start () {
    transform.position = new Vector3 (0, transform.position.y, transform.position.z);
    transform.localScale = new Vector3 (2f * Camera.main.aspect * Camera.main.orthographicSize + 0.1f, transform.localScale.y, transform.localScale.z);
  }
}
