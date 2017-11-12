using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolTip : MonoBehaviour {

	public Image bg;

	public Text t0;
	public Text t1;

	public Text t2;

	public GameObject child0;
	public GameObject child1;
	

	public float max = -180;
	public float min = -220;

	public float target;

	public float step = 0.5f;
	
	private bool move;

	private bool errorMode;

	float timer = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(move)
			if(bg.rectTransform.localPosition.y != target) 
			{
				bg.rectTransform.localPosition = new Vector2(bg.rectTransform.localPosition.x, Mathf.Lerp(bg.rectTransform.localPosition.y, target, step));
			}else 
			{
				move = false;
			}





			if(errorMode) 
			{
				child0.SetActive(false);
				child1.SetActive(true);


				if(timer >= 2) { FadeOut(); }
			}else 
			{
				child0.SetActive(true);
				child1.SetActive(false);

			}

	}

	public void Fade(string s, string s1) 
	{
		errorMode = false;
		move = true;

		t0.text = s;
		t1.text = s1;
		target = max;
		
	}
	public void FadeOut() 
	{
		errorMode = false;
		move = true;

		target = min;
	}

	public void ErrorMsg(string s) 
	{
		errorMode = true;
		move = true;

		t2.text = s;
		target = max;
		timer = 0;	
	}

}
