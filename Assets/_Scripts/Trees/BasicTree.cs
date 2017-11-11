using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTree : BaseTree {

    public Transform[] trees;

    void Start() {
        base.Start();
        
        trees = GetComponentsInChildren<Transform>();
    }

    protected override void Grow() {
        transform.localPosition = new Vector3(0,(2+currentGrowthLevel)*0.25f,0);
		currentGrowthLevel++;
		currentGrowthTime = 0;
        transform.localScale = new Vector3(1,(1+currentGrowthLevel),1);
    }

    protected override void DeGrow() {
        transform.localPosition = new Vector3(0,(1+currentGrowthLevel)*0.25f,0);
        transform.localScale = new Vector3(1,(currentGrowthLevel),1);

        if (currentGrowthLevel <= 0) {
            ChangeTreeType(0);
        }
    }
}
