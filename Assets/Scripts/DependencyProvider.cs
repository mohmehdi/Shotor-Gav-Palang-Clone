using UnityEngine;

public class DependencyProvider : IDependencyProvider
{
    GameObject _obj;
    public DependencyProvider(GameObject obj)=> this._obj = obj;
    public T GetDependency<T>() where T:Component
    {
        return _obj.GetComponent<T>();
    }
}