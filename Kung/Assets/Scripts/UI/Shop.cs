using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Transform enterShopTarget;
    public Transform quitShopTarget;

    public Transform enterInventoryTarget;
    public Transform quitInventoryTarget;

    private float _UIspeed = 0.5f;

    public float delayBeforeShopOpen = 0.5f;

    [SerializeField] private GameObject ShopUI;
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private PlayerMovement playerMovement;


    private Coroutine _shopOpenCoroutine; // �ڷ�ƾ ������ ������ ����
    private bool _isPlayerInsideTrigger = false; // �÷��̾ Ʈ���� �ȿ� �ִ��� Ȯ���ϴ� �÷���

    public void OnShopQuitButton()
    {

        // ���� ���� �� ���� ���� �ڷ�ƾ�� �ִٸ� �����մϴ�.
        if (_shopOpenCoroutine != null)
        {
            StopCoroutine(_shopOpenCoroutine);
            _shopOpenCoroutine = null;
        }


        ShopUI.transform.DOLocalMove(quitShopTarget.localPosition, _UIspeed);
        InventoryUI.transform.DOLocalMove(quitInventoryTarget.localPosition, _UIspeed);

        playerMovement.IsMovementLocked = false;
        _isPlayerInsideTrigger = false; // �÷��̾ Ʈ���Ÿ� ������Ƿ� �÷��� �ʱ�ȭ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // �̹� �ڷ�ƾ�� ���� ���̰ų� �÷��̾ �̹� Ʈ���� �ȿ� �ִٸ� �ٽ� �������� �ʽ��ϴ�.
            if (_shopOpenCoroutine == null && !_isPlayerInsideTrigger)
            {
                _isPlayerInsideTrigger = true; // �÷��̾ Ʈ���� �ȿ� �������� ǥ��
                _shopOpenCoroutine = StartCoroutine(OpenShopWithDelay());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾ Ʈ���ſ��� ������ ��, ���� ���� �ڷ�ƾ�� �����մϴ�.
            // �̷��� �ϸ� �÷��̾ ���� ���ο��� ��� �ӹ��� ������ ������ ������ �ʽ��ϴ�.
            if (_shopOpenCoroutine != null)
            {
                StopCoroutine(_shopOpenCoroutine);
                _shopOpenCoroutine = null;
            }
            _isPlayerInsideTrigger = false; // �÷��̾ Ʈ���Ÿ� ������� ǥ��
            // ���� ���� UI�� �̹� �����־��ٸ�, ���⿡�� �ݴ� ������ �߰��� ���� �ֽ��ϴ�.
            // ������ ����� OnShopQuitButton()�� ���ؼ��� �����Ƿ�, ���⼭�� �ܼ��� �ڷ�ƾ �ߴܸ� �մϴ�.
        }
    }



    private IEnumerator OpenShopWithDelay()
    {
        // ������ �ð���ŭ ����մϴ�.
        yield return new WaitForSeconds(delayBeforeShopOpen);

        // ��� �Ŀ��� �÷��̾ ������ Ʈ���� �ȿ� �ִٸ� ������ ���ϴ�.
        // �̷��� �ٽ� Ȯ���ϴ� ������ �÷��̾ ���� �ð� ���� Ʈ���� ������ ���� ���� �ֱ� �����Դϴ�.
        if (_isPlayerInsideTrigger)
        {
            ShopUI.transform.DOLocalMove(enterShopTarget.localPosition, _UIspeed);
            InventoryUI.transform.DOLocalMove(enterInventoryTarget.localPosition, _UIspeed);

            playerMovement.IsMovementLocked = true;
        }
        _shopOpenCoroutine = null; // �ڷ�ƾ�� �Ϸ�Ǿ����Ƿ� ������ null�� ���� 
    }
}




