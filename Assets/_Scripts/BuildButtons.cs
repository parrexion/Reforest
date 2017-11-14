using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtons : MonoBehaviour {

	public Sprite[] deactive;
	public Sprite[] active;

	public Image[] buttonImages;
	

	public void UpdateButtonSprites (int selectedBuilding) 
	{
		for (int i = 0; i < buttonImages.Length; i++)
		{
			buttonImages[i].sprite = (i == (selectedBuilding-1)) ? active[i] : deactive[i];
		}
	}
}
