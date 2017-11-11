using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTree : BaseTree {


    protected override void Grow() {
        transform.localScale *= 1.2f;
		currentGrowthLevel++;
		currentGrowthTime = 0;
    }

}
