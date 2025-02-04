using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{    
    [CreateAssetMenu(fileName = "ColorsConfig", menuName = "ScriptableObjects/ColorsConfig")]
    public class ColorsConfig : ScriptableObject
    {
        public Color[] colors;
    }
}
