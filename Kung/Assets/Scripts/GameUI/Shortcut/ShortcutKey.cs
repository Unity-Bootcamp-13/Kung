using UnityEngine;
using UnityEngine.Tilemaps;

public class ShortcutKey : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject _bigBomb;
    [SerializeField] private GameObject _dynamite;
    [SerializeField] private Tilemap _rockTile;

    // ��ź�̶� ����ź�� ������ ������ġ�� ��ġ (isGround)üũ�ؾ���
    // ���̳�����Ʈ�� ������ ������ġ �ٷ� �Ʒ��� ��ġ (isGround)üũ�ؾ���
    // �Ʒ��� ������ �����ϰ� �� ����ġ�� ��ȯ

    // ���޻��ڴ� �÷��̾� Health.hp �̷��� �����ؼ� 100 ��
    // ��Ҵ� �÷��̾� Air.air �̷��� �����ؼ� 100% ��

    // ��� ��ư���� ���� �ڿ� ���� �ش� �������� �������ִ���. ���� Ȯ�� �ؾ���
    public void OnBombButtonClick()
    {
        if(_groundChecker.IsGrounded == true)
        {
            Instantiate(_bomb, _playerTransform.position, Quaternion.identity);
        }
    }
    public void OnBigBombButtonClick()
    {
        if(_groundChecker.IsGrounded == true)
        {
            Instantiate(_bigBomb, _playerTransform.position, Quaternion.identity);
        }
    }
    public void OnDynamiteButtonClick()
    {
        if (_groundChecker.IsGrounded == true)
        {
            // ���� �÷��̾��� ��ġ ���� �̳׶� Ÿ���� �����;���
            Vector3Int DownCell = _rockTile.WorldToCell(_playerTransform.position + new Vector3(0, -0.1f, 0));
            TileBase tile = _rockTile.GetTile(DownCell);

            if (tile is CustomOreTile customOreTile)
            {
                if (customOreTile.oreType == CustomOreTile.OreType.Rock)
                {
                    Instantiate(_dynamite, _rockTile.CellToWorld(DownCell) + new Vector3(0.15f, 0.15f, 0), Quaternion.identity);
                }
            }
        }
    }
    public void OnFirstAidKitButtonClick()
    {
        _playerHealth.hp = _playerHealth.hp.Heal(_playerHealth.MaxHp);
    }
    public void OnAirCapsuleButtonClick()
    {
        _playerHealth.air = _playerHealth.air.Heal(_playerHealth.MaxAir);
    }
}
