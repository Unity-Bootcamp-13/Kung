using UnityEngine;

public class PlayerAniController : MonoBehaviour
{
    //���� - ä���ߤ��Ӥ��ᤷ��, �ƴϤ��ܤ�����
    //������ - ä�������̤��ϰܤ�����,�ƴϤ��ܤ�����
    //idle - ä�������̤��ϰܤ�����
    [Header("�ִϸ�����")]
    [SerializeField] private Animator _headAnimator;
    [SerializeField] private Animator _bodyAnimator;
    [SerializeField] private Animator _boostAnimator;
    [SerializeField] private Animator _drillLeft;
    [SerializeField] private Animator _drillRight;

    [Header("��������Ʈ")]
    [SerializeField] private SpriteRenderer _bodySprite;
    [SerializeField] private SpriteRenderer _headSprite;
    [SerializeField] private Sprite[] _drillingSprite;

    public void OnLeftAniPlay(bool isDrilling)
    {
        if (isDrilling)
        {
        }
        else
        {

        }
    }
    
}
