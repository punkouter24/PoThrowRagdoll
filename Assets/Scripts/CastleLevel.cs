using UnityEngine;

public class CastleLevel : Level
{
    public GameObject blockPrefab; // Cube prefab for walls
    public GameObject towerPrefab; // Cylinder prefab for towers
    public GameObject gatePrefab; // Cube or custom prefab for gates

    // Increased dimensions for a more elaborate castle
    public int castleLength = 50;
    public int castleWidth = 50;
    public int wallHeight = 5;
    public int towerHeight = 10;
    public float blockSpacing = 1.4f; // Slightly more than object size to ensure no overlaps

    public override void CreateLevel()
    {
        Vector3 centerPoint = transform.position;
        float offsetX = (castleLength / 2f) * blockSpacing;
        float offsetZ = (castleWidth / 2f) * blockSpacing;
        Vector3 baseCenter = new Vector3(centerPoint.x - offsetX, centerPoint.y, centerPoint.z - offsetZ);

        BuildOuterWalls(baseCenter);
     //   BuildTowers(baseCenter);
      //  CreateMainGate(baseCenter);
    }

    void BuildOuterWalls(Vector3 baseCenter)
    {
        // Walls are constructed with slight spacing to prevent physics engine from reacting to overlaps
        for (int x = 0; x < castleLength; x++)
        {
            for (int z = 0; z < castleWidth; z++)
            {
                if (x == 0 || x == castleLength - 1 || z == 0 || z == castleWidth - 1) // Perimeter
                {
                    for (int y = 0; y < wallHeight; y++)
                    {
                        Instantiate(blockPrefab, baseCenter + new Vector3(x * blockSpacing, y * blockSpacing, z * blockSpacing), Quaternion.identity, this.transform);
                    }
                }
            }
        }
    }

    void BuildTowers(Vector3 baseCenter)
    {
        // Towers have a wider base for stability
        Vector3[] towerBases = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(castleLength - 1, 0, 0) * blockSpacing,
            new Vector3(0, 0, castleWidth - 1) * blockSpacing,
            new Vector3(castleLength - 1, 0, castleWidth - 1) * blockSpacing
        };

        foreach (Vector3 basePos in towerBases)
        {
            Vector3 pos = baseCenter + basePos;
            for (int y = 0; y < towerHeight; y++)
            {
                Instantiate(towerPrefab, pos + new Vector3(0, y * blockSpacing, 0), Quaternion.identity, this.transform);
            }
        }
    }

    void CreateMainGate(Vector3 baseCenter)
    {
        // Gate is created with considerations for stability and proper embedding into the wall
        GameObject gate = Instantiate(gatePrefab, baseCenter + new Vector3(castleLength / 2f * blockSpacing, 0, -blockSpacing), Quaternion.identity, this.transform);
        gate.transform.localScale = new Vector3(5, wallHeight, 1); // Ensure gate is properly scaled and positioned
    }

    public override void DestroyLevel()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
