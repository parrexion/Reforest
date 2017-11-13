using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Builder builder;
	public ToolTip tt;
	public int buildingID;
	public string cost1;
	public string cost2;


	// Use this for initialization
	void Start () {
		Costs cost = builder.GetSelectedBuildingCosts(buildingID);
		cost1 = cost.aquaticCost.ToString();
		cost2 = cost.solarCost.ToString();
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		tt.Fade(cost1, cost2);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		tt.FadeOut();
	}
}
