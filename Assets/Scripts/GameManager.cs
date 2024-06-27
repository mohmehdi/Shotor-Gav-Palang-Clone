using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public UnityEvent OnFreeze;
    public UnityEvent OnDeFreeze;
    public UnityEvent OnRestart;
    public UnityEvent OnWinLevel;
    GoalPlatform[] _platfroms = new GoalPlatform[0];
    bool isFreeze = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        _platfroms = FindObjectsOfType<GoalPlatform>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FreezeGame();
        }
    }
    public void CheckObjective()
    {
        foreach (var p in _platfroms)
        {
            if (!p.check)
                return;
        }
        OnWinLevel.Invoke();
    }
    public void Restart()
    {
        OnRestart.Invoke();
    }

    public void FreezeGame()
    {
        Time.timeScale = isFreeze ? 1 : 0;
        isFreeze = !isFreeze;

        if (isFreeze)
            OnFreeze.Invoke();
        else
            OnDeFreeze.Invoke();
    }
}
