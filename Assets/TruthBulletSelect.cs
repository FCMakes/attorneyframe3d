using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruthBulletSelect : FCMakes.LazyPort.ButtonInputManager
{
    public string toselect;
    public void Update()
    {
        if (this.GetKeyDown)
        {
            TruthBulletsController tbc = GameObject.FindObjectOfType<TruthBulletsController>();
            
                tbc.PlaySE();
            
            tbc.SetActiveBullet(toselect);

        }
    }
}
