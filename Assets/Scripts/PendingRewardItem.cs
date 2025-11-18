using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendingRewardItem : RewardItem
{
    int amount;

    public override void Fill(Sprite sprite, int amount)
    {
        base.Fill(sprite,amount);
        this.amount = amount;
    }

    public void AddAmount(int amount)
    {
        this.amount += amount;
        multiplierTxt.text = "x" + GetRewardMultiplierTxt(this.amount);
    }
}
