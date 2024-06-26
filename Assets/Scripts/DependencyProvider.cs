using UnityEngine;

public class DependencyProvider : IDependencyProvider
{
    public T GetDependency<T>(GameObject obj) where T:Component
    {
        return obj.GetComponent<T>();
    }
}