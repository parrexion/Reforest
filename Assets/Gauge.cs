using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gauge : MonoBehaviour {

	public float someValue;
	public float maxValue;
	public Image bar;
	public float width = 250;
	public Text t;
	
	private float initialWidth;

	void Update() 
	{

		Visualize();
	}

	void Visualize() 
	{
		float percent = someValue/maxValue;
		bar.rectTransform.sizeDelta = new Vector2(percent * width, bar.rectTransform.sizeDelta.y);	

		t.text = someValue + " / " + maxValue;

	}

}
