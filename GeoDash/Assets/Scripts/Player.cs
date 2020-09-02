using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

  Rigidbody2D rigidBody;
  BoxCollider2D boxCollider;
  public float acceleration;
  public float angularVelocity;
  public float normalSpeed;

  bool canJump = false, justJumped = false, doubleJump = false;

  void Start () {
    rigidBody = gameObject.GetComponent<Rigidbody2D> ();
    boxCollider = gameObject.GetComponent<BoxCollider2D> ();
  }

  void Update () {
    if ((canJump || doubleJump) && !justJumped) {
      if (Input.GetKey (KeyCode.Space)) {
        justJumped = true;

        rigidBody.velocity = new Vector2(normalSpeed, acceleration);
        rigidBody.angularVelocity = angularVelocity;
      }
    }

    if (!(canJump || doubleJump)) {
      justJumped = false;
    }
  }

  void FixedUpdate () {
    rigidBody.velocity = new Vector2 (normalSpeed, rigidBody.velocity.y);

    canJump = boxCollider.IsTouchingLayers(LayerMask.GetMask("Obstacles"));
  }

  void OnTriggerEnter2D (Collider2D triggerCollider) {
    if (triggerCollider.tag == "Sharp") {
      SceneManager.LoadScene (0);
    } else if (triggerCollider.tag == "Jump") {
      doubleJump = true;
    }
  }

  void OnTriggerExit2D (Collider2D triggerCollider) {
    if (triggerCollider.tag == "Jump") {
      doubleJump = false;
    }
  }


}
