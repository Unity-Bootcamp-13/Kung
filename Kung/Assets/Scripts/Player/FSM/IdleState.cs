using UnityEngine;

public class IdleState : IState
{
    //private Animator _headAnimator;
    //private Animator _bodyAnimator;
    //public IdleState(Animator head, Animator body)
    //{
    //    _headAnimator = head;
    //    _bodyAnimator = body;
    //}

    public void Enter()
    {
        //_headAnimator.SetBool("Move", false);
        //_bodyAnimator.SetBool("Move", false);
    }
    public void Update() { /* �Է� ������ �޸���� ���� ���� */ }
    public void Exit() { Debug.Log("Idle ����"); }
}