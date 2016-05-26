using UnityEngine;
using System.Collections;

public class generate : MonoBehaviour {
	public GameObject tree;
	public GameObject stone;
	public float treeY;
	public float treeRot;
	public float stoneY;
	public float terrainWidth;
	public int radius;
	public int maxNumber;
	public int minNumber;
	
	// Use this for initialization
	void Start () {
		int current = Random.Range (minNumber, maxNumber);
		for (int i=0; i<current; i++) {
			int cur = (int)Random.Range(0,2);
			Vector3 pos = Vector3.zero;
			//tree
			if(cur==0) {
				pos = new Vector3(
					Random.Range(-terrainWidth,terrainWidth),
					treeY,
					Random.Range(-terrainWidth,terrainWidth));
				//stone
			} else {
				pos = new Vector3(
					Random.Range(-terrainWidth,terrainWidth) + 6.29f,
					stoneY,
					Random.Range(-terrainWidth,terrainWidth) + 16.15f);
			}
			if((pos.x*pos.x + pos.z*pos.z)>=(radius*radius)){
				Quaternion rot = Quaternion.Euler(0, treeRot, 0);
				if(cur==0) {
					Instantiate(tree,pos,rot);
				} else {
					Instantiate(stone,pos,rot);
				}
			} else 
				i--;
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
