using UnityEngine;
using Popcorn.Interfaces;

namespace Popcorn.ObjectsModifiers
{

    public class Move : ICommandMoves
    {

        public void Execute(Rigidbody2D rb2D, float value)
        {
            Vector2 vel = rb2D.velocity;
            vel.x = value;
            rb2D.velocity = vel;
        }

    }
}
