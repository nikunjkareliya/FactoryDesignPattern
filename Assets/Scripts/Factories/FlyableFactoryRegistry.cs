using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    [System.Serializable]
    public struct FlyableFactoryEntry
    {
        public FlyableType flyableType;
        public FlyableFactory factory;
    }

    [CreateAssetMenu(fileName = "FlyableFactoryRegistry", menuName = "ScriptableObjects/Factories/FlyableFactoryRegistry")]
    public class FlyableFactoryRegistry : ScriptableObject
    {
        [SerializeField] private List<FlyableFactoryEntry> _factories = new();         

        private Dictionary<FlyableType, FlyableFactory> _factoryLookup = new Dictionary<FlyableType, FlyableFactory>();

        public void Init()
        {
            _factoryLookup.Clear();

            foreach (var entry in _factories)
            {
                if (!_factoryLookup.ContainsKey(entry.flyableType))
                {
                    _factoryLookup.Add(entry.flyableType, entry.factory);
                }
            }
        }

        public IFlyable CreateFlyable(FlyableType flyableType)
        {
            if (_factoryLookup.TryGetValue(flyableType, out FlyableFactory factory))
            {                
                return factory.CreateFlyable();
            }

            Debug.LogError($"No factory registered for FlyableType: {flyableType}");
            return null;
        }

        public IFlyable CreateRandomFlyable()
        {
            int randomIndex = Random.Range(0, _factories.Count);
            var randomFactory = _factories[randomIndex].factory;

            return randomFactory.CreateFlyable();
        }

    }

}