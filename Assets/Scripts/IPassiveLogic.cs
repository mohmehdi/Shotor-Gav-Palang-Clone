using UnityEngine;

public interface IPassiveLogic
{
    /// <summary>
    /// can puzzle elements have multiple of this logic
    /// </summary>
    bool Stackable { get; }

    /// <summary>
    /// one time initializations similar to Awake and Start
    /// </summary>
    void Start();

    /// <summary>
    /// one time initializations
    /// </summary>
    void Reset();

    /// <summary>
    /// get the dependencies for this logic
    /// </summary>
    /// <param name="provider"></param>
    void Setup(IDependencyProvider provider);

    /// <summary>
    /// this should be executed once after setup every swap
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
