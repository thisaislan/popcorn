using Popcorn.Bases;
using UnityEngine;
using Popcorn.ObjectsServices;

namespace Popcorn.GameObjects.Objects
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ParticleSystem))]
    public class Phosphor : RunByTriggerBase
    {

        enum AnimationParameters { isPlayerNear };

        Animator animator;
        ParticleSystem particleSys;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
        }

        void GetComponents()
        {
            animator = (Animator)Getter.Component(this, gameObject, typeof(Animator));
            particleSys = (ParticleSystem)Getter.Component(this, gameObject, typeof(ParticleSystem));
        }

        protected override void StartRun()
        {
            animator.SetBool(AnimationParameters.isPlayerNear.ToString(), true);
            particleSys.Play();
        }

        protected override void StopRun()
        {
            animator.SetBool(AnimationParameters.isPlayerNear.ToString(), false);
            particleSys.Stop();
        }

    }
}
