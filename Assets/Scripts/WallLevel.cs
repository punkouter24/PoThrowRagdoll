using UnityEngine;

public class WallLevel : Level
{
    public GameObject blockPrefab;
    private int wallWidth = 10;
    private int wallHeight = 10;
    private float blockSpacing = 1.1f;

    //void Start()
    //{
    //    CreateLevel();
    //}

    public override  void CreateLevel()
    {
        // Use this object's position as the center point for the wall
        Vector3 centerPoint = transform.position;

        // Adjust the start position based on the wall dimensions to center the wall around the centerPoint
        Vector3 startPosition = new Vector3(
            centerPoint.x - (wallWidth * blockSpacing) / 2 + (blockSpacing / 2),
            centerPoint.y,
            centerPoint.z
        );

        int numberOfLayers = 10; // New variable to control the number of wall layers
        float layerDepthSpacing = 1.1f; // Spacing between each layer

        for (int layer = 0; layer < numberOfLayers; layer++) // Loop through each layer
        {
            Vector3 layerOffset = new Vector3(0, 0, layer * layerDepthSpacing); // Calculate the offset for the current layer

            for (int y = 0; y < wallHeight; y++)
            {
                for (int x = 0; x < wallWidth; x++)
                {
                    Vector3 blockPosition = startPosition + new Vector3(x * blockSpacing, y * blockSpacing, 0) + layerOffset;
                    GameObject blockInstance = Instantiate(blockPrefab, blockPosition, Quaternion.identity);
                    blockInstance.transform.parent = this.transform; // Parent the block to this container
                }
            }
        }
    }

    public override  void DestroyLevel()
    {
        // Destroy all child objects (the blocks) when resetting or changing levels
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
