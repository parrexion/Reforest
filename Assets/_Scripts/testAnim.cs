using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnim : MonoBehaviour {
	public Animation animation;

	// Use this for initialization
	void Start () {
		animation = GetComponent<Animation>();
		animation.Play(animation.clip.name);
        //yield return new WaitForSeconds(animation.clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
