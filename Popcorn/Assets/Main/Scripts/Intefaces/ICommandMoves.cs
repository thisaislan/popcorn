using UnityEngine;

namespace Popcorn.Interfaces
{
    public interface ICommandMoves
    {
        void Execute(Rigidbody2D rigidbody2D, float value);
        
    }
}