using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IGameState
{
    public void Enter()
    {
        GameManager.Instance.CollectPanel.SetActive(true);
    }

    public void Exit()
    {
        GameManager.Instance.CollectPanel.SetActive(false);
    }
}
