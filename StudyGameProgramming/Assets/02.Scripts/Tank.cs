using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    #region 변수 설정
    // 터렛 앞 뒤 이동
    public int moveSpeed; //이동속도
    public float move;
    public float moveVertical;

    // 터렛 방향 변환
    public int rotateSpeed;
    public float rotate;
    public float rotateHorizon;

    // 터렛 총구 회전
    public float rotateTurret;
    public GameObject turret;

    // 터렛 총구 상하 회전
    public float keyGun;
    public float upDownSpeed;
    public GameObject gunBase;

    // 총알 
    public int bulletPower;
    public GameObject bulletPrefab;
    public Transform spPoint;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5;
        rotateSpeed = 20;
        upDownSpeed = 7;
        bulletPower = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        // 이동 설정
        move    = moveSpeed * Time.deltaTime;  // 속도 * 시간 == 거리;
        rotate  = rotateSpeed * Time.deltaTime;

        moveVertical    = Input.GetAxis("Vertical");
        rotateHorizon   = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * moveVertical * move);
        transform.Rotate(new Vector3(0.0f, rotate * rotateHorizon, 0.0f));

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

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, spPoint.position, spPoint.rotation) as GameObject;
            Rigidbody bulletAddforce = bullet.GetComponent<Rigidbody>();
            bulletAddforce.AddForce(turret.transform.forward * bulletPower);
        }
    }
}
