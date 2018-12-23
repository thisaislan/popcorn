using UnityEngine;
using Popcorn.ObjectsServices;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class PersonBase : MonoBehaviour
    {

        protected Rigidbody2D rb;
        protected Animator animator;
        protected SpriteRenderer spriteRenderer;

        [HideInInspector] public bool IsAlive { get; protected set; }

        protected virtual void Awake()
        {
            IsAlive = true;

            GetComponents();
        }

        void GetComponents()
        {
            rb = (Rigidbody2D)Getter.Component(this, gameObject, typeof(Rigidbody2D));
            animator = (Animator)Getter.Component(this, gameObject, typeof(Animator));
            spriteRenderer = (SpriteRenderer)Getter.ComponentInChild(this, gameObject, typeof(SpriteRenderer), 0);
        }

    }

}