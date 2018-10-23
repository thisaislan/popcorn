using UnityEngine;
using Popcorn.Interfaces;

namespace Popcorn.ObjectsModifiers
{

    public class Jump : ICommandMoves
    {

        public void Execute(Rigidbody2D rigidbody2D, float value)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            rigidbody2D.AddForce(Vector2.up * value);
        }

    }
}
