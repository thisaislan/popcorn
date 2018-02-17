using UnityEngine;
using Popcorn.Bases;
using Errors = Popcorn.Metadados.Strings.Errors;
using ErrorsAuxs = Popcorn.Metadados.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadados.Strings.CombineCharacters;

namespace Popcorn.Managers
{

    public sealed class AudioManager : ManagerBase<AudioManager>
    {

        private AudioSource backgroundMusic;

        public void PlayBackgroundMusic(UnityEngine.Object caller, AudioSource music)
        {
            backgroundMusic = music;
            PlaySource(backgroundMusic, true);
        }

        public void StopBackgroundMusic(UnityEngine.Object caller)
        {
            if (backgroundMusic != null) backgroundMusic.Stop();
            else throw new UnityException(Errors.ATTEMPT_STOP_DONT_INITIALIZED_BACKGROUND_MUSIC +
               CombineCharacters.SPACE_COLON_SPACE +
               ErrorsAuxs.CALLER +
               caller.ToString());
        }

        public void PlaySoundOnce(UnityEngine.Object caller, AudioSource sound)
        {
            PlaySource(sound, false);
        }

        void PlaySource(AudioSource source, bool inloop)
        {
            source.loop = inloop;
            source.Play();
        }

    }
}