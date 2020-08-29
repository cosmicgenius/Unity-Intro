using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {
  public Transform targetTransform;
  public float speed = 7;

  void Update () {
    Vector3 displacement = targetTransform.position - transform.position;
    Vector3 direction = displacement.normalized;
    Vector3 velocity = direction * speed;

    float distance = displacement.magnitude;
    if (distance > 1.5) transform.Translate (velocity * Time.deltaTime);
  }
}
