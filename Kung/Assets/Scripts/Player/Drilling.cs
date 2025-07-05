using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.Jobs;
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
    [Header("잘 연결해야 함")]
    [SerializeField] private Tilemap _mineralTilemap;
    [Header("미니맵 관련")]
    public Tilemap _miniMapFrontTilemap;   //추가
    [SerializeField] private TextMeshProUGUI _depthText;    //추가
    private int _surfaceY; // 지면높이.추가

    [Header("부셔지는 타일맵 스프라이트 배열")]
    public Sprite[] brokenTileSprites;

    [Header("드릴 성능")]
    [SerializeField] PlayerStats _playerState;
    //public float drillDamage;
    public float drillCoolTime; // 낮을수록 좋음

    public Tilemap _brokenableTilemap;
    public Tilemap _rockTilemap;
    [SerializeField] TileManager tileManager;

    public CurrentDirectionState currentDirectionState = CurrentDirectionState.Down; // 현재 굴착할 방향

    public bool isDrilling = false;

    
    private int _spriteIndex;

    private float[,] _tiles; 


    

    private void Start()
    {
        _brokenableTilemap = tileManager.brokenTileMapInstance;
        _miniMapFrontTilemap = tileManager.frontMiniMapInstance;
        _tiles = tileManager._tiles;
    }

    private void Update()
    {
        Vector3Int currentCell = _brokenableTilemap.WorldToCell(transform.position); //추가
        int depth = Mathf.Max(0, _surfaceY - currentCell.y);    // 지면일 때는 0m.추가
        _depthText.text = depth + "m";
    }

  

    /// <summary>
    /// 드릴 키를 눌렀을 때 동작할 코루틴
    /// </summary>
    /// <returns>드릴 쿨타임만큼 기다림</returns>
    public IEnumerator DrillingRoutine()
    {

        while (true)
        {
            Vector3Int currentPos = _brokenableTilemap.WorldToCell(transform.position);
            Vector3Int rockCurrentPos = _rockTilemap.WorldToCell(transform.position);
            Vector3Int pos = currentPos;
            Vector3Int rockPos = rockCurrentPos;
            switch (currentDirectionState)
            {
                case CurrentDirectionState.Left:
                    pos = new Vector3Int(currentPos.x - 1, currentPos.y);
                    rockPos = new Vector3Int(rockCurrentPos.x - 1, rockCurrentPos.y);
                    break;
                case CurrentDirectionState.Right:
                    pos = new Vector3Int(currentPos.x + 1, currentPos.y);
                    rockPos = new Vector3Int(rockCurrentPos.x + 1, rockCurrentPos.y);
                    break;
                case CurrentDirectionState.Down:
                    pos = new Vector3Int(currentPos.x, currentPos.y - 1);
                    rockPos = new Vector3Int(rockCurrentPos.x, rockCurrentPos.y - 1);
                    break;
            }

            (bool valid, int x, int y) = tileManager.TryCellToIndex(pos);
            if (!_brokenableTilemap.HasTile(pos) && !_rockTilemap.HasTile(rockPos))
            {
                isDrilling = false;

            }
            else if(_rockTilemap.HasTile(rockPos))
            {
                isDrilling = true;
            }
            else
            {
                isDrilling = true;
                
                _tiles[x, y] -= _playerState.drillDamage;
                if (_brokenableTilemap.GetTile(pos) != null)
                {
                    if (_tiles[x, y] <= 0)
                    {
                        _brokenableTilemap.SetTile(pos, null);
                        if (_miniMapFrontTilemap != null && _miniMapFrontTilemap.HasTile(pos))    //추가
                        {
                            _miniMapFrontTilemap.SetTile(pos, null); //추가
                        }
                        isDrilling = false;
                        yield return new WaitForSeconds(drillCoolTime);
                    }
                    else
                    {
                        Tile newTile = ScriptableObject.CreateInstance<Tile>();
                        int index = Mathf.Clamp((int)(_tiles[x, y] / (30 / 7)), 0, brokenTileSprites.Length - 1);
                        newTile.sprite = brokenTileSprites[index];
                        _brokenableTilemap.SetTile(pos, newTile);
                    }
                }

            }
            

                yield return new WaitForSeconds(drillCoolTime);
        }
    }

}
