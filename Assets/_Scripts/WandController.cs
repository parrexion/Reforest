using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour {

	private Valve.VR.EVRButtonId leftButton = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
	private Valve.VR.EVRButtonId rightButton = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
	private Valve.VR.EVRButtonId upButton = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
	private Valve.VR.EVRButtonId downButton = Valve.VR.EVRButtonId.k_EButton_DPad_Down;

	public bool leftButtonDown = false;
	public bool leftButtonUp = false;
	public bool leftButtonPressed = false;

	public bool rightButtonDown = false;
	public bool rightButtonUp = false;
	public bool rightButtonPressed = false;

	public bool upButtonDown = false;
	public bool upButtonUp = false;
	public bool upButtonPressed = false;

	public bool downButtonDown = false;
	public bool downButtonUp = false;
	public bool downButtonPressed = false;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); }}
	private SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		leftButtonDown = controller.GetPressDown(leftButton);
		leftButtonUp = controller.GetPressUp(leftButton);
		leftButtonPressed = controller.GetPress(leftButton);

		rightButtonDown = controller.GetPressDown(rightButton);
		rightButtonUp = controller.GetPressUp(rightButton);
		rightButtonPressed = controller.GetPress(rightButton);

		upButtonDown = controller.GetPressDown(upButton);
		upButtonUp = controller.GetPressUp(upButton);
		upButtonPressed = controller.GetPress(upButton);

		downButtonDown = controller.GetPressDown(downButton);
		downButtonUp = controller.GetPressUp(downButton);
		downButtonPressed = controller.GetPress(downButton);
	}
}
