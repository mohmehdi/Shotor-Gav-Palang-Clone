using UnityEngine;

[System.Serializable]
public class StaticRigidbodyLogic : IPassiveLogic
{
    public bool Stackable => false;
    Rigidbody2D _rb;
    
    public void Disable()
    {
    }

    public void Execute()
    {
        if (_rb != null)
            _rb.bodyType = RigidbodyType2D.Static;
    }

    public void HandleCollision(Collision2D other)
    {
    }

    public void Reset()
    {
        Execute();
    }

    public void Setup(IDependencyProvider provider)
    {
        _rb = provider.GetDependency<Rigidbody2D>();
    }

    public void Start()
    {
    }
}
