using System;
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
    void Reset();

    /// <summary>
    /// get the dependencies for this logic
    /// </summary>
    /// <param name="provider"></param>
    void Setup(IDependencyProvider provider);

    /// <summary>
    /// this will be executed every fixedupdate after setup
    /// </summary>
    void Execute();

    /// <summary>
    /// disable the logic
    /// </summary>
    void Disable();

    /// <summary>
    /// When object collides with something call this
    /// </summary> 
    void HandleCollision(Collision2D other);

}
