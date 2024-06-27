using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreezeTime : MonoBehaviour
{
    public static FreezeTime Instance;
    public UnityEvent OnFreeze;
    public UnityEvent OnDeFreeze;

    private bool isFreeze = false;
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
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = isFreeze ? 1 : 0;
            isFreeze = !isFreeze;

            if (isFreeze)
                OnFreeze.Invoke();
            else
                OnDeFreeze.Invoke();
                
        }
    }
}
