using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    [System.Serializable]
    public class LetterEntry
    {
        public string ID;
        public string value;
        public Sprite letterSprite;
        //public AudioClip letterObjectClip;
        //public AudioClip letterClip;
    }

    [CreateAssetMenu(fileName = "LettersConfig", menuName = "ScriptableObjects/LettersConfig")]
    public class LettersConfig : ScriptableObject
    {
        public string locale = "en";
        public List<LetterEntry> lettersMap = new List<LetterEntry>();
    }
}