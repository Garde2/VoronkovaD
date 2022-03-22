using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Shield : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private float _durability = 10f;  //����� ��������� � �����

        public void Init(float durability)
        {
            _durability = durability;
            Destroy(gameObject, 10f); 
        }
        public void Hit (float damage)
        {
            _durability -= damage;

            if (_durability <= 0)
                Destroy(gameObject);
        }
        public void Hurt(float _damage)
        {
            print("Ouch: " + _durability);

            _durability -= _damage; ;

            if (_durability <= 0)
            {
                Destroy(gameObject);  //��� ������� ����� Die
            }
        }
    }

}
