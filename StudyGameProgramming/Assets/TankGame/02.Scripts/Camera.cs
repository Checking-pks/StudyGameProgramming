using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    #region 변수 설정
    public GameObject player;
    public Vector3 cameraPosition;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + cameraPosition;
        }
    }
}
