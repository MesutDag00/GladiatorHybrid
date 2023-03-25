using System;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManger : MonoBehaviour, IDamagble
{
    public GameObject BloodEfect => Resources.Load<GameObject>("BloodEffect");
    public Slider PlayerSlider;
    
    public int Damage { get; set; }

    private void Start()
    {
        PlayerData.Health = PlayerData.HealthsCharacter[PlayerData.ChracterNumber,
            PlayerPrefs.GetInt($"HealthIndex{PlayerData.ChracterNumber}")];
        PlayerSlider = GameObject.Find("PlayerHealthSlider").GetComponent<Slider>();
        PlayerSlider.maxValue = PlayerData.Health;
        PlayerSlider.value = PlayerData.Health;

        Debug.Log(PlayerData.Speed);
        Debug.Log(PlayerData.Health);
        Debug.Log(PlayerData.SwordAttackRange);
    }

    public void DamageTaken(int _damage)
    {
        PlayerSlider.value = PlayerData.Health -= _damage;
        Debug.Log("Health is Player = " + PlayerData.Health);
        Instantiate(BloodEfect, transform.Find("BloodSpawn").transform.position,
            transform.Find("BloodSpawn").transform.rotation);
        if (PlayerData.Health <= 0)
        {
            GetComponent<Animator>().SetTrigger("Deadth");
            Destroy(this.GetComponent<BoxCollider>());
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<JoystickPlayerExample>());
            Destroy(this.GetComponent<PlayerManger>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerData.DamageActive && other.gameObject.GetComponent<IDamagble>() != null)
        {
            Debug.Log("Hi Guys");
            PlayerData.DamageActive = false;
            other.gameObject.GetComponent<IDamagble>().DamageTaken(PlayerData.SwordAttackRange);
            GameOver.Instance.CheckGameController();
        }

        if (other.gameObject.name == "LifePot")
        {
            PlayerData.HealthAdd(50);
            PlayerSlider.value = PlayerData.Health;
            other.gameObject.transform.position += new Vector3(0, -Mathf.Exp((-180 * Time.deltaTime)), 0);
            Destroy(other.gameObject, 5f);
        }
    }
}