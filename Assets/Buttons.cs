using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {


	public Sprite[] deactive;
	public Sprite[] active;

	public Image i0;
	public Image i1;
	public Image i2;
	


	// Update is called once per frame
	void UpdateButtonSprites () 
	{
		switch(Stats.instance.selectedBuilding) 
		{	
			case 0: 
			i0.sprite = active[0];
			i1.sprite = deactive[1];
			i2.sprite = deactive[2];
				break;
			case 1:
			i0.sprite = deactive[0];
			i1.sprite = active[1];
			i2.sprite = deactive[2]; 
				break;

			case 2: 
			i0.sprite = deactive[0];
			i1.sprite = deactive[1];
			i2.sprite = active[2];
				break;
		}
	}
}
