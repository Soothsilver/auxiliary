using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Specialized;
using FMOD;
#pragma warning disable 1591

namespace Auxiliary
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public static class Audio
    {
        private static FMOD.System fSystem = null;
        private static Channel fCurrentChannel;

        public static void PlaySound(FSound fSound, float volume = 0.5f)
        {
            if (fSound == null) throw new ArgumentNullException("The FSound must not be null.");
            if (fSound.Sound == null) throw new ArgumentNullException("The FSound's inner sound must not be null.");
            fSystem.playSound(CHANNELINDEX.FREE, fSound.Sound, false, ref fSound.Channel);
            fSound.Channel.setVolume(volume);
        }

        public static Channel PlaySoundInNewChannel(FSound fSound, float volume = 0.5f)
        {
            if (fSound == null) throw new ArgumentNullException("The FSound must not be null.");
            if (fSound.Sound == null) throw new ArgumentNullException("The FSound's inner sound must not be null.");
            Channel returnChannel = null;
            fSystem.playSound(CHANNELINDEX.FREE, fSound.Sound, false, ref returnChannel);
            returnChannel.setVolume(volume);
            return returnChannel;
        }

        public static void fmod_PlayVoice(Sound fmodSound)
        {
            fSystem.playSound(CHANNELINDEX.FREE, fmodSound, false, ref fCurrentChannel);
        }

        public static void fmod_StopVoice(Channel fmodChannel)
        {
            bool isPlaying = false;
            fmodChannel.isPlaying(ref isPlaying);
            if (fmodChannel != null && isPlaying)
            {
                fmodChannel.stop();
            }
        }
        /*
        public void PlayVoice(FSound fs)
        {
            fSystem.playSound(CHANNELINDEX.FREE, fs.Sound, true, ref fs.Channel);
            fs.Channel.setVolume(globalSettings.VoiceVolume);
            fs.Channel.setPaused(false);
        }
         **/

        /// <summary>
        /// Loads a sound in a filename relative to the application directory.
        /// </summary>
        /// <param name="filename">example: "Audio\\sound.mp3"</param>
        /// <returns>The FSound class.</returns>
        public static FSound LoadSound(string filename)
        {
            FSound fs = new FSound();
            fSystem.createSound(System.IO.Path.Combine(THISFOLDER, filename), MODE.SOFTWARE, ref fs.Sound);
            if (fs.Sound == null)
            {
                throw new Exception("Sound was not loaded! Filename requested: " + filename + ". Full path: " + System.IO.Path.Combine(THISFOLDER, filename));
            }
            return fs;
        }
        /// <summary>
        /// Loads a sound in a filename relative to the .\\Audio subfolder. Also appends ".mp3" automatically at the end.
        /// </summary>
        /// <param name="filename">example: "sound" gets loaded from ".\\Audio\\sound.mp3"</param>
        /// <returns></returns>
        public static FSound LoadSoundFromAudioSubfolder(string filename)
        {
            return LoadSound("Audio\\" + filename + ".mp3");
        }

        public static void InitializeFMOD()
        {
            uint version = 0;
            FMOD.RESULT result;
            result = FMOD.Factory.System_Create(ref fSystem);
            fmod_errCheck(result);
            result = fSystem.getVersion(ref version);
            fmod_errCheck(result);
            if (version < FMOD.VERSION.number)
            {
          
            }
            fSystem.init(32, INITFLAGS.NORMAL, (IntPtr)null);
        }
        public static string THISFOLDER = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private static void fmod_errCheck(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                       
            }
        }
    }

    /// <summary>
    /// Wrapper class for a FMOD sound
    /// </summary>
    public class FSound
    {
        public Sound Sound;
        public Channel Channel;
        private float _volume = 0.5f;
        public void Play(float volume = -1)
        {
            if (volume != -1) _volume = volume;
            Audio.PlaySound(this, _volume);
        }
        public Channel PlayInNewChannel(float volume = -1)
        {
            Channel ch = Audio.PlaySoundInNewChannel(this, (volume == -1 ? _volume : volume));
            return ch;
        }
        public void Stop()
        {
            Audio.fmod_StopVoice(Channel);
        }
        public bool Paused
        {
            get
            {
                bool paused = false;
                if (Channel != null)
                {
                    Channel.getPaused(ref paused);
                    return paused;
                }
                return false;
            }
            set
            {
                if (Channel != null)
                    Channel.setPaused(value);
            }
        }
        public float Volume
        {
            
            get
            {
                float f = 0;
                if (Channel != null)
                {
                    Channel.getVolume(ref f);
                    return f;
                }
                else return _volume;
            }
            set
            {
                _volume = value;
                if (Channel != null)
                    Channel.setVolume(value);
            }
        }
        public bool IsPlaying
        {
            get
            {
                bool b = false;
                Channel.isPlaying(ref b);
                return b;
            }
        }
    }
}
#pragma warning restore 1591