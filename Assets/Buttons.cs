using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Builder))]
public class Buttons : MonoBehaviour {

	public Sprite[] deactive;
	public Sprite[] active;

	public Image[] buttonImages;
	

	public void UpdateButtonSprites (int selectedBuilding) 
	{
		for (int i = 0; i < buttonImages.Length; i++)
		{
			buttonImages[i].sprite = (i == selectedBuilding) ? active[i] : deactive[i];
		}
	}
}
