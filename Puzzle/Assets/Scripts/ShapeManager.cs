using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour {
  public enum Shape {
    None = 0,
    LTriomino,
    JTetromino,
    LTetromino
  }

  public int[][,] block = new int[][,] {
    new int[,] {
      {0}
    },
    new int[,] {
      {0, 1},
      {1, 1}
    },
    new int[,] {
      {0, 1},
      {0, 1},
      {1, 1}
    },
    new int[,] {
      {1, 0},
      {1, 0},
      {1, 1}
    }
  };
}
