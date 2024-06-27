using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionEffect : MonoBehaviour
{
    Transform _target;
    Transform _transform;
    private void Awake() => _transform = transform;
    public void SetTarget(BehaviorController target) => _target = target.transform;
    private void Update() {
        if (_target == null)return;
        _transform.position = _target.position;
    }
}
