using System.Collections;
using UnityEngine;

public interface IBossPattern
{
    //������ ������ �� �ִ��� ����
    bool CanExecute(BossController boss, Transform player);

    //���� ���� �ڷ�ƾ
    IEnumerator Execute(BossController boss, Transform player);

    //��Ÿ�� �ð� ���
    float Cooldown { get; }
}

public interface ISpawnPattern : IBossPattern
{
    // ������ ������ ����ϴ� ���ϵ��� ��������Ʈ ������ �������
    void SetSpawnPoint(Transform spawnPoint);
}