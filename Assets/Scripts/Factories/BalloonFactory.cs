using UnityEngine;

namespace DesignPatterns.Factory
{
    [CreateAssetMenu(fileName = "BalloonFactory", menuName = "ScriptableObjects/Factories/BalloonFactory")]
    public class BalloonFactory : FlyableFactory
    {
        public FlyableConfig config;
        public override IFlyable CreateFlyable()
        {
            var obj = Instantiate(config.flyablePrefab, config.spawnPos, Quaternion.identity);
            return obj.GetComponent<IFlyable>();
        }
    }
}
