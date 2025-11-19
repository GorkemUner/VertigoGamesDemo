using UnityEngine;
using UnityEngine.UI;

public class BombPanel : MonoBehaviour
{
    private Button restartBtn;
    private Button reviveBtn;

    private void OnValidate()
    {
        var btns = GetComponentsInChildren<Button>(true);
        restartBtn = btns[0];
        reviveBtn = btns[1];
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
        transform.gameObject.SetActive(false);
    }
}
