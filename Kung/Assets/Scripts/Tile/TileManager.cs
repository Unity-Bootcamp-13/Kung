using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] Transform par;
    [Header("�̸� ���� Ÿ�ϸ� ����")]
    [SerializeField] private GameObject brokenTileMap;
    [SerializeField] private GameObject backGroundTIleMap;
    [SerializeField] private GameObject frontMiniMapTilemap;
    [SerializeField] private GameObject backMiniMapTilemap;
    [SerializeField] private GameObject mineralTilemap;

    [SerializeField] private Player _player;

    public Tilemap brokenabelTileMap;
    public int width;
    public int height;
    public int offsetX;
    public int offsetY;
    public int spriteIndex;
    public float[,] tiles;
    public Sprite[] brokenTileSprites;


    void Awake()
    {
        Instantiate(backGroundTIleMap, par);
        Instantiate(frontMiniMapTilemap, par);
        Instantiate(backMiniMapTilemap, par);
        Instantiate(mineralTilemap, par);
        brokenabelTileMap = Instantiate(brokenTileMap, par).GetComponent<Tilemap>();
        tileArrayInit();
    }

    private void tileArrayInit()
    {
        BoundsInt bounds = brokenabelTileMap.cellBounds;
        width = bounds.xMax - bounds.xMin;
        height = bounds.yMax - bounds.yMin;
        offsetX = -bounds.xMin;
        offsetY = -bounds.yMin;
        tiles = new float[width, height];
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                tiles[TryCellToIndex(pos).x, TryCellToIndex(pos).y] = 100;
            }
        }
        spriteIndex = 100 / brokenTileSprites.Length;
    }


    /// <summary>
    /// ���� Vector3Int�� ������ ������ �ʵ��� ���������� �����ؼ� �迭���� ����� �ε��� ��ȯ
    /// </summary>
    /// <param name="cellPos"></param>
    /// <returns>�迭 ���� ���� ��ǥ����, 2���� �迭���� ����� x,y</returns>
    public (int x, int y) TryCellToIndex(Vector3Int cellPos)
    {
        int x = cellPos.x + offsetX;
        int y = cellPos.y + offsetY;
        if (x < 0 || y < 0 || x >= width || y >= height)
            return (0, 0);

        return (x, y);
    }

    
    
}
