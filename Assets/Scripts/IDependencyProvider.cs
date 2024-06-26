using UnityEngine;

public interface IDependencyProvider{
    T GetDependency<T>(GameObject obj) where T:Component;
}
