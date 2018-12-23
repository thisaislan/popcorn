using UnityEngine;
using Popcorn.Interfaces;

namespace Popcorn.ObjectsModifiers
{

    public class Move : ICommandMoves
    {

        public void Execute(Rigidbody2D rb, float value)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = value;
            rb.velocity = velocity;
        }

    }
    
}
