public interface IActiveLogic
{   
    // can puzzle elements have multiple of this logic
    bool Stackable{get;}
    // roll back any changes to the initial values
    void Reset();
    // get the dependencies for this logic
    void Setup(IDependencyProvider provider);
    // this will be executed every fixedupdate after setup
    void Execute();
    // disable the logic
    void Disable();
}
