using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClass : MonoBehaviour {

	private Image img;

	void Start () {
		img = GetComponent<Image> ();
	}

	void Update () {
		Debug.Log (img.color);
		img.color += Color.white * Time.deltaTime;
	}

	public void ChangeRed(){
		img.color = Color.red;
	}

}
