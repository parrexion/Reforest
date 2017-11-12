using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public ToolTip tt;

	public int spellID;

	public string cost1;
	public string cost2;


	// Use this for initialization
	void Start () {
		cost1 = Stats.instance.costs[spellID].aquaticCost.ToString();
		cost2 = Stats.instance.costs[spellID].solarCost.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
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
