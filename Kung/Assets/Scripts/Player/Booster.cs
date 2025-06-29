using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    // static���� ������ �̺�Ʈ. �ٸ� Ŭ�������� ���� ������.
    public static event Action<bool> OnBoostInput;

    // �̺�ƮƮ���Ÿ� ��ư�� �޾Ƽ� �����ʹٿ� �����ϸ� PressBoost �Լ� ����. �븮�ڿ� true�� ������.( ���ȴٴ� �ǹ� )
    public void PressBoost() => OnBoostInput?.Invoke(true);
    // �̺�ƮƮ���Ÿ� ��ư�� �޾Ƽ� �����ʹٿ� �����ϸ� ReleaseBoost �Լ� ����. �븮�ڿ� false ������.( �ôٴ� �ǹ� )
    public void ReleaseBoost() => OnBoostInput?.Invoke(false);
}
