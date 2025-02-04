using UnityEngine;

namespace DesignPatterns.Factory
{
    public abstract class FlyableFactory : ScriptableObject
    {                
        public abstract IFlyable CreateFlyable();
    }

}