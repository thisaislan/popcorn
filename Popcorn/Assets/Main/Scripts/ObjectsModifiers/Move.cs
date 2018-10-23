using UnityEngine;
using Popcorn.Interfaces;

namespace Popcorn.ObjectsModifiers
{

    public class Move : ICommandMoves
    {

        public void Execute(Rigidbody2D rigidbody2D, float value)
        {
            Vector2 velocity = rigidbody2D.velocity;
            velocity.x = value;
            rigidbody2D.velocity = velocity;
        }

    }
}
