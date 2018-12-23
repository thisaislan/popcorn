using UnityEngine;
using Popcorn.Bases;
using Errors = Popcorn.Metadatas.Strings.Errors;
using ErrorsAuxs = Popcorn.Metadatas.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadatas.Strings.CombineCharacters;

namespace Popcorn.Managers
{

    public sealed class AudioManager : ManagerBase<AudioManager>
    {

        AudioSource backgroundMusic;

        public void PlayBackgroundMusic(UnityEngine.Object caller, AudioSource music)
        {
            backgroundMusic = music;
            PlaySource(backgroundMusic, true);
        }

        public void StopBackgroundMusic(UnityEngine.Object caller)
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }
            else
            {
                throw new UnityException(Errors.AttemptStopDontInitializedBackgroundMusic +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    caller.ToString());
            }
        }

        public void PlaySoundOnce(UnityEngine.Object caller, AudioSource sound) { PlaySource(sound, false); }

        void PlaySource(AudioSource source, bool inloop)
        {
            source.loop = inloop;
            source.Play();
        }

    }

}