using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class JoystickPlayerExample : MonoBehaviour
{
    public FixedJoystick variableJoystick;
    public Rigidbody rb;
    private Button Attack => GameObject.Find("Attack").GetComponent<Button>();
    private Button Shield => GameObject.Find("Shield").GetComponent<Button>();

    private Animator _anim => this.GetComponent<Animator>();

    private void Start()
    {
        Shield.onClick.AddListener(ShieldButton);
        Attack.onClick.AddListener(AttackButton);
    }

    public void FixedUpdate()
    {
        MovementController();
        AnimController();
    }

    #region MovementAndAnimationManger

    private void AttackAndShield(bool a)
    {
        Attack.interactable = a;
        Shield.interactable = a;
    }

    private IEnumerator AttackWating()
    {
        yield return new WaitForSeconds(1f);
        PlayerData.DamageActive = true;
        yield return new WaitForSeconds(1f);
        AttackAndShield(true);
        _anim.SetBool("isAttack", false);
    }

    public void AttackButton()
    {
        AttackAndShield(false);
        _anim.SetBool("isAttack", true);
        StartCoroutine(AttackWating());
    }

    public void ShieldButton()
    {
        AttackAndShield(false);
        PlayerData.Save = true;
        _anim.SetBool("isShield", true);
        Invoke(nameof(SaveCharecter), 1.2f);
    }

    private void MovementController()
    {
        if (_anim.GetBool("isAttack") != true && _anim.GetBool("isShield") != true)
            if (variableJoystick.Horizontal != 0 && variableJoystick.Vertical != 0)
            {
                Vector3 direction = Vector3.forward * variableJoystick.Vertical +
                                    Vector3.right * variableJoystick.Horizontal;
                rb.velocity = (direction * PlayerData.Speed * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    15 * Time.fixedDeltaTime);
            }
    }

    private void SaveCharecter()
    {
        AttackAndShield(true);
        PlayerData.Save = false;
    }

    private void AnimController()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            _anim.SetBool("isAttack", false);
            _anim.SetBool("isShield", false);
        }

        if (variableJoystick.Vertical != 0 && variableJoystick.Horizontal != 0) _anim.SetBool("isMovement", true);
        else if (variableJoystick.Vertical == 0 && variableJoystick.Horizontal == 0) _anim.SetBool("isMovement", false);
    }

    #endregion
}