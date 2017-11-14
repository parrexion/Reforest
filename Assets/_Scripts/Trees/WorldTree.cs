using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTree : BaseTree {

    private float startScale;

    void Start()
    {
        startScale = transform.localScale.y;
    }

    protected override void Grow() {

    }

    protected override void DeGrow() {
        //transform.localPosition = new Vector3(0,(1+currentGrowthLevel)*0.25f,0);
        transform.localScale = new Vector3(startScale,((startScale / 10) * currentGrowthLevel),startScale);

        if (currentGrowthLevel <= 0) {
            SceneMan.GameOver();
        }
    }
    
}
