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
        [SerializeField] private List<FlyableFactoryEntry> factories = new();         

        private Dictionary<FlyableType, FlyableFactory> _factoryLookup = new Dictionary<FlyableType, FlyableFactory>();

        public void Initialize()
        {
            _factoryLookup.Clear();

            foreach (var entry in factories)
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
            int randomIndex = Random.Range(0, factories.Count);
            var randomFactory = factories[randomIndex].factory;

            return randomFactory.CreateFlyable();
        }

        //public FlyableConfig GetConfigForFlyable(IFlyable flyable)
        //{
        //    foreach (var entry in factories)
        //    {

        //        entry.factory.config.flyableType
        //        if (flyable.GetType() == entry.factory.config.flyablePrefab.GetComponent<IFlyable>().GetType())
        //        {
        //            return entry.factory.config;
        //        }
        //    }

        //    Debug.LogError("No matching config found for flyable.");
        //    return null;
        //}



    }

}