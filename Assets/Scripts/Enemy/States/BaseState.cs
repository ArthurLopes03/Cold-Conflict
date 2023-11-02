public abstract class BaseState
{
    public Enemy enemy;
    //statemachine class

    public StateMachine stateMachine;

    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();
}