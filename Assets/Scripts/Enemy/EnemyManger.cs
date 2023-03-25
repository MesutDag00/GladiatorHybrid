using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManger : MonoBehaviour, IDamagble
{
    public GameObject BloodEfect => Resources.Load<GameObject>("BloodEffect");
    public int Health = 100;
    public int DamageChcaracter;

    public int Damage
    {
        get => DamageChcaracter;
        set => DamageChcaracter = value;
    }

    public Slider EnemySlider => transform.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();
    private bool _attackActive = true;

    private void Start()
    {
        EnemySlider.gameObject.SetActive(false);
        EnemySlider.maxValue = Health;
        EnemySlider.value = Health;
    }


    public void DamageTaken(int _damage)
    {
        Health -= _damage;
        EnemySlider.value = Health;
        GameObject gbj = Instantiate(BloodEfect, transform.Find("BloodSpawn").transform.position,
            transform.Find("BloodSpawn").transform.rotation);
        EnemySlider.gameObject.SetActive(true);
        gbj.transform.SetParent(this.transform);
        if (Health <= 0)
        {
            EnemySlider.gameObject.SetActive(false);
            GetComponent<Animator>().SetTrigger("Deadth");
            Destroy(this.GetComponent<BoxCollider>());
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<EnemyController>());
            Destroy(this.GetComponent<EnemyManger>());
        }
    }

    private void Attack() => _attackActive = true;

    private void OnTriggerStay(Collider other)
    {
        if (_attackActive)
        {
            if (other.gameObject.name == "Player" && !PlayerData.Save)
            {
                _attackActive = false;
                other.gameObject.GetComponent<IDamagble>().DamageTaken(Damage);
                Invoke(nameof(Attack), 2f);
                GameOver.Instance.CheckGameController();
            }
        }
    }
}