using UnityEngine;
using System.Collections; // Added for coroutine support

public class PyramidLevel : Level
{
    public GameObject blockPrefab; // Assign this in the Inspector
    public int pyramidBaseSize = 10; // How wide the base of the pyramid should be
    public float blockSpacing = 1.5f; // Adjusted spacing to reduce block movement

    public override void CreateLevel()
    {
        Debug.Log("Creating Pyramid Level");

        // Calculate the center point of the base
        Vector3 centerPoint = transform.position;
        float offsetX = (pyramidBaseSize / 2f) * blockSpacing;
        float offsetZ = (pyramidBaseSize / 2f) * blockSpacing;
        Vector3 baseCenter = new Vector3(centerPoint.x - offsetX, centerPoint.y, centerPoint.z - offsetZ);

        // Generate pyramid layers
        for (int y = 0; y < pyramidBaseSize; y++) // Height of the pyramid
        {
            // Calculate the number of blocks on this level of the pyramid
            int levelSize = pyramidBaseSize - y;
            for (int x = 0; x < levelSize; x++)
            {
                for (int z = 0; z < levelSize; z++)
                {
                    // Calculate the position for each block
                    Vector3 position = baseCenter +
                                       new Vector3(x * blockSpacing, y * blockSpacing, z * blockSpacing);
                    // Instantiate the block at the calculated position
                    GameObject blockInstance = Instantiate(blockPrefab, position, Quaternion.identity);
                    Rigidbody blockRb = blockInstance.GetComponent<Rigidbody>();
                    blockRb.useGravity = false;
                    blockRb.isKinematic = true;
                    blockInstance.transform.SetParent(this.transform, true);

                                     // Mirror blocks across the X axis, skip if x is 0 to avoid overlap
                    if (x != 0)
                    {
                        Vector3 mirroredXPosition = baseCenter +
                                                  new Vector3(-x * blockSpacing, y * blockSpacing, z * blockSpacing);
                        GameObject mirroredBlockInstance = Instantiate(blockPrefab, mirroredXPosition, Quaternion.identity, this.transform);
                        Rigidbody mirroredBlockRb = mirroredBlockInstance.GetComponent<Rigidbody>();
                        mirroredBlockRb.useGravity = false;
                        mirroredBlockRb.isKinematic = true;
                    }

                    // Mirror blocks across the Z axis, skip if z is 0 to avoid overlap
                    if (z != 0)
                    {
                        Vector3 mirroredZPosition = baseCenter +
                                                  new Vector3(x * blockSpacing, y * blockSpacing, -z * blockSpacing);
                        GameObject mirroredZBlockInstance = Instantiate(blockPrefab, mirroredZPosition, Quaternion.identity, this.transform);
                        Rigidbody mirroredZBlockRb = mirroredZBlockInstance.GetComponent<Rigidbody>();
                        mirroredZBlockRb.useGravity = false;
                        mirroredZBlockRb.isKinematic = true;
                    }

                    // Mirror blocks across both X and Z axes, only if both x and z are not 0
                    if (x != 0 && z != 0)
                    {
                        Vector3 mirroredXZPosition = baseCenter +
                                                   new Vector3(-x * blockSpacing, y * blockSpacing, -z * blockSpacing);
                        GameObject mirroredXZBlockInstance = Instantiate(blockPrefab, mirroredXZPosition, Quaternion.identity, this.transform);
                        Rigidbody mirroredXZBlockRb = mirroredXZBlockInstance.GetComponent<Rigidbody>();
                        mirroredXZBlockRb.useGravity = false;
                        mirroredXZBlockRb.isKinematic = true;
                    }
                }
            }
        }

        ActivateBlockPhysics();  // Activate physics for all blocks
    }

    public void ActivateBlockPhysics()
    {
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }

    public override void DestroyLevel()
    {
        Debug.Log("Destroying Pyramid Level");

        // Destroy all children (the blocks that make up the pyramid)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

