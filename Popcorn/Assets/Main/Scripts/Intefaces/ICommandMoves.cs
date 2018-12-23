using UnityEngine;

namespace Popcorn.Interfaces
{
    public interface ICommandMoves
    {
        void Execute(Rigidbody2D rb, float value);
        
    }
}