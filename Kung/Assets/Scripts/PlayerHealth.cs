using System;
using System.Collections;
using UnityEngine;

// TODO : ���� �߰� : IsGround ���������� ������������ �������� �ν�Ʈ ���������� �������� �������� ���

// TODO : �ִϸ����� ���� : �´� ���, �״� ���. Trigger�� ����

// TODO : UI�� ���� : HUD�� ������ ü����, ���ڶ� �����ϱ�. UI���� ��ũ��Ʈ �ʿ���
public class PlayerHealth : MonoBehaviour
{
    [Header("ü��, ���")]
    public int air;
    public int hp;
    public int maxair = 100;
    private int maxhp = 100;
    private bool isAirDecrease;
    private bool isHpDecrease;


    private float decreaseAirTime = 3.0f;


    private bool isDead;
    
    
    private bool isInvincible;
    private float invincibleTime = 1.0f;

    [Header("Head�� Body �ִϸ����� ����")]
    public Animator headAnimator;
    public Animator bodyAnimator;

    private void Awake()
    {
        hp = maxhp;
        air = maxair;
    }

    void Update()
    {
        if (isDead) return;

        if(transform.position.y >= -4)
        {
            air = maxair;
        }

        // �ϴ� ���� 0 �Ʒ��ΰ��� ��Ұ� �޵���
        if(transform.position.y < -4 && !isAirDecrease)
        {
            StartCoroutine(airDecrease());
        }

        if(air <= 0 && !isHpDecrease)
        {
            StartCoroutine(hpDecrease());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            air -= 5;
            Debug.Log($"���� ��� :{air}");
        }
    }

    IEnumerator hpDecrease()
    {
        isHpDecrease = true;
        yield return new WaitForSeconds(0.2f);
        hp--;
        Debug.Log($"���� ü�� : {hp}");

        if (hp <= 0)
        {
            Die();
        }
        isHpDecrease = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        hp -= amount;
        headAnimator.SetTrigger("isDamaged");
        bodyAnimator.SetTrigger("isDamaged");
        StartCoroutine(Invincible());
        Debug.Log($"���� ü�� : {hp}");
        if(hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        headAnimator.SetTrigger("isDead");
        bodyAnimator.SetTrigger("isDead");
        Debug.Log("���");
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        Debug.Log("����");
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
    
    IEnumerator airDecrease()
    {
        isAirDecrease = true;
        yield return new WaitForSeconds(decreaseAirTime);
        if(air >= 0)
        {
            air--;
            Debug.Log($"���� ��� : {air}");
        }
        isAirDecrease = false;
    }
}
