using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
  public GameObject gameOverScreen;
  public Text secondsSurvived;
  bool gameOver;

  void Start () {
    FindObjectOfType<Player> ().onPlayerDeath += onGameOver;
  }


  void Update () {
    if (gameOver) {
      if (Input.GetKeyDown (KeyCode.Space)) {
        SceneManager.LoadScene (0);
      }
    }
  }

  void onGameOver () {
    gameOverScreen.SetActive (true);
    secondsSurvived.text = (Mathf.RoundToInt(Time.timeSinceLevelLoad)).ToString ();
    gameOver = true;
  }
}
