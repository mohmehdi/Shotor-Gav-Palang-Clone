using UnityEngine;

[System.Serializable]
public class LazerLogic : IPassiveLogic
{
    public bool Stackable => true;
    [SerializeField] LayerMask lazerHitMask;
    [SerializeField] Direction direction;
    [SerializeField] Lazer lazer;
    [SerializeField] float maxDistance = 50f;
    GameObject _initialOwner;
    Transform _transform;

    public void Disable()
    {
        lazer.gameObject.SetActive(false);
    }

    public void Execute()
    {
        lazer.SetLazerOwner(_transform.gameObject);
        var hit = Physics2D.Raycast(_transform.position, direction.GetDirectionVector(), maxDistance, lazerHitMask);
        if (hit.collider == null)
        {
            lazer.SetLinePoints(_transform.position, direction.GetDirectionVector()*maxDistance);
            return;
        }
        lazer.SetLinePoints(_transform.position, hit.point);
    }

    public void HandleCollision(Collision2D other)
    {

    }

    public void Reset()
    {
        lazer.gameObject.SetActive(true);
        // lazer.SetLazerOwner(_initialOwner);
        Execute();
    }

    public void Setup(IDependencyProvider provider)
    {
        _transform = provider.GetDependency<Transform>();
    }

    public void Start()
    {
        _initialOwner = _transform.gameObject;
    }
}