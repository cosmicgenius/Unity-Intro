using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ShapeManager))]
public class Window : MonoBehaviour {

  public int width, height;

  [System.NonSerialized]
  public Vector2 windowMin;
  [System.NonSerialized]
  public float unit;

  public bool[,] covered;

  ShapeManager shapeManager;

  void Start () {
    transform.localScale = new Vector3 (8f * width / height, 8f, 1);
    transform.position = new Vector3 (-80f / 9 + 1f + 4f * width / height, 0, 1);

    windowMin = transform.position - transform.localScale / 2f;
    unit = 8f / height;

    PuzzlePiece[] puzzlePieces = GameObject.FindObjectsOfType<PuzzlePiece> ();
    foreach (PuzzlePiece piece in puzzlePieces) {
      piece.gameObject.transform.localScale = new Vector3 (unit, unit, unit);
    }

    covered = new bool[width, height];

    shapeManager = GetComponent<ShapeManager> ();

  }

  public bool CanAddObject (int u, int v, ShapeManager.Shape shape, int rot) {
    int height = shapeManager.block[(int) shape].GetLength (0);
    int width = shapeManager.block[(int) shape].GetLength (1);

    if (rot % 2 == 1) {
      int t = height;
      height = width;
      width = t;
    }

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        bool exists = false;
        if (rot == 0) {
          exists = shapeManager.block[(int) shape][height - y - 1, x] == 1;
        } else if (rot == 1) {
          exists = shapeManager.block[(int) shape][x, y] == 1;
        } else if (rot == 2) {
          exists = shapeManager.block[(int) shape][y, width - x - 1] == 1;
        } else if (rot == 3) {
          exists = shapeManager.block[(int) shape][width - x - 1, height - y - 1] == 1;
        }

        if (exists && covered[u + x, v + y]) {
          return false;
        }
      }
    }
    return true;
  }

  public void EditObject (int u, int v, ShapeManager.Shape shape, int rot, bool add) {
    int height = shapeManager.block[(int) shape].GetLength (0);
    int width = shapeManager.block[(int) shape].GetLength (1);

    if (rot % 2 == 1) {
      int t = height;
      height = width;
      width = t;
    }

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {

        bool exists = false;
        if (rot == 0) {
          exists = shapeManager.block[(int) shape][height - y - 1, x] == 1;
        } else if (rot == 1) {
          exists = shapeManager.block[(int) shape][x, y] == 1;
        } else if (rot == 2) {
          exists = shapeManager.block[(int) shape][y, width - x - 1] == 1;
        } else if (rot == 3) {
          exists = shapeManager.block[(int) shape][width - x - 1, height - y - 1] == 1;
        }

        if (exists) {
          covered[u + x, v + y] = add;
        }
      }
    }
    Print ();
  }

  public void Print () {
    string output = "";
    for (int y = height - 1; y >= 0; y--) {
      for (int x = 0; x < width; x++) {
        output += covered[x, y].ToString () + ' ';
      }
      output += '\n';
    }
    Debug.Log (output);
  }
}
