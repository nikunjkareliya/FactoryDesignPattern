using UnityEngine;

namespace DesignPatterns.Factory
{
    [CreateAssetMenu(fileName = "BubbleFactory", menuName = "ScriptableObjects/Factories/BubbleFactory")]
    public class BubbleFactory : FlyableFactory
    {
        public FlyableConfig config;
        public override IFlyable CreateFlyable()
        {
            var obj = Instantiate(config.flyablePrefab, config.spawnPos, Quaternion.identity);
            return obj.GetComponent<IFlyable>();
        }
    }
}
