using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPics : MonoBehaviour {

	public float changeTime = 5f;
	private float currentTime;
	public int currentPic = 0;

	public Image[] images;

	// Use this for initialization
	void Start () {
		showPic();
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= changeTime) {
			showPic();
			currentTime = 0;
		}
	}

	public void showPic(){
		if (currentPic >= 6){
			SceneMan.NewGame();
			return;
		}
		for (int i = 0; i < images.Length; i++)
		{
			images[i].enabled = (i == currentPic);
		}
		currentPic++;
	}
}
