using UnityEngine;
using Popcorn.ObjectsServices;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class PersonBase : MonoBehaviour
    {

        protected Rigidbody2D rb2D;
        protected Animator animator;
        protected SpriteRenderer spriteR;

        [HideInInspector]
        public bool isAlive { get; protected set; }

        protected virtual void Awake()
        {
            isAlive = true;

            GetComponents();
        }

        void GetComponents()
        {
            rb2D = (Rigidbody2D)Getter.Component(this, gameObject, typeof(Rigidbody2D));
            animator = (Animator)Getter.Component(this, gameObject, typeof(Animator));
            spriteR = (SpriteRenderer)Getter.ComponentInChild(this, gameObject, typeof(SpriteRenderer), 0);
        }

    }
}