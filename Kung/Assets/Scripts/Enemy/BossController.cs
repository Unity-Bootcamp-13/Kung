using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BossController : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Transform spawner;

    [Header("���� ScriptableObject")]
    public List<ScriptableObject> patternSOs;

    List<IBossPattern> patterns;
    public bool isBusy;

    Health health;
    private int _maxhp = 300;

    void Awake()
    {
        health = Health.New(_maxhp, _maxhp);

        // SO����Ʈ���� IBossPattern �������̽��� �����Ѱ͵鸸 patterns�� �ִ´�
        patterns = new List<IBossPattern>();

        foreach (ScriptableObject so in patternSOs)
        {
            if (so is IBossPattern bossPatterns)
            {
                patterns.Add(bossPatterns);
            }
        }
    }

    void Update()
    {
        if (isBusy)
        {
            return;
        }
         if (health.IsDead) return;

        var tempList = new List<IBossPattern>();
        //���ϵ� �� ���� ������ ���ϵ鸸�� ����Ʈ�� ��� �迭�� ��ȯ�Ѵ�
        foreach (var pattern in patterns)
        {
            if (pattern.CanExecute(this, player))
            {
                tempList.Add(pattern);
            }
        }

        IBossPattern[] available = tempList.ToArray();

        if (available.Length == 0) return;

        // ���� ������ ���ϵ� ��, ISpawnPattern�� ������ ���� ��ġ�� �������ش�
        foreach (IBossPattern pattern in available)
        {
            if (pattern is ISpawnPattern spawnPat)
                spawnPat.SetSpawnPoint(spawner);
        }

        // �������� ���� �ϳ� ���� �� ����
        var choice = available[Random.Range(0, available.Length)];
        StartCoroutine(RunPattern(choice));
    }
    IEnumerator RunPattern(IBossPattern pattern)
    {
        isBusy = true;
        yield return StartCoroutine(pattern.Execute(this, player));
        yield return new WaitForSeconds(0.2f);
        isBusy = false;
    }
}