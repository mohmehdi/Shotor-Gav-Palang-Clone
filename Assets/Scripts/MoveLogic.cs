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
    [SerializeField] float speed = 1f;
    [Range(0, 360)]
    [Tooltip("The angle in which if collision occurs in front of this object it turns around")]
    [SerializeField] float changeDirectionAngle = 60f;
    [SerializeField] Direction direction = Direction.Left;

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

        var movement = direction.GetDirectionVector();
        _rb.MovePosition(_rb.position + movement * speed * Time.fixedDeltaTime);

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

    public void HandleCollision(Collision2D other)
    {
        if (_rb == null) return;
        _rb.velocity = Vector2.zero;
        var collision_direction =other.collider.ClosestPoint(_rb.position) - _rb.position; 
        // Debug.DrawRay(_rb.position,collision_direction,Color.red);
        // Debug.DrawRay( _rb.position,direction.GetDirectionVector(),Color.green);
        // Debug.Log($"{_rb.name} -> {Vector2.Angle(collision_direction,direction.GetDirectionVector())}");
        if (Vector2.Angle(collision_direction,direction.GetDirectionVector()) < changeDirectionAngle)
            direction = direction.GetOppositeDirection();
    }
}
