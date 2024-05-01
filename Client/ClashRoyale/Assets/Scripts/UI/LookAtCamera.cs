using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        Quaternion lookRotation = Camera.main.transform.rotation;
        transform.rotation = lookRotation;
    }
}
