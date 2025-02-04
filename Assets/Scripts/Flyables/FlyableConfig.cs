using UnityEngine;

namespace DesignPatterns.Factory
{
    [CreateAssetMenu(fileName = "FlyableConfig", menuName = "ScriptableObjects/FlyableConfig")]
    public class FlyableConfig : ScriptableObject
    {
        public FlyableType flyableType;
        public GameObject flyablePrefab;
        public ParticleSystem popEffect;
        public Sprite sprite;
        public Vector3 spawnPos;        
        public float minSpeed = 1.2f;
        public float maxSpeed = 2.2f;
    }
}