using UnityEngine;
using System.Collections;

public class cameraTranslate : MonoBehaviour {
	public GameObject cameraGO;

	private bool click = false;
	private Vector2 curPos = Vector2.zero;
	private Vector3 beginPos;

	public float mouse;
	public float cameraMinX;
	public float cameraMaxX;
	public float cameraMinZ;
	public float cameraMaxZ;

	public float minScale;
	public float maxScale;
	// Use this for initialization
	void Start () {
		beginPos = cameraGO.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//click
		if (Input.GetMouseButtonDown(0)) {
			click = true;
			curPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		} else if (Input.GetMouseButtonUp(0)) {
			click = false;
			curPos = Vector2.zero;
		}
		if (click) {
			curPos -= new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			Vector3 trans = new Vector3(curPos.x/mouse, 0, curPos.y/mouse);
			Vector3 newPos = cameraGO.transform.position + trans;
			if (newPos.x>=cameraMinX && newPos.x<=cameraMaxX && newPos.z>=cameraMinZ && newPos.z<=cameraMaxZ) {
				cameraGO.transform.position += trans;
			}
			curPos = Input.mousePosition;
		}
		if(Input.mouseScrollDelta.y!=0) {
			float newScale = cameraGO.transform.eulerAngles.x + Input.mouseScrollDelta.y;
			if (newScale >= minScale && newScale <= maxScale) {
				cameraGO.transform.eulerAngles = new Vector3(newScale, 0, 0);
			}
		}
	}
}
