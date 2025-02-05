using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private LettersConfig _lettersConfig;
        
        [Header("---- Factories ----")]
        [SerializeField] private FlyableFactoryRegistry _factoryRegistry;        
        
        private IEnumerator _routineStartSpawning = null;
        
        [SerializeField] private Transform _container;
        [SerializeField] private float _spawnFrequency = 0.5f;

        private void Start()
        {
            _factoryRegistry.Init();
            StartSpawning();
        }

        public void StartSpawning()
        {                        
            if (_routineStartSpawning != null)
            {
                StopCoroutine(_routineStartSpawning);
            }
            _routineStartSpawning = StartSpawningBalloons();
            StartCoroutine(_routineStartSpawning);
        }

        public void StopSpawning()
        {            
            if (_routineStartSpawning != null)
            {
                StopCoroutine(_routineStartSpawning);
            }
        }

        private IEnumerator StartSpawningBalloons()
        {
            var allLetters = _lettersConfig.lettersMap;
                                    
            while (true)
            {                
                var letterEntry = allLetters[Random.Range(0, allLetters.Count)];

                IFlyable flyable = _factoryRegistry.CreateRandomFlyable();
                flyable.Init(letterEntry);
                flyable.Fly();
                
                yield return new WaitForSeconds(_spawnFrequency); // Adjust spawn rate as needed.
            }

        }

    }
}