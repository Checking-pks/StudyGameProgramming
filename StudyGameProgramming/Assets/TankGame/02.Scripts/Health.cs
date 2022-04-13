using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float maxHealth;    //최대 체력
    private float health;       // 현재 체력

    private int killScore;  // 킬 점수

    public  GameObject healthBar;    // 체력바 UI
    private GameObject player;   // 플레이어

    void Start()
    {
        #region 스텟 설정
        Stats setStats = GetComponent<Stats>();

        maxHealth = setStats.maxHealth;
        killScore = setStats.killScore;
        #endregion

        #region 기본 셋팅
        health = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        #endregion
    }

    void Update()
    {
        #region UI 설정
        if (gameObject.tag != "Neutrality")
            healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
        #endregion

        #region 사망 처리
        if (health <= 0)
        {
            player.GetComponent<Player>().getScore(killScore);
            Destroy(gameObject);
        }
        #endregion
    }

    #region 체력 변경 함수
    public void getHealth(float h)
    {
        health += h;
    }
    #endregion
}
