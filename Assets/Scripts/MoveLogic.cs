using System;
using UnityEngine;

[System.Serializable]
public class MoveLogic : IActiveLogic, IFreezeEffect
{
    public bool Stackable => false;
    [SerializeField] float speed = 1f;
    [Range(0, 360)]
    [Tooltip("The angle in which if collision occurs in front of this object it turns around")]
    [SerializeField] float changeDirectionAngle = 60f;
    [SerializeField] Direction direction = Direction.Left;
    [SerializeField] AnimatedDirectionalSprite arrows;

    Direction _initialDir;
    Rigidbody2D _rb;
    Animator _anim;

    public void Disable() { }

    public void Execute()
    {
        if (_rb == null)
        {
            Debug.LogWarning("A Movable Objects requires a RigidBody2D");
            return;
        }
        _rb.bodyType = RigidbodyType2D.Dynamic;
        var movement = direction.GetDirectionVector();
        _rb.MovePosition(_rb.position + movement * speed * Time.fixedDeltaTime);

        if (_anim == null)
            return;

        _anim.SetFloat("horizontal", movement.x);
        _anim.SetFloat("vertical", movement.y);
        _anim.SetFloat("speed", movement.sqrMagnitude);
    }

    public void Start()
    {
        _initialDir = direction;
    }

    public void Reset()
    {
        direction = _initialDir;
    }

    public void Setup(IDependencyProvider provider)
    {
        if (_anim != null)
            _anim.SetFloat("speed", 0);
        _rb = provider.GetDependency<Rigidbody2D>();
        _anim = provider.GetDependency<Animator>();
        if (arrows != null)
            arrows.transform.position = _rb.position;
    }

    public void HandleCollision(Collision2D other)
    {
        if (_rb == null) return;
        _rb.velocity = Vector2.zero;
        var collision_direction = other.collider.ClosestPoint(_rb.position) - _rb.position;
        // Debug.DrawRay(_rb.position,collision_direction,Color.red);
        // Debug.DrawRay( _rb.position,direction.GetDirectionVector(),Color.green);
        // Debug.Log($"{_rb.name} -> {Vector2.Angle(collision_direction,direction.GetDirectionVector())}");
        if (Vector2.Angle(collision_direction, direction.GetDirectionVector()) < changeDirectionAngle)
            direction = direction.GetOppositeDirection();
    }

    public void OnFreeze()
    {
        arrows.transform.position = _rb.position;
        arrows.SetAnimation(direction);
    }

    public void OnDeFreeze()
    {
        arrows.ClearAnimation();
    }
}
