using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolTip : MonoBehaviour {

	public Image bg;

	public Text t0;
	public Text t1;

	public Text t2;

	public GameObject resourcesLayout;
	public GameObject errorLayout;
	

	public float ymax = 170; //Show
	public float ymin = 15; //Hide

	public float target;

	public float step = 0.5f;
	
	private bool move;

	private bool errorMode;

	float timer = 0;


	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(move) {
			if(bg.rectTransform.localPosition.y != target)
				bg.rectTransform.localPosition = new Vector2(bg.rectTransform.localPosition.x, Mathf.Lerp(bg.rectTransform.localPosition.y, target, step));
			else
				move = false;
		}

		if(errorMode) 
		{
			resourcesLayout.SetActive(false);
			errorLayout.SetActive(true);

			if (timer >= 2)
				FadeOut();

		} else {
			resourcesLayout.SetActive(true);
			errorLayout.SetActive(false);
		}

	}

	/// <summary>
	/// Show the tooltip.
	/// </summary>
	/// <param name="s"></param>
	/// <param name="s1"></param>
	public void FadeIn(string s, string s1) {
		errorMode = false;
		move = true;
		t0.text = s;
		t1.text = s1;
		target = ymax;
	}

	/// <summary>
	/// Hide the tooltip.
	/// </summary>
	public void FadeOut() {
		errorMode = false;
		move = true;
		target = ymin;
	}

	/// <summary>
	/// Show an error message in the tooltip.
	/// </summary>
	/// <param name="s"></param>
	public void ErrorMsg(string s) {
		errorMode = true;
		move = true;
		t2.text = s;
		target = ymax;
		timer = 0;	
	}

}
