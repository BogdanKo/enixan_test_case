using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public GameObject Plane;

    public int Width;
    public int Height;

    private GameObject[,] grid = new GameObject[10, 10];

    void GridDraw() {

        for (int x = 0; x < Width; x++) {

            for (int z = 0; z < Height; z++) {

                GameObject gridPlane = (GameObject)Instantiate(Plane);

                gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x, gridPlane.transform.position.y, gridPlane.transform.position.z + z);
                grid[x, z] = gridPlane;

            }
        }

    }

    void Awake() {

        GridDraw();

    }

    void onGUI() {

        if (GUI.Button(new Rect(10, 10, 50, 50), "Grid")) {

            Destroy(grid[10,10]);
        }

    }
	void Start () {

    }
	
	void Update () {
	
	}
}
