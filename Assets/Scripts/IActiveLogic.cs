using UnityEngine;

public interface IActiveLogic
{   
    /// <summary>
    /// can puzzle elements have multiple of this logic
    /// </summary>
    bool Stackable{get;}
    /// <summary>
    /// roll back any changes to the initial values
    /// </summary>
    public void Reset();
    /// <summary>
    /// get the dependencies for this logic
    /// </summary>
    /// <param name="provider"></param>
    public void Setup(IDependencyProvider provider);
    /// <summary>
    /// this will be executed every fixedupdate after setup
    /// </summary>
    public void Execute();
    /// <summary>
    /// disable the logic
    /// </summary>
    public void Disable();
}
