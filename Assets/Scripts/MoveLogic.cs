using System.Collections.Generic;
using UnityEngine;
public enum MovementDirection
{
    Up=0,
    Down=1,
    Left=2,
    Right=3
}
[System.Serializable]
public class MoveLogic : IActiveLogic
{
    public bool Stackable => false;
    [SerializeField] float _speed = 1f;
    [SerializeField] MovementDirection _direction = MovementDirection.Left;

    Rigidbody2D _rb;
    readonly Dictionary<MovementDirection, Vector2> _directionMapping = new()
    {
        { MovementDirection.Up, Vector2.up },
        { MovementDirection.Down, Vector2.down },
        { MovementDirection.Left, Vector2.left },
        { MovementDirection.Right, Vector2.right }
    };
    readonly Dictionary<MovementDirection, MovementDirection> _oppositeDirectionMapping = new()
    {
        { MovementDirection.Up, MovementDirection.Down },
        { MovementDirection.Down, MovementDirection.Up },
        { MovementDirection.Left, MovementDirection.Right },
        { MovementDirection.Right, MovementDirection.Left }
    };

    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        if (_rb == null){
            Debug.LogWarning("A Movable Objects requires a RigidBody2D");
            return;
        }
        
        if (_directionMapping.TryGetValue(_direction, out Vector2 movement))
        {
            _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime);
        }
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void Setup(IDependencyProvider provider)
    {
        _rb = provider.GetDependency<Rigidbody2D>();
    }

    public void HandleCollision()
    {
        _direction = _oppositeDirectionMapping[_direction];
    }
}
