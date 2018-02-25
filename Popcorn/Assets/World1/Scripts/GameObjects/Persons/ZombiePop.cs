using UnityEngine;
using Popcorn.Bases;
using Popcorn.Managers;
using Popcorn.Metadados;
using Popcorn.ObjectsServices;
using Popcorn.ObjectsModifiers;
using Popcorn.GameObjects.Elementies;
using PersonsTags = Popcorn.Metadados.Tags.Persons;
using GameStates = Popcorn.GameObjects.Elementies.GameBehavior.GameStates;

namespace Popcorn.GameObjects.Persons
{

    [RequireComponent(typeof(AudioSource))]
    public class ZombiePop : EnemyBase
    {

        enum AnimationParameters { WalkTrigger };

        [SerializeField]
        float velocity = 1.2f;
        [SerializeField]
        bool isWalking = false;
        [SerializeField]
        bool startPostionToLeft = true;
        [SerializeField]
        float timeToWalk = 1f;

        float dir = Transforms.Direction.RIGHT;
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
                dir = Transforms.Direction.LEFT;
            }
            else
            {
                float scaleX = rb2D.transform.localScale.x;

                rb2D.transform.localScale = new Vector3(-scaleX,
                    Transforms.Scale.NORMAL,
                    Transforms.Scale.NORMAL);
            }
        }

        void FixedUpdate()
        {
            walkClock += Time.deltaTime;
        }

        protected override void Update()
        {
            base.Update();

            if (GameBehavior.GameState != GameStates.Runing || !isAlive)
            {
                if (animator.speed != 0) animator.speed = 0;
                if (GameBehavior.GameState == GameStates.Runing) rb2D.transform.Rotate(Vector3.forward * 2 * dir);
                return;
            }

            if (isWalking) Walk();
        }

        void Walk()
        {
            if (walkClock >= timeToWalk)
            {
                move.Execute(rb2D, velocity * dir);
                animator.SetTrigger(AnimationParameters.WalkTrigger.ToString());
                CheckedIfNeedIversionOfPosition();
                walkClock = 0;
            }
        }

        private void CheckedIfNeedIversionOfPosition()
        {
            if (HasHoleAhead() || HasObstacleAhead()) InvertionOfDirection();
        }

        bool HasObstacleAhead()
        {
            Vector2 direction;
            Vector2 position = transform.position;

            if (dir == Transforms.Direction.LEFT) direction = Vector2.left;
            else direction = Vector2.right;

            position.x += dir;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, 0.08f);

            if (hit.collider != null && hit.collider.tag != PersonsTags.Player.ToString()) return true;
            return false;
        }

        bool HasHoleAhead()
        {
            Vector2 position = transform.position;
            position.x += dir;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1);

            if (hit.collider == null) return true;
            return false;
        }

        void InvertionOfDirection()
        {

            if (dir == Transforms.Direction.RIGHT) dir = Transforms.Direction.LEFT;
            else dir = Transforms.Direction.RIGHT;

            float scaleX = rb2D.transform.localScale.x;

            rb2D.transform.localScale = new Vector3(-scaleX,
                Transforms.Scale.NORMAL,
                Transforms.Scale.NORMAL);
        }

        protected override void WeakPointHitted()
        {            
            if (isAlive)
            {
                isAlive = false;
                KillAnimation();
            }
        }

        void KillAnimation()
        {
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: deathAudioSource);
            animator.speed = 0;
            rb2D.velocity = Vector2.zero;
            (Getter.Component(this, gameObject, typeof(Collider2D)) as Collider2D).isTrigger = true;
            (Getter.ComponentInChild(this, gameObject, typeof(CircleCollider2D), 0) as CircleCollider2D).isTrigger = true;
            spriteR.sortingOrder = (int)Layers.OrdersInDefaultLayer.Max;
            rb2D.AddForce(new Vector2(0, 100));
            rb2D.gravityScale = Transforms.Gravity.HARD;
        }

    }
}
