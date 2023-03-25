using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject _baseCharecter => GameObject.Find("Player");
    [CanBeNull] private Animator _animator => GetComponent<Animator>();

    private void Update()
    {
        if (_baseCharecter.GetComponent<PlayerManger>() != null)
        {
            Vector3 Direction = (this.transform.position - _baseCharecter.transform.Find("Face").transform.position) *
                                -1;
            Quaternion rotation = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, 1);

            if (Vector3.Distance(this.transform.position, _baseCharecter.transform.position) > 2f)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, _baseCharecter.transform.position,
                    0.2f * Time.deltaTime);
                _animator.SetBool("Attack", false);
            }
            else
                _animator.SetBool("Attack", true);
        }
        else
            _animator.SetTrigger("Idle");
    }
}