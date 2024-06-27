using UnityEngine;

[System.Serializable]
public class TeleportObjectLogic : IPassiveLogic
{
    public bool Stackable => false;
    [SerializeField] Transform light;

    Transform _currOwner;
    Transform _initialOwner;
    
    public void Disable()
    {
    }

    public void Execute()
    {
        if(light == null){
            Debug.Log("Add a Child for TeleportObjectLogic");
            return;
        }
        if(_currOwner == null){
            Debug.Log("TeleportObjectLogic needs a Transfrom");
            return;
        }
        light.position = _currOwner.position;
    }

    public void HandleCollision(Collision2D other){}

    public void Reset()
    {
        Execute();
    }

    public void Setup(IDependencyProvider provider)
    {
        _currOwner = provider.GetDependency<Transform>();
    }

    public void Start()
    {
        light.SetParent(null);
        _initialOwner = _currOwner;
    }
}