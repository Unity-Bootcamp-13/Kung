using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] Transform par;
    [Header("미리 만든 타일맵 연결")]
    [SerializeField] private GameObject brokenTileMap;
    [SerializeField] private GameObject backGroundTIleMap;
    [SerializeField] private GameObject frontMiniMapTilemap;
    [SerializeField] private GameObject backMiniMapTilemap;
    [SerializeField] private Tile mineralTile;

    [SerializeField] private Drilling _drilling;


    public Tilemap brokenTileMapInstance;
    public Tilemap frontMiniMapInstance;

    private int _width;
    private int _height;
    private int _offsetX;
    private int _offsetY;

    public float[,] _tiles;
    public int baseHp;

    void Awake()
    {
        Instantiate(backGroundTIleMap, par);
        Instantiate(backMiniMapTilemap, par);
        brokenTileMapInstance = Instantiate(brokenTileMap, par).GetComponent<Tilemap>();
        frontMiniMapInstance = Instantiate(frontMiniMapTilemap, par).GetComponent<Tilemap>();
        tileArrayInit();

    }


    private void tileArrayInit()
    {
        BoundsInt bounds = brokenTileMapInstance.cellBounds;
        _width = bounds.xMax - bounds.xMin;
        _height = bounds.yMax - bounds.yMin;
        _tiles = new float[_width, _height];
        _offsetX = -bounds.xMin;
        _offsetY = -bounds.yMin;

        float firstThreshold = bounds.yMax - (_height / 3f);      // 위에서 1/3
        float secondThreshold = bounds.yMax - (_height * 2f / 3f); // 위에서 2/3

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (brokenTileMapInstance.HasTile(pos))
                {
                    float brightness = 1f;

                    if (y < firstThreshold && y >= secondThreshold)
                    {
                        brightness = 0.8f; // 중간 깊이: 20% 어둡게
                        _tiles[TryCellToIndex(pos).x, TryCellToIndex(pos).y] = baseHp * 2;
                    }
                    else if (y < secondThreshold)
                    {
                        brightness = 0.6f; // 제일 깊음: 40% 어둡게
                        _tiles[TryCellToIndex(pos).x, TryCellToIndex(pos).y] = baseHp * 3;
                    }
                    else
                    {
                        _tiles[TryCellToIndex(pos).x, TryCellToIndex(pos).y] = baseHp;
                    }

                    brokenTileMapInstance.SetColor(pos, new Color(brightness, brightness, brightness, 1f));
                }
            }
        }
    }



    /// <summary>
    /// 들어온 Vector3Int를 음수가 나오지 않도록 오프셋으로 조절해서 배열에서 사용할 인덱스 반환
    /// </summary>
    /// <param name="cellPos"></param>
    /// <returns>배열 범위 안의 좌표인지, 2차원 배열에서 사용할 x,y</returns>
    public (bool valid, int x, int y) TryCellToIndex(Vector3Int cellPos)
    {
        int x = cellPos.x + _offsetX;
        int y = cellPos.y + _offsetY;
        if (x < 0 || y < 0 || x >= _width || y >= _height)
            return (false, 0, 0);

        return (true, x, y);
    }


}
