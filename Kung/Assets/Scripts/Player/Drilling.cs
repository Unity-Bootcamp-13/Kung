using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("�� �����ؾ� ��")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Tilemap brokenableTilemap;
    [SerializeField] private Tilemap mineralTilemap;

    [Header("�μ����� Ÿ�ϸ� ��������Ʈ �迭")]
    public Sprite[] brokenTileSprites;

    [Header("�帱 ����")]
    public float drillDamage;
    public float drillCoolTime; // �������� ����

    public CurrentDirectionState currentDirectionState = CurrentDirectionState.Down; // ���� ������ ����

    public bool isDrilling = false;

    private int _width;
    private int _height;
    private int _offsetX;
    private int _offsetY;
    private int _spriteIndex;

    private float[,] tiles; 


    

    private void Start()
    {
        tileArrayInit();
    }


    /// <summary>
    /// Ÿ�ϸ��� Ÿ�� �ϳ��ϳ� �ʱ�ȭ
    /// </summary>
    private void tileArrayInit()
    {
        BoundsInt bounds = brokenableTilemap.cellBounds;
        _width = bounds.xMax - bounds.xMin;
        _height = bounds.yMax - bounds.yMin;
        tiles = new float[_width, _height];
        _offsetX = -bounds.xMin;
        _offsetY = -bounds.yMin;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                tiles[TryCellToIndex(pos).x, TryCellToIndex(pos).y] = 100;
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


    /// <summary>
    /// �帱 Ű�� ������ �� ������ �ڷ�ƾ
    /// </summary>
    /// <returns>�帱 ��Ÿ�Ӹ�ŭ ��ٸ�</returns>
    public IEnumerator DrillingRoutine()
    {

        while (true)
        {
            Vector3Int currentPos = brokenableTilemap.WorldToCell(transform.position);
            Vector3Int pos = currentPos;
            switch (currentDirectionState)
            {
                case CurrentDirectionState.Left:
                    pos = new Vector3Int(currentPos.x - 1, currentPos.y);
                    break;
                case CurrentDirectionState.Right:
                    pos = new Vector3Int(currentPos.x + 1, currentPos.y);

                    break;
                case CurrentDirectionState.Down:
                    pos = new Vector3Int(currentPos.x, currentPos.y - 1);
                    break;
            }

            (bool valid, int x, int y) = TryCellToIndex(pos);
            if (brokenableTilemap.GetTile(pos) == null)
            {
                isDrilling = false;

            }
            else if (valid)
            {
                isDrilling = true;

                tiles[x, y] -= drillDamage;
                if (brokenableTilemap.GetTile(pos) != null)
                {
                    if (tiles[x, y] <= 0)
                    {
                        brokenableTilemap.SetTile(pos, null);
                    }
                    else
                    {
                        Tile newTile = ScriptableObject.CreateInstance<Tile>();
                        int index = Mathf.Clamp((int)(tiles[x, y] / _spriteIndex), 0, brokenTileSprites.Length - 1);
                        newTile.sprite = brokenTileSprites[index];
                        brokenableTilemap.SetTile(pos, newTile);
                    }
                }
                
            }
            

                yield return new WaitForSeconds(drillCoolTime);
        }
    }

}
