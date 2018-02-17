using UnityEngine;

namespace Popcorn.Interfaces
{
    public interface ICommandMoves
    {
        void Execute(Rigidbody2D rb2D, float value);
        
    }
}