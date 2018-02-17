using UnityEngine;
using Popcorn.Interfaces;

namespace Popcorn.ObjectsModifiers
{

    public class Jump : ICommandMoves
    {

        public void Execute(Rigidbody2D rb2D, float value)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
            rb2D.AddForce(Vector2.up * value);
        }

    }
}
