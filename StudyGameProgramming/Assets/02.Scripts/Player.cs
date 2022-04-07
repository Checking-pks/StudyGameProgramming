using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 변수 설정
    // 탱크 앞 뒤 이동
    private float moveSpeed; //이동속도
    private float move;
    private float moveVertical;

    // 탱크 방향 변환
    private float rotateSpeed;
    private float rotate;
    private float rotateHorizon;

    // 터렛 총구 회전
    private float rotateTurret;
    public  GameObject turret;

    // 터렛 총구 상하 회전
    private float keyGun;
    private float upDownSpeed;
    public  GameObject gunBase;

    // 총알 
    private float fireCoolingTime;
    private float nowFireCoolingTime;
    private float bulletPower;
    private float bulletDamage;
    public  GameObject bulletPrefab;
    public  Transform spPoint;
    #endregion

    void Start()
    {
        #region 스텟 설정
        Stats setStats = GetComponent<Stats>();

        // 움직임 관련
        moveSpeed   = setStats.moveSpeed;
        rotateSpeed = setStats.rotateSpeed;
        upDownSpeed = setStats.upDownSpeed;

        // 공격 관련
        fireCoolingTime = setStats.fireCoolingTime;
        bulletPower     = setStats.bulletPower;
        bulletDamage    = setStats.bulletDamage;
        #endregion

        #region 기본 셋팅
        nowFireCoolingTime = 1f;
        #endregion
    }

    void Update()
    {
        #region 이동 설정
        // 앞뒤 이동
        move    = moveSpeed * Time.deltaTime;  // 속도 * 시간 == 거리;
        moveVertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * moveVertical * move);

        // 좌우 이동
        rotate  = rotateSpeed * Time.deltaTime;
        rotateHorizon   = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0.0f, rotate * rotateHorizon, 0.0f));
        #endregion

        #region 총구 움직임 설정
        // 터렛 총구 회전
        rotateTurret = Input.GetAxis("Window Shake X");
        keyGun = Input.GetAxis("Mouse ScrollWheel");

        turret.transform.Rotate(Vector3.up * rotateTurret * rotate);
        gunBase.transform.Rotate(Vector3.right * keyGun * upDownSpeed);

        Vector3 ang = gunBase.transform.eulerAngles;
        if (ang.x > 180)
            ang.x -= 360;
        ang.x = Mathf.Clamp(ang.x, -45, 0);
        gunBase.transform.eulerAngles = ang;
        #endregion

        #region 총알 발사
        if (nowFireCoolingTime > 0)
            nowFireCoolingTime -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (nowFireCoolingTime <= 0)
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
        }
        #endregion
    }
}