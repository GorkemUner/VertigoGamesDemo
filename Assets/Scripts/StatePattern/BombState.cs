using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombState : IGameState
{
    public void Enter()
    {
        GameManager.Instance.BombPanel.SetActive(true);
    }

    public void Exit()
    {
        GameManager.Instance.BombPanel.SetActive(false);
    }

}
