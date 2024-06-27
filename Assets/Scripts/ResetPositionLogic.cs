using UnityEngine;
[System.Serializable]
public class ResetPositionLogic : IPassiveLogic
{
    public bool Stackable => false;

    Transform _targetTransform=null;
    Vector2 _initialPos;
    public void Disable() { }

    public void Execute() {/*Since this is a reset is should not trigger on Swaps*/}

    public void HandleCollision(Collision2D other) { }

    public void Reset()
    {
        _targetTransform.position = _initialPos;
    }

    public void Setup(IDependencyProvider provider)
    {
        if (_targetTransform == null)
            _targetTransform = provider.GetDependency<Transform>();
    }

    public void Start()
    {
        _initialPos = _targetTransform.position;
    }
}