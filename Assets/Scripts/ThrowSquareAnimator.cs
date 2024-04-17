//using UnityEngine;

//public class ThrowSquareAnimator : MonoBehaviour
//{
//    public GameObject ThrowSquare; // Reference to the ThrowSquare GameObject
//    public Transform ragdollRoot; // Reference to the ragdoll's root transform
//    private float xDirection; // Variable to store the x direction

//    private void Update()
//    {
//        // Update xDirection based on the current position of ThrowSquare and the initial position of ragdollRoot
//        float oldXDirection = ThrowSquare.transform.position.x;
//        //  Debug.Log("oldXDirection" + oldXDirection);

//        xDirection = ((oldXDirection - 0) / (1200 - 0) * (1 - (-1))) + (-1);
//        //    Debug.Log("xDirection: " + xDirection);
//    }
//}