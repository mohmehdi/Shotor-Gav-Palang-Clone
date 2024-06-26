using UnityEngine;

public interface IPassiveLogic {
    /// <summary>
    /// can puzzle elements have multiple of this logic
    /// </summary>
    bool Stackable{get;}
    /// <summary>
    /// roll back any changes to the initial values
    /// </summary>
    void Reset();
    /// <summary>
    /// get the dependencies for this logic
    /// </summary>
    /// <param name="provider"></param>
    void Setup(IDependencyProvider provider,GameObject obj);
    /// <summary>
    /// this should be executed once after setup
    /// </summary>
    void Execute();
    /// <summary>
    /// disable the logic
    /// </summary>
    void Disable();
 }
