using UnityEngine;

namespace DesignPatterns.Factory
{
    [CreateAssetMenu(fileName = "HeartFactory", menuName = "ScriptableObjects/Factories/HeartFactory")]
    public class HeartFactory : FlyableFactory
    {
        public FlyableConfig config;
        public override IFlyable CreateFlyable()
        {
            var obj = Instantiate(config.flyablePrefab, config.spawnPos, Quaternion.identity);
            return obj.GetComponent<IFlyable>();
        }
    }
}
