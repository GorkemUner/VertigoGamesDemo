using UnityEngine;
using UnityEngine.UI;

public class CollectedPanel : MonoBehaviour
{
    private Button restartBtn;

    private void OnValidate()
    {
        restartBtn = GetComponentInChildren<Button>(true);
    }

    private void OnEnable()
    {
        restartBtn.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        restartBtn.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        GameManager.Instance.Restart();
        HidePanel();
    }

    private void HidePanel()
    {
        transform.gameObject.SetActive(false);
    }
}
