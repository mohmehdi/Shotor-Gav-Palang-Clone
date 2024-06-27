using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnRestart;
    public UnityEvent OnWinLevel;
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
    public void Restart(){
        OnRestart.Invoke();
    }
}
