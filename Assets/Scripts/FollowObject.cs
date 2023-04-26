using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform followThis;
    private void LateUpdate()
    {
        transform.position = followThis.position + new Vector3(0, 0, -10);
    }
}
