using System.Data.Common;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum InputLockState
{
    None,
    Any,
    Left,
    Right
}
public class PlayerMovement : MonoBehaviour
{
    // �÷��̾ ������ ������Ʈ
    // �÷��̾��� �̵� ����

    [SerializeField] private float speed;
    private InputLockState currentState = InputLockState.Any;
    [SerializeField] private Animator headAnimator;
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private Animator boostAnimator;
    [SerializeField] private Animator drillAnimator;

    [Header("�ν�Ʈ ���")]
    public float boostPower = 7f; // �ν�Ʈ ����Ŀ�
    public float maxBoostSpeed = 2f;
    private bool isBoost; // �ν�Ʈ���� Ȯ�� bool

    [Header("���� �ִ�ӵ� ����")]
    public float maxFallSpeed = -5f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // �� ������Ʈ�� Ȱ��ȭ ���� ��, �븮�ڿ� HandleBoostInput �Լ��� ����� (����)
    private void OnEnable()
    {
        Booster.OnBoostInput += HandleBoostInput;
    }

    // �� ������Ʈ�� ��Ȱ��ȭ ���� ��, �븮���� �̺�Ʈ ������ ������.
    private void OnDisable()
    {
        Booster.OnBoostInput -= HandleBoostInput;
    }

    private void HandleBoostInput(bool isPressed)
    {
        isBoost = isPressed;
        boostAnimator.SetBool("isBoost", isPressed);
    }

    private void Update()
    {
        // �ν���, �ִ�ӵ� ����
        if (isBoost && rb.linearVelocity.y < maxBoostSpeed)
        {
            rb.AddForce(new Vector2(0, boostPower) * Time.deltaTime * 100, ForceMode2D.Force);
        }

        // ���� �ӵ� ����
        if(rb.linearVelocityY < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, maxFallSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && currentState != InputLockState.Left)
        {
            currentState = InputLockState.Right;
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
            rb.linearVelocity = new Vector2(- 1 * speed, rb.linearVelocityY);
           
            ChangeAnimation("MoveLeft", bodyAnimator);
            ChangeAnimation("MoveLeft", headAnimator);
            ChangeAnimation("MoveLeft", drillAnimator);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && currentState != InputLockState.Right)
        {
            currentState = InputLockState.Left;
            //transform.Translate(Vector3.right * speed * Time.deltaTime);
            rb.linearVelocity = new Vector2(1 * speed, rb.linearVelocityY);

            ChangeAnimation("MoveRight", bodyAnimator);
            ChangeAnimation("MoveRight", headAnimator);
            ChangeAnimation("MoveRight", drillAnimator);
        }
        else
        {
            currentState = InputLockState.Any;
            rb.linearVelocity = new Vector2(0 , rb.linearVelocityY);

            ChangeAnimation("Idle",bodyAnimator);
            ChangeAnimation("Idle",headAnimator);
            ChangeAnimation("Idle",drillAnimator);
        }


    }

    // �Ѱ���� ��Ÿ�� �ִϸ��̼� �Ķ���� �̸� , �ִϸ����� �־�
    private void ChangeAnimation(string aniName, Animator animator)
    {
        var parameters = animator.parameters;

        foreach (var parameter in parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
        animator.SetBool(aniName, true);

    }
}
