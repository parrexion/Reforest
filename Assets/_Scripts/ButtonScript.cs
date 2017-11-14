using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Builder builder;
	public ToolTip tt;
	public int buildingID;
	public string costAqua;
	public string costSun;


	// Use this for initialization
	void Start () {
		Costs cost = builder.GetSelectedBuildingCosts(buildingID);
		costAqua = cost.aquaticCost.ToString();
		costSun = cost.solarCost.ToString();
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		tt.FadeIn(costAqua, costSun);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		tt.FadeOut();
	}
}
