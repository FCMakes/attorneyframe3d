using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoomObject : InvestigationObject
{
    public int CanLeaveFRM;
    public int CantLeaveFRM;
    public bool canLeave;

    public void Leave()
    {
        GameObject.FindObjectOfType<ModifyStrings>().canInvestigate = false;
        GameObject.FindObjectOfType<ModifyStrings>().LoadFromFRM(FRM);
    }
    public void Update()
    {
        if (canLeave && this.FRM != CanLeaveFRM)
        {
            this.FRM = CanLeaveFRM;
            if (Investigated)
            {
                Investigated = false;
            }
        }
        if (!canLeave && this.FRM != CantLeaveFRM)
        {
            this.FRM = CantLeaveFRM;
        }

    }


}
