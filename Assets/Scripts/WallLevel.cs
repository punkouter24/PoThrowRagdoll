using UnityEngine;

public class WallLevel : Level
{
    public GameObject blockPrefab;
    public Material material1;
    public Material material2;
    private readonly int wallWidth = 10;
    private readonly int wallHeight = 10;
    private readonly float blockSpacing = 1.1f;

    public override void CreateLevel()
    {
        Vector3 centerPoint = transform.position;
        Vector3 startPosition = new(
            centerPoint.x - (wallWidth * blockSpacing / 2) + (blockSpacing / 2),
            centerPoint.y,
            centerPoint.z
        );

        int numberOfLayers = 10;
        float layerDepthSpacing = 1.1f;

        for (int layer = 0; layer < numberOfLayers; layer++)
        {
            Vector3 layerOffset = new(0, 0, layer * layerDepthSpacing);

            for (int y = 0; y < wallHeight; y++)
            {
                for (int x = 0; x < wallWidth; x++)
                {
                    Vector3 blockPosition = startPosition + new Vector3(x * blockSpacing, y * blockSpacing, 0) + layerOffset;
                    GameObject blockInstance = Instantiate(blockPrefab, blockPosition, Quaternion.identity);

                    // Apply different materials
                    Renderer renderer = blockInstance.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = (x % 2 == 0) ? material1 : material2;
                    }

                    blockInstance.transform.parent = transform;
                }
            }
        }

        // Add additional walls
        // Add additional walls further away
        float wallOffsetDistance = 20.0f; // Adjust this value as needed
        AddWall(centerPoint + new Vector3(0, 0, wallOffsetDistance));
        AddWall(centerPoint + new Vector3(0, 0, -wallOffsetDistance));
        AddWall(centerPoint + new Vector3(wallOffsetDistance, 0, 0));
        AddWall(centerPoint + new Vector3(-wallOffsetDistance, 0, 0));
    }

    private void AddWall(Vector3 startPosition)
    {
        for (int y = 0; y < wallHeight; y++)
        {
            for (int x = 0; x < wallWidth; x++)
            {
                Vector3 blockPosition = startPosition + new Vector3(x * blockSpacing, y * blockSpacing, 0);
                GameObject blockInstance = Instantiate(blockPrefab, blockPosition, Quaternion.identity);

                // Apply different materials
                Renderer renderer = blockInstance.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = (x % 2 == 0) ? material1 : material2;
                }

                blockInstance.transform.parent = transform;
            }
        }
    }

    public override void DestroyLevel()
    {
        // Destroy all child objects (the blocks) when resetting or changing levels
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
