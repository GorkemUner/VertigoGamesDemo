using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRandomStrategy
{
    //public abstract List<WheelData> FixWrongData(List<WheelData> wheelData);
    public abstract WheelData Generate();
    //protected abstract void FillNonFullWhellData(WheelData wheelData);
    //protected abstract void CutOverFlowedWhellData(WheelData wheelData);
    //protected abstract void FillRewardId(WheelData wheelData);
}