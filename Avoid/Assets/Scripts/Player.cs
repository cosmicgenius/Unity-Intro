using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public float speed = 7;
  public event System.Action onPlayerDeath;

  float halfWidth;

  void Start () {
    float halfPlayerWidth = transform.localScale.x / 2f;
    halfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
  }

  void Update () {
    Vector2 displacement = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0);

    transform.Translate (displacement.normalized * speed * Time.deltaTime);

    if (transform.position.x < -halfWidth) {
      transform.position = new Vector2 (halfWidth, transform.position.y);
    } else if (transform.position.x > halfWidth) {
      transform.position = new Vector2 (-halfWidth, transform.position.y);
    }
  }

  void OnTriggerEnter2D (Collider2D triggerCollider) {
    if (triggerCollider.tag == "Falling Block") {
      if (onPlayerDeath != null) {
        onPlayerDeath ();
      }
      Destroy (gameObject);
    }
  }
}
