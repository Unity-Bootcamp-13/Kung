using Unity.VisualScripting;
public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class StateMachine
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();      // ���� ���� ������
        currentState = newState;
        currentState?.Enter();     // �� ���� ����
    }

    public void Update()
    {
        currentState?.Update();    // ���� ���� ���� �� ó��
    }
}
