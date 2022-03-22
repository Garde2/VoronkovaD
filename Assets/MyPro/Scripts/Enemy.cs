using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProjectL
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _health;
        [SerializeField] private float _speedRotate = 200f;

        [SerializeField] private bool _isFire;
        [SerializeField] private UnityEvent _event;
        void Start()
        {
            _player = FindObjectOfType<Player>();

            //var q = new Quaternion(1, 1, 1, 1);               // x, y, z, w
        }

        private void Update()   //��� ��� ���� ���� �������
        {

            Ray ray = new Ray(_spawnPosition.position, transform.forward);
            //Debug.DrawRay(_spawnPosition.position, transform.forward * 6, Color.blue);
            if (Physics.Raycast(ray, out RaycastHit hit, 3))
            {
                Debug.DrawRay(_spawnPosition.position, transform.forward * hit.distance, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.magenta);

                if (hit.collider.CompareTag("Player"))
                {
                    if (_isFire)
                        Fire();
                }
            }
            /**
            if (Vector3.Distance(transform.position, _player.transform.position) < 3)
            {
                //if (Input.GetMouseButtonDown(1))  //��-����� ��� ���� �� ������ ���������
                    Fire();
                    //_isFire = true;
                    
            }
            **/
        }

        private void FixedUpdate()
        {
            var direction =
                    _player.transform.position - transform.position;

            var pr = Vector3.Dot(
                transform.forward,
                direction);
            var abs = Mathf.Abs(pr);
            var rad = Mathf.Sin(abs);
            var deg = rad * Mathf.Rad2Deg;

            if (deg <= 45f)
            {
                Debug.Log("I see you");
            }

            direction.Set(direction.x, 0, direction.z);
            var stepRotate = Vector3.RotateTowards(transform.forward,
                    direction,
                    _speedRotate * Time.fixedDeltaTime,
                    0f);

            transform.rotation = Quaternion.LookRotation(stepRotate);
        }
        public void Hurt(float _damage)
        {
            print("Ouch: " + _damage);

            _health -= _damage; ;

            if (_health <= 0)
            {
                Destroy(gameObject);  //��� ������� ����� Die
            }
        }

        private void Fire()
        {
            _isFire = false;
            var shieldObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var shield = shieldObj.GetComponent<Bullet>();
            shield.Init(_player.transform, 10, 0.6f);
            _event?.Invoke();
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _isFire = true;
        }
    }
}
