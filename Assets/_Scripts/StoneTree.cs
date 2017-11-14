using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTree : BaseTree 
{

bool grownUp;

    protected override void Grow() {
		
		if(grownUp)
			return;		
       
	    transform.localPosition = new Vector3(0, (maxGrowthLevel - currentGrowthLevel) *  -0.5f, 0);
		currentGrowthLevel++;
		currentGrowthTime = 0;
        transform.localScale = new Vector3(1, 1,1);
		
		if(currentGrowthLevel >= maxGrowthLevel) 
			grownUp = true;
    }

    protected override void DeGrow() {
        transform.localPosition = new Vector3(0,(maxGrowthLevel - currentGrowthLevel) * -0.5f,0);
        transform.localScale = new Vector3(1,(currentGrowthLevel), 1 );

        if (currentGrowthLevel <= 0) {
            ChangeTreeType(0);
        }
    }

}
