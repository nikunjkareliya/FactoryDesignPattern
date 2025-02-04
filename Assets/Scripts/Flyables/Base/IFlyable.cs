using UnityEngine;

namespace DesignPatterns.Factory
{
    public interface IFlyable
    {        
        void Init(LetterEntry entry);
        void Fly();
        void Pop();        
    }
}