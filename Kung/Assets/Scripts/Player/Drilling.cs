using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum CurrentDirectionState
{
    None,
    Left,
    Right,
    Down
}
public class Drilling : MonoBehaviour
{
    [Header("ä�� Ÿ�ϸ�")]
    public Tilemap _brokenableTilemap;
    
    [Header("�̴ϸ� ����")]
    [SerializeField] private Tilemap _miniMapFrontTilemap;   //�߰�
    [SerializeField] private TextMeshProUGUI _depthText;    //�߰�
    [Header("�÷��̾� ����")]
    [SerializeField] private PlayerMovement _player;

    private int _surfaceY; // �������.�߰�

    [Header("�μ����� Ÿ�ϸ� ��������Ʈ �迭")]
    public Sprite[] brokenTileSprites;

    [Header("�帱 ����")]
    public float drillDamage;
    public float drillCoolTime; // �������� ����
    private Coroutine _drillCoroutine;


    public CurrentDirectionState currentDirectionState = CurrentDirectionState.Down; // ���� ������ ����

    public bool isDrilling = false;

    private int _width;
    private int _height;
    private int _offsetX;
    private int _offsetY;
    private int _spriteIndex;

    private float[,] _tiles; 

    private void Start()
    {
        tileArrayInit();
    }

    private void Update()
    {
        Vector3Int currentCell = _brokenableTilemap.WorldToCell(transform.position); //�߰�
        int depth = Mathf.Max(0, _surfaceY - currentCell.y);    // ������ ���� 0m.�߰�
        //_depthText.text = depth + "m";
    }

    /// <summary>
    /// Ÿ�ϸ��� Ÿ�� �ϳ��ϳ� �ʱ�ȭ
    /// </summary>
    private void tileArrayInit()
    {
        BoundsInt bounds = _brokenableTilemap.cellBounds;
        _width = bounds.xMax - bounds.xMin;
        _height = bounds.yMax - bounds.yMin;
        _tiles = new float[_width, _height];
        _offsetX = -bounds.xMin;
        _offsetY = -bounds.yMin;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                _tiles[TryCellToIndex(pos).x, TryCellToIndex(pos).y] = 100;
            }
        }
        _spriteIndex = 100 / brokenTileSprites.Length;
    }


    /// <summary>
    /// ���� Vector3Int�� ������ ������ �ʵ��� ���������� �����ؼ� �迭���� ����� �ε��� ��ȯ
    /// </summary>
    /// <param name="cellPos"></param>
    /// <returns>�迭 ���� ���� ��ǥ����, 2���� �迭���� ����� x,y</returns>
    private (bool valid, int x, int y) TryCellToIndex(Vector3Int cellPos)
    {
        int x = cellPos.x + _offsetX;
        int y = cellPos.y + _offsetY;
        if (x < 0 || y < 0 || x >= _width || y >= _height)
            return (false, 0, 0);

        return (true, x, y);
    }

    public void C_StartDrilling(int dir)
    {
        if (_drillCoroutine == null)
            _drillCoroutine = StartCoroutine(DrillingRoutine(dir));
    }
    public void C_StopDrilling(int dir)
    {
        if (_drillCoroutine != null)
        {
            StopCoroutine(_drillCoroutine);
            _drillCoroutine = null;
        }
    }
    /// <summary>
    /// �帱 Ű�� ������ �� ������ �ڷ�ƾ
    /// </summary>
    /// <returns>�帱 ��Ÿ�Ӹ�ŭ ��ٸ�</returns>
    public IEnumerator DrillingRoutine(int dir)
    {

        while (true)
        {
            Vector3Int currentPos = _brokenableTilemap.WorldToCell(transform.position);
            Vector3Int pos = currentPos;
            switch (dir)
            {
                case -1:
                    pos = new Vector3Int(currentPos.x - 1, currentPos.y);
                    break;
                case 1:
                    pos = new Vector3Int(currentPos.x + 1, currentPos.y);

                    break;
                case 0:
                    pos = new Vector3Int(currentPos.x, currentPos.y - 1);
                    break;
            }

            (bool valid, int x, int y) = TryCellToIndex(pos);
            if (!_brokenableTilemap.HasTile(pos))
            {
                isDrilling = false;

            }
            else
            {
                isDrilling = true;

                _tiles[x, y] -= drillDamage;
                if (_brokenableTilemap.GetTile(pos) != null)
                {
                    if (_tiles[x, y] <= 0)
                    {
                        _brokenableTilemap.SetTile(pos, null);
                        if (_miniMapFrontTilemap != null && _miniMapFrontTilemap.HasTile(pos))    //�߰�
                        {
                            _miniMapFrontTilemap.SetTile(pos, null); //�߰�
                        }
                        isDrilling = false;
                        yield return new WaitForSeconds(drillCoolTime);
                    }
                    else
                    {
                        Tile newTile = ScriptableObject.CreateInstance<Tile>();
                        int index = Mathf.Clamp((int)(_tiles[x, y] / _spriteIndex), 0, brokenTileSprites.Length - 1);
                        newTile.sprite = brokenTileSprites[index];
                        _brokenableTilemap.SetTile(pos, newTile);
                    }
                }
                
            }
            

                yield return new WaitForSeconds(drillCoolTime);
        }
    }

}
