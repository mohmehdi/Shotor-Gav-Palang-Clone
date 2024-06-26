using System.Collections.Generic;
using UnityEngine;
public enum MovementDirection
{
    Up=0,
    Down=1,
    Left=2,
    Right=3
}
public class MoveLogic : IActiveLogic
{

    public bool Stackable => false;
    public float Speed = 1f;

    Rigidbody2D _rb;
    MovementDirection _direction = MovementDirection.Left;
    readonly Dictionary<MovementDirection, Vector2> _directionMapping = new()
    {
        { MovementDirection.Up, Vector2.up },
        { MovementDirection.Down, Vector2.down },
        { MovementDirection.Left, Vector2.left },
        { MovementDirection.Right, Vector2.right }
    };
    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        if (_rb == null) return;
        
        if (_directionMapping.TryGetValue(_direction, out Vector2 movement))
        {
            _rb.MovePosition(_rb.position + movement * Speed * Time.fixedDeltaTime);
        }
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void Setup(IDependencyProvider provider,GameObject obj)
    {
        _rb = provider.GetDependency<Rigidbody2D>(obj);
    }
}
