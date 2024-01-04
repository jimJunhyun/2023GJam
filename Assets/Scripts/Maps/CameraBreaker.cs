using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBreaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Camera script

    void LateUpdate()
    {
        // Player는 싱글톤이기에 전역적으로 접근할 수 있습니다.
        Vector3 direction = transform.forward;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
                            1 << LayerMask.NameToLayer("Building"));

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
