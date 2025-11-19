using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panel
{
    public class RewardItem : MonoBehaviour
    {
        [SerializeField] protected Image itemImage;
        [SerializeField] protected TextMeshProUGUI multiplierTxt;

        public virtual void Fill(Sprite sprite, int amount)
        {
            itemImage.sprite = sprite;
            multiplierTxt.text = (amount < 0) ? "" : "x" + GetRewardMultiplierTxt(amount);
        }

        public string GetRewardMultiplierTxt(int amount)
        {
            int division = amount / 1000;
            return division == 0 ? amount.ToString() : division.ToString("D") + "K";
        }
    }
}