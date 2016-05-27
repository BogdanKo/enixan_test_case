using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class progressbar : MonoBehaviour {
	public float minPos;
	public float maxPos;
	
	public Image img;
	public Image Shop;
	public Image rusText;
	public Image engText;
	public Text txt;
	public GameObject grid;

	public GameObject tree;
	public GameObject stone;
	public GameObject particle;

	private float curPercent = 0;
	private float time = 0;

	private Rect r1,r2;
	private GameObject nGo;

	//0 for tree 1 for stone
	private int number = 0;

	private int gridNumber = 16;
	private bool[,] matrix;

	//terrian minPos and length
	private int startPos = -5;
	private int length = 10;

	private bool clickSpam= false;

	[SerializeField]
	Toggle tg;
	[SerializeField]
	Toggle rus;
	// Use this for initialization
	void Start () {
		engText.enabled = false;
		int sizeBut = 111;
		int posX = 27, posY = 215;
		r1 = new Rect((Screen.width - 600)/2f + posX,
		                  (Screen.height - 400)/2f + posY,
		                  sizeBut,
		                  sizeBut);
		posX += 10 + sizeBut;
		r2 = new Rect((Screen.width - 600)/2f + posX,
		              (Screen.height - 400)/2f + posY,
		              sizeBut,
		              sizeBut);
		matrix = new bool[gridNumber,gridNumber];
		for (int i=0; i<gridNumber; i++) {
			for (int j=0; j<gridNumber; j++) {
				matrix[i,j] = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool shopActive = Shop.IsActive ();
		Vector3 curMouse = Input.mousePosition;
		time += Time.deltaTime;
		if (time >= 60) {
			time -=60;
		}
		curPercent = time / 60f;
		txt.text = (int)time + "s";
		img.rectTransform.sizeDelta = new Vector2 (-(maxPos - minPos) * (1 - curPercent),
		                                           img.rectTransform.sizeDelta.y);
		if(Input.GetMouseButtonDown(0)&&shopActive) {
			if(curMouse.x>=r1.xMin && curMouse.x<r1.xMax && curMouse.y>=r1.yMin &&curMouse.y<r1.yMax) {
				Shop.gameObject.SetActive (false);
				nGo = Instantiate(tree);
				nGo.transform.position = curMouse;
				nGo.transform.parent = null;
				number = 0;
				particle.SetActive(true);
				clickSpam = true;
			} else if(curMouse.x>=r2.xMin && curMouse.x<r2.xMax && curMouse.y>=r2.yMin &&curMouse.y<r2.yMax) {
				Shop.gameObject.SetActive (false);
				nGo = Instantiate(stone);
				nGo.transform.position = curMouse;
				nGo.transform.parent = null;
				number = 1;
				particle.SetActive(true);
				clickSpam = true;
			}
		}
		int ngoX=0,ngoY=0;
		if (nGo != null) {
			if(number==0) {
				ngoX = (int)((nGo.transform.position.x - startPos)/length * gridNumber);
				ngoY = (int)((nGo.transform.position.z - startPos)/length * gridNumber);
			} else {
				ngoX = (int)((nGo.transform.position.x + 6.29f - startPos)/length * gridNumber);
				ngoY = (int)((nGo.transform.position.z + 16.15f - startPos)/length * gridNumber);
			}
			Ray ray = Camera.main.ScreenPointToRay(curMouse);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Vector3 ngoPos = hit.point;
				if(number==0) {
					ngoPos.y = -0.35f;
				} else {
					ngoPos.x -= 6.29f;
					ngoPos.y = -7f;
					ngoPos.z -= 16.15f;
				}
				nGo.transform.position = ngoPos;
			}
			if(ngoX<gridNumber && ngoY<gridNumber) {
				if(!matrix[ngoX,ngoY]) {
					float lenElem = length/(gridNumber+0f);
					float posX = ngoX*lenElem + lenElem/2f + startPos;
					float posY = ngoY*lenElem + lenElem/2f + startPos;
					particle.transform.position = new Vector3(posX, -0.117f,posY);
					particle.SetActive(true);
				} else {
					particle.SetActive(false);
				}
			}
		}
		if(Input.GetMouseButtonDown(0)&&!shopActive&&clickSpam) {
			if(!matrix[ngoX,ngoY]) {
				if(number==0) {
					nGo = Instantiate(tree);
				} else {
					nGo = Instantiate(stone);
				}
				nGo.transform.position = curMouse;
				nGo.transform.parent = null;
				matrix[ngoX,ngoY] = true;
			}
		}

	}
	public void GridChange() {
		grid.SetActive (tg.isOn);
	}
	public void RusChange() {
		if (rus.isOn) {
			rusText.enabled = true;
			engText.enabled = false;
		} else {
			rusText.enabled = false;
			engText.enabled = true;
		}
	}
	public void closeShop() {
		Shop.gameObject.SetActive (false);
	}
	public void showShop() {
		Shop.gameObject.SetActive (true);
	}
}
