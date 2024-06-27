using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swapper : MonoBehaviour
{
    public UnityEvent<BehaviorController> OnFirstSelected;
    public UnityEvent OnFirstUnSelected;
    public UnityEvent<BehaviorController,BehaviorController> OnSwap;
    [SerializeField] LayerMask elements_mask;
    [SerializeField] LayerMask light_mask;
    BehaviorController _first = null;
    BehaviorController _second = null;
    Camera _camera;
    void Awake() => _camera = Camera.main;
    private void OnEnable() => OnSwap.AddListener(Swap);
    private void OnDisable() => OnSwap.RemoveListener(Swap);
    void Update() // TODO: this is sooooo smelly pffff X/
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit_lit = Physics2D.Raycast(mousePos, Vector2.zero, 1, light_mask);
            if (hit_lit.collider == null)
            {
                if (_first != null)
                    OnFirstUnSelected.Invoke();
                _first = null;
                return;
            }

            var hit = Physics2D.Raycast(mousePos, Vector2.zero, 1, elements_mask);
            if (hit.collider == null)
            {
                if (_first != null)
                    OnFirstUnSelected.Invoke();
                _first = null;
            }
            else
            {
                if (_first != null)
                {
                    _second = hit.collider.GetComponent<BehaviorController>();
                    OnSwap.Invoke(_first,_second);
                }
                else
                {
                    _first = hit.collider.GetComponent<BehaviorController>();
                    OnFirstSelected.Invoke(_first);
                }
            }
        }
    }
    void Swap(BehaviorController a,BehaviorController b){
        var tempA = a.CurrActiveLogics;
        var tempP = a.CurrPassiveLogics;
        a.SwitchBehavior(b.CurrActiveLogics,b.CurrPassiveLogics);
        b.SwitchBehavior(tempA,tempP);
        _first = null;
        _second = null;
        OnFirstUnSelected.Invoke();
    }
}
