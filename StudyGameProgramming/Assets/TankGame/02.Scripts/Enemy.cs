using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region 변수 설정
    // 탱크 따라가기
    private NavMeshAgent nav;
    private GameObject target;

    private float moveSpeed; //이동속도
    private float minDistance;
    private float distance;

    // 총알 발사
    public GameObject turret;
    public Transform spPoint;
    public GameObject bulletPrefab;
    
    private float fireCoolingTime;
    private float nowFireCoolingTime;

    private float bulletPower;
    private float bulletDamage;
    #endregion

    void Start()
    {
        #region 스텟 설정
        Stats setStats = GetComponent<Stats>();

        // 움직임 관련
        moveSpeed = setStats.moveSpeed;
        minDistance = setStats.followDistance;

        // 공격 관련
        fireCoolingTime = setStats.fireCoolingTime;
        bulletPower = setStats.bulletPower;
        bulletDamage = setStats.bulletDamage;
        #endregion

        #region 기본 셋팅
        nav = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");

        nowFireCoolingTime = fireCoolingTime;
        #endregion
    }

    void Update()
    {
        #region 이동 설정
        transform.LookAt(target.transform.position);    // 타겟 바라보기
        
        // Target과의 거리 구하기
        distance = Vector3.Distance(target.transform.position, transform.position);
        
        if (distance > minDistance) // minDistance보다 멀 때만 따라옴
            nav.destination = target.transform.position;
        else
            nav.destination = transform.position;
        #endregion

        #region 총알 발사
        // 총알 쿨링 타임
        if (nowFireCoolingTime > 0)
            nowFireCoolingTime -= Time.deltaTime;
        
        if (nowFireCoolingTime <= 0)    // 현재 쿨링 타임이 0보다 작을 시 총알 발사
        {
            // Bullet 생성
            GameObject bullet = Instantiate(bulletPrefab, spPoint.position, spPoint.rotation) as GameObject;

            // Bullet 스텟 설정
            Bullet bulletStats = bullet.GetComponent<Bullet>();
            bulletStats.firedObject = tag;
            bulletStats.damage = bulletDamage;

            // Bullet 위치, 파워 설정
            Rigidbody bulletAddforce = bullet.GetComponent<Rigidbody>();
            bulletAddforce.AddForce(turret.transform.forward * bulletPower);

            // 쿨링 타임 초기화
            nowFireCoolingTime = fireCoolingTime;
        }
        #endregion
    }
}
