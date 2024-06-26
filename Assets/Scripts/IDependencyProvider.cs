using UnityEngine;

public interface IDependencyProvider{
    T GetDependency<T>() where T:Component;
}
