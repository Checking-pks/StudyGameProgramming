using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    #region 변수 설정
    [Header("- 공용")]
    public  float maxHealth; // 최대 체력
    public  float moveSpeed;    // 이동 속도

    public  float fireCoolingTime;  // 포 발사 쿨링 타임
    public  float bulletPower;  // 총알 속도
    public  float bulletDamage; // 총알 데미지

    [Header("- Player 한정")]
    public  float rotateSpeed;  // 회전 속도
    public  float upDownSpeed;  // 포 상하 속도

    [Header("- Enemy 한정")]
    public  float followDistance; // 따라오는 최소 거리
    public int killScore;
    #endregion
}
