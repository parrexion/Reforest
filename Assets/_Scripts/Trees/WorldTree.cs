using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTree : BaseTree {

    protected override void Grow() {

    }

    protected override void DeGrow() {
        transform.localPosition = new Vector3(0,(1+currentGrowthLevel)*0.25f,0);
        transform.localScale = new Vector3(1,(currentGrowthLevel),1);

        if (currentGrowthLevel <= 0) {
            SceneMan.GameOver();
        }
    }
    
}
