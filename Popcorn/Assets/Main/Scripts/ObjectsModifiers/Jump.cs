using UnityEngine;
using Popcorn.Interfaces;

namespace Popcorn.ObjectsModifiers
{

    public class Jump : ICommandMoves
    {

        public void Execute(Rigidbody2D rb, float value)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * value);
        }

    }
    
}
