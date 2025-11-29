using System;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Panel
{
    public class InfiniteZoneSlider : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> items;
        [SerializeField] private float itemWidth;
        [SerializeField] private float speed;
        [SerializeField] private float leftBorderX;
        [SerializeField] private float shiftTime = .4f;

        private int zoneCount = 19;
        private int spacing = 50;
        private bool isAnimating = false;

        public void ShiftLeft(Action EndAction = null)
        {
            if (isAnimating) return;
            isAnimating = true;

            float shiftAmount = itemWidth + spacing;

            int completedTweens = 0;

            foreach (var item in items)
            {
                item.DOAnchorPosX(item.anchoredPosition.x - shiftAmount, shiftTime)
                    .SetEase(Ease.Linear)
                    .SetId(this)
                    .OnComplete(() =>
                    {
                        completedTweens++;

                        if (completedTweens == zoneCount)
                        {
                            ReorderItems(shiftAmount);
                            isAnimating = false;
                            EndAction?.Invoke();
                        }
                    });
            }
        }

        private void ReorderItems(float shiftAmount)
        {
            RectTransform first = items[0];

            if (first.anchoredPosition.x <= leftBorderX)
            {
                items.RemoveAt(0);
                items.Add(first);

                RectTransform last = items[items.Count - 2];

                first.anchoredPosition = new Vector2(
                    last.anchoredPosition.x + shiftAmount,
                    first.anchoredPosition.y
                );

                first.GetComponent<TMPro.TextMeshProUGUI>().text = (zoneCount + Convert.ToInt32(first.GetComponent<TMPro.TextMeshProUGUI>().text)).ToString();
            }
        }

        public void Restart()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].anchoredPosition = new Vector2(100 * i, items[i].anchoredPosition.y);
                items[i].GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            }
        }

        void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}