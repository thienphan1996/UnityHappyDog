using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGunting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float gunSpeed = 2f;

    MonsterMoment monsterMoment;

    bool isFiring = false;

    void Start()
    {
        monsterMoment = GetComponent<MonsterMoment>();
    }

    void Update()
    {
        if (monsterMoment.IsDied || isFiring) { return; }

        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        isFiring = true;

        yield return new WaitForSecondsRealtime(gunSpeed);

        var bulletObject = Instantiate(bullet, gun.position, transform.rotation);
        var monsterBullet = bulletObject.GetComponent<MonsterBullet>();

        if (monsterBullet != null)
        {
            monsterBullet.SetXScaleType(-transform.localScale.x);
        }

        isFiring = false;
    }
}
