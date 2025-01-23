using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _speed = "Speed";
    private float speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        _movement.Set(Inputmg.Movement.x, Inputmg.Movement.y);

        _rb.linearVelocity = _movement * _moveSpeed;
        speed = Vector3.Magnitude(_rb.linearVelocity);

        if (_movement.x != 0 || _movement.y != 0)
        {
            
            _animator.SetFloat(_horizontal, _movement.x);
            _animator.SetFloat(_vertical, _movement.y);
            _animator.SetFloat(_speed, speed);
        }
        else
        {
            _animator.SetFloat(_speed, 0);
        }


    }




}
