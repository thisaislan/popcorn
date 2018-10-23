using Popcorn.Bases;
using UnityEngine;
using Popcorn.ObjectsServices;

namespace Popcorn.GameObjects.Objects
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ParticleSystem))]
    public class Phosphor : RunByTriggerBase
    {

        enum AnimationParameters { IsPlayerNear };

        Animator Animator;
        ParticleSystem ParticleSys;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
        }

        void GetComponents()
        {
            Animator = (Animator)Getter.Component(this, gameObject, typeof(Animator));
            ParticleSys = (ParticleSystem)Getter.Component(this, gameObject, typeof(ParticleSystem));
        }

        protected override void StartRun()
        {
            Animator.SetBool(AnimationParameters.IsPlayerNear.ToString(), true);
            ParticleSys.Play();
        }

        protected override void StopRun()
        {
            Animator.SetBool(AnimationParameters.IsPlayerNear.ToString(), false);
            ParticleSys.Stop();
        }

    }
}
