using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolTip : MonoBehaviour {

	public Image background;

	public Text costAqua;
	public Text costSun;
	public Text errorText;

	public GameObject resourcesLayout;
	public GameObject errorLayout;
	
	public float ymax = -365f; //Show
	public float ymin = -520f; //Hide

	public float target;
	public float step = 0.5f;
	private bool move;
	private bool errorMode;
	float timer = 0;


	void Start() {
		transform.localPosition = new Vector3(transform.localPosition.x,ymin,transform.localPosition.z);
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(move) {
			if(background.rectTransform.localPosition.y != target)
				background.rectTransform.localPosition = new Vector2(background.rectTransform.localPosition.x, Mathf.Lerp(background.rectTransform.localPosition.y, target, step));
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
		costAqua.text = s;
		costSun.text = s1;
		target = ymax;
	}

	/// <summary>
	/// Hide the tooltip.
	/// </summary>
	public void FadeOut() {
		target = ymin;
	}

	/// <summary>
	/// Show an error message in the tooltip.
	/// </summary>
	/// <param name="s"></param>
	public void ErrorMsg(string s) {
		Debug.Log(s);
		errorText.text = s;
		target = ymax;
		timer = 0;
		resourcesLayout.SetActive(false);
		errorLayout.SetActive(true);
	}

}
