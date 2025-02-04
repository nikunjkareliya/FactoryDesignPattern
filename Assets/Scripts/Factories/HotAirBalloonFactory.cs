using UnityEngine;

namespace DesignPatterns.Factory
{
    [CreateAssetMenu(fileName = "HotAirBalloonFactory", menuName = "ScriptableObjects/Factories/HotAirBalloonFactory")]
    public class HotAirBalloonFactory : FlyableFactory
    {
        public FlyableConfig config;
        public override IFlyable CreateFlyable()
        {
            var obj = Instantiate(config.flyablePrefab, config.spawnPos, Quaternion.identity);
            return obj.GetComponent<IFlyable>();
        }
    }
}
