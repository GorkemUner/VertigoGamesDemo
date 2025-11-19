using System;
using UnityEngine;

public class RandomProvider : Singleton<RandomProvider>, IWheelDataProvider
{
    [SerializeField] private int priority = 50;
    public int Priority => priority;

    private BaseRandomStrategy strategy;
    public BaseRandomStrategy Strategy => strategy;

    [Space]
    [Header ("VertigoGamesStrategyAssets")]
    [SerializeField] public WheelSO normalZoneSO;
    [SerializeField] public WheelSO safeZoneSO;
    [SerializeField] public WheelSO superZoneSO;
    [Space]

    [RandomStrategySelector]
    [SerializeField] private string selectedStrategyClassName;

    private void Awake()
    {
        strategy = CreateStrategyInstance();
    }

    private void OnEnable()
    {
        WheelResolver.Register(this);
    }

    public WheelData GetData(int zone)
    {
        return Strategy.Generate();
    }

    private BaseRandomStrategy CreateStrategyInstance()
    {
        var type = Type.GetType(selectedStrategyClassName);
        if (type == null)
            Debug.LogError("Strategy is not found: " + selectedStrategyClassName);
        return Activator.CreateInstance(type) as BaseRandomStrategy;
    }
}