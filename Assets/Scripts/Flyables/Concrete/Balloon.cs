using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class Balloon : MonoBehaviour, IFlyable
    {        
        [SerializeField] private FlyableConfig _config;        
        [SerializeField] private TextMeshProUGUI _textLetter;
        [SerializeField] private SpriteRenderer _spriteRenderer;        

        private float _speed = 2.2f;        
        private bool _isFlying = false;
        private Vector3 _targetDirection;
        private LetterEntry _entry;

        public void Init(LetterEntry entry)
        {
            _entry = entry;            
            _textLetter.SetText(entry.value);
            _spriteRenderer.sprite = _config.sprite;            
        }

        public void Fly()
        {
            _isFlying = true;
            _targetDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1f, 0f);
            _speed = Random.Range(_config.minSpeed, _config.maxSpeed);
        }

        private void Update()
        {
            if (_isFlying)
            {                
                transform.Translate(_targetDirection * _speed * Time.deltaTime);

                // Destroy if out of bounds.
                if (transform.position.y > Camera.main.orthographicSize * 2)
                {
                    _isFlying = false;
                    //GameEvents.OnDestroyItem.Execute(this.gameObject);
                    Destroy(gameObject);
                }
            }
        }

        public void Pop()
        {
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => {
                Destroy(gameObject, 1f);
            });

            if (_config.popEffect != null)
            {
                Instantiate(_config.popEffect, transform.position, Quaternion.identity);
            }
        }
    }
}