using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSeeCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = CameraManager.Instance.MainCamera.transform.forward.normalized;

        transform.rotation = Quaternion.LookRotation(-vec);
    }
}
