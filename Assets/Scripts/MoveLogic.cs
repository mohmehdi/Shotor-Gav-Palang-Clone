using System;
using UnityEngine;
public enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3

}
[System.Serializable]
public class MoveLogic : IActiveLogic
{
    public bool Stackable => false;
    [SerializeField] float _speed = 1f;
    [SerializeField] Direction _direction = Direction.Left;

    Rigidbody2D _rb;
    Animator _anim;

    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        if (_rb == null)
        {
            Debug.LogWarning("A Movable Objects requires a RigidBody2D");
            return;
        }

        var movement = _direction.GetDirectionVector();
        _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime);

        if (_anim == null)
            return;

        _anim.SetFloat("horizontal", movement.x);
        _anim.SetFloat("vertical", movement.y);
        _anim.SetFloat("speed", movement.sqrMagnitude);
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void Setup(IDependencyProvider provider)
    {
        _rb = provider.GetDependency<Rigidbody2D>();
        _anim = provider.GetDependency<Animator>();
    }

    public void HandleCollision() => _direction = _direction.GetOppositeDirection();
}
