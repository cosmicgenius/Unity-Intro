using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class PuzzlePiece : MonoBehaviour {

  private Vector2 mOffset;
  private Vector2 startPos;
  private Vector2Int startUvs;
  private bool fromInsideWindow;

  public SpriteRenderer spriteRenderer;
  public ShapeManager.Shape shape;

  public Window window;

  void Start () {
    spriteRenderer = GetComponent<SpriteRenderer> ();

    window = GameObject.FindGameObjectWithTag ("Window").GetComponent<Window> ();
  }

  void OnMouseDown () {
    mOffset = transform.position - Camera.main.ScreenToWorldPoint (Input.mousePosition);
    startPos = transform.position;

    Bounds bounds = spriteRenderer.bounds;

    int uLeft = Mathf.RoundToInt ((bounds.min.x - window.windowMin.x) / window.unit),
        uRight = Mathf.RoundToInt ((bounds.max.x - window.windowMin.x) / window.unit);
    int vBottom = Mathf.RoundToInt ((bounds.min.y - window.windowMin.y) / window.unit),
        vTop = Mathf.RoundToInt ((bounds.max.y - window.windowMin.y) / window.unit);

    startUvs = new Vector2Int (uLeft, vBottom);

    fromInsideWindow = false;
    if (uLeft >= 0 && uRight <= window.width && vBottom >= 0 && vTop <= window.height) {
      fromInsideWindow = true;
      window.EditObject (uLeft, vBottom, shape, Mathf.RoundToInt (transform.localEulerAngles.z / 90f), false);
    }

    transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
  }

  void OnMouseDrag () {
    float originalZ = transform.position.z;
    transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + (Vector3) mOffset;
    transform.position = new Vector3 (transform.position.x, transform.position.y, originalZ);


    Snap ();
  }

  void OnMouseUp () {
    Snap ();

    Bounds bounds = spriteRenderer.bounds;

    int uLeft = Mathf.RoundToInt ((bounds.min.x - window.windowMin.x) / window.unit),
        uRight = Mathf.RoundToInt ((bounds.max.x - window.windowMin.x) / window.unit);
    int vBottom = Mathf.RoundToInt ((bounds.min.y - window.windowMin.y) / window.unit),
        vTop = Mathf.RoundToInt ((bounds.max.y - window.windowMin.y) / window.unit);

    if (uLeft >= 0 && uRight <= window.width && vBottom >= 0 && vTop <= window.height) {
      if (window.CanAddObject (uLeft, vBottom, shape, Mathf.RoundToInt (transform.localEulerAngles.z / 90f))) {
        window.EditObject (uLeft, vBottom, shape, Mathf.RoundToInt (transform.localEulerAngles.z / 90f), true);
      } else {
        transform.position = startPos;
        if (fromInsideWindow) {
          window.EditObject (startUvs.x, startUvs.y, shape, Mathf.RoundToInt (transform.localEulerAngles.z / 90f), true);
        }
      }
    }
    transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
  }

  void Snap () {
    Bounds bounds = spriteRenderer.bounds;

    // uvs for bounds of the sprite to calculate snapping to grid
    int uLeft = Mathf.RoundToInt ((bounds.min.x - window.windowMin.x) / window.unit),
        uRight = Mathf.RoundToInt ((bounds.max.x - window.windowMin.x) / window.unit);
    int vBottom = Mathf.RoundToInt ((bounds.min.y - window.windowMin.y) / window.unit),
        vTop = Mathf.RoundToInt ((bounds.max.y - window.windowMin.y) / window.unit);

    if (uLeft >= 0 && uRight <= window.width && vBottom >= 0 && vTop <= window.height) {
      transform.position += new Vector3 (window.windowMin.x + uLeft * window.unit - bounds.min.x, window.windowMin.y + vBottom * window.unit - bounds.min.y, 0);
    }

  }


  /*public Row[] grid = new Row[] {
    new Row (0, 1, 1),
    new Row (0, 0, 1),
    new Row (0, 1, 1)
  };

  Mesh mesh;
  public List<Vector3> vertices = new List<Vector3> ();
  public List<int> triangles = new List<int> ();

  GameObject parent;

  void Start () {
    mesh = new Mesh ();
    GetComponent<MeshFilter> ().mesh = mesh;

    GenerateMesh();
  }

  void GenerateMesh () {
    int width = grid.GetLength (0);
    int height = grid[0].row.GetLength(0);

    int[,] gridIndex = new int[width + 1, height + 1];

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        if (grid[y].row[x] == 1) {
          gridIndex[x, y] = 1;
          gridIndex[x + 1, y] = 1;
          gridIndex[x, y + 1] = 1;
          gridIndex[x + 1, y + 1] = 1;
        }
      }
    }
    int vertexIndex = 0;
    for (int y = 0; y <= height; y++) {
      for (int x = 0; x <= width; x++) {
        if (gridIndex[x, y] > 0) {
          gridIndex[x, y] = ++vertexIndex;
          vertices.Add (new Vector3 (x / 2f, y / 2f, 0));
        }
      }
    }
    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        if (grid[y].row[x] == 1) {
          triangles.Add (gridIndex[x, y + 1] - 1);
          triangles.Add (gridIndex[x + 1, y] - 1);
          triangles.Add (gridIndex[x, y] - 1);
          triangles.Add (gridIndex[x + 1, y] - 1);
          triangles.Add (gridIndex[x, y + 1] - 1);
          triangles.Add (gridIndex[x + 1, y + 1] - 1);
        }
      }
    }
    mesh.Clear ();

    mesh.vertices = vertices.ToArray ();
    mesh.triangles = triangles.ToArray ();
    mesh.RecalculateNormals ();

  }*/
}

/*public class Row {
  public int[] row;

  public Row (params int[] row) {
    this.row = row;
  }

}*/