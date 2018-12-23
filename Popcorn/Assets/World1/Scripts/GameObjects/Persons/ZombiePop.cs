using UnityEngine;
using Popcorn.Bases;
using Popcorn.Managers;
using Popcorn.Metadatas;
using Popcorn.ObjectsServices;
using Popcorn.ObjectsModifiers;
using Popcorn.GameObjects.Elementies;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using GameStates = Popcorn.GameObjects.Elementies.GameBehavior.GameStates;

namespace Popcorn.GameObjects.Persons
{

    [RequireComponent(typeof(AudioSource))]
    public class ZombiePop : EnemyBase
    {

        enum AnimationParameters { WalkTrigger };

        [SerializeField] float velocity = 1.2f;
        [SerializeField] bool isWalking = false;
        [SerializeField] bool startPostionToLeft = true;
        [SerializeField] float timeToWalk = 1f;

        float direction = Transforms.Direction.Right;
        float walkClock = 0;

        Move move = new Move();

        AudioSource deathAudioSource;

        override protected void Awake()
        {
            base.Awake();
            deathAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 0);
        }

        void Start()
        {
            if (startPostionToLeft)
            {
                direction = Transforms.Direction.Left;
            }
            else
            {
                float scaleX = rb.transform.localScale.x;

                rb.transform.localScale = new Vector3(-scaleX,
                    Transforms.Scale.Normal,
                    Transforms.Scale.Normal);
            }
        }

        void FixedUpdate() { walkClock += Time.deltaTime; }

        protected override void Update()
        {
            base.Update();

            if (GameBehavior.GameState != GameStates.Runing || !IsAlive)
            {
                if (animator.speed != 0) { animator.speed = 0; }

                if (GameBehavior.GameState == GameStates.Runing) { rb.transform.Rotate(Vector3.forward * 2 * direction); }
                return;
            }

            if (isWalking) { Walk(); }
        }

        void Walk()
        {
            if (walkClock >= timeToWalk)
            {
                move.Execute(rb, velocity * direction);
                animator.SetTrigger(AnimationParameters.WalkTrigger.ToString());
                CheckedIfNeedIversionOfPosition();
                walkClock = 0;
            }
        }

        void CheckedIfNeedIversionOfPosition()
        {
            if (HasHoleAhead() || HasObstacleAhead()) { InvertionOfDirection(); }
        }

        bool HasObstacleAhead()
        {
            Vector2 direction;
            Vector2 position = transform.position;

            if (this.direction == Transforms.Direction.Left) { direction = Vector2.left; }
            else { direction = Vector2.right; }

            position.x += this.direction;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, 0.08f);

            if (hit.collider != null && hit.collider.tag != PersonsTags.Player.ToString()) { return true; }
            return false;
        }

        bool HasHoleAhead()
        {
            Vector2 position = transform.position;
            position.x += direction;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1);

            if (hit.collider == null) { return true; }
            return false;
        }

        void InvertionOfDirection()
        {

            if (direction == Transforms.Direction.Right)
            {
                direction = Transforms.Direction.Left;
            }
            else
            {
                direction = Transforms.Direction.Right;
            }

            float scaleX = rb.transform.localScale.x;

            rb.transform.localScale = new Vector3(-scaleX,
                Transforms.Scale.Normal,
                Transforms.Scale.Normal);
        }

        protected override void WeakPointHitted()
        {
            if (IsAlive)
            {
                IsAlive = false;
                KillAnimation();
            }
        }

        void KillAnimation()
        {
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: deathAudioSource);
            animator.speed = 0;
            rb.velocity = Vector2.zero;
            (Getter.Component(this, gameObject, typeof(Collider2D)) as Collider2D).isTrigger = true;
            (Getter.ComponentInChild(this, gameObject, typeof(CircleCollider2D), 0) as CircleCollider2D).isTrigger = true;
            spriteRenderer.sortingOrder = (int)Layers.OrdersInDefaultLayer.Max;
            rb.AddForce(new Vector2(0, 100));
            rb.gravityScale = Transforms.Gravity.Hard;
        }

    }

}