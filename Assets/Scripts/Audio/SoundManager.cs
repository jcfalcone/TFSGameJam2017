using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // Audio Clips in game
    public enum AudioClips
    {

        Invalid = -1,

        //MUSIC
        GameMusic1, GameMusic2, GameMusic3, GameMusic4, GameMusic5, GameMusic6, GameMusic7,
        //PLAYER
        Death, inTangible, Solid, Ricochet, Whoosh,
        //GRIM
        GrimLaugh, Fireball,
        //SFX
        LanternLit, LanternLaugh, RopeSnap, Thunder, MatchStrike, Switch, Explosion, Clapping,
        //UI
        Button,
        //AMBIANCE
        Rain, Wind
    };

    //Child members of Sound Manager
    public Transform MusicSource;
    public Transform PlayerSource;
    public Transform GrimSource;
    public Transform sfxSource;
    public Transform UISource;
    public Transform AmbianceSource;

    public static SoundManager instance = null;


    // Use this for initialization
    void Awake()
    {
        //Instance creation
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        //Keep music consistent through scenes
        DontDestroyOnLoad(gameObject);
    }

    // Calling Reference: SoundManager.instance.Play(SoundManager.AudioClips.*** clip***)
    public void Play(AudioClips clip)
    {
        if (clip == AudioClips.Invalid)
        {
            return;
        }

        // MUSIC: 
        else if (clip == AudioClips.GameMusic1 || clip == AudioClips.GameMusic2 ||
            clip == AudioClips.GameMusic3 || clip == AudioClips.GameMusic4 ||
            clip == AudioClips.GameMusic5 || clip == AudioClips.GameMusic6 ||
            clip == AudioClips.GameMusic7)
        {
            AudioSource music = MusicSource.GetComponent<AudioSource>();

            music.clip = GetClip(clip);

            music.Play();
        }

        // PLAYER: 
        else if (clip == AudioClips.Whoosh || clip == AudioClips.inTangible || clip == AudioClips.Solid)
        {
            AudioSource player = PlayerSource.GetComponent<AudioSource>();

            player.clip = GetClip(clip);

            player.Play();
        }

        // GRIM: 
        else if (clip == AudioClips.GrimLaugh || clip == AudioClips.Fireball)
        {
            AudioSource grim = GrimSource.GetComponent<AudioSource>();

            grim.clip = GetClip(clip);

            grim.Play();
        }

        // SOUND EFFECTS: 
        else if (clip == AudioClips.LanternLit || clip == AudioClips.LanternLaugh ||
            clip == AudioClips.RopeSnap || clip == AudioClips.Thunder ||
            clip == AudioClips.MatchStrike || clip == AudioClips.Switch ||
            clip == AudioClips.Explosion || clip == AudioClips.Clapping ||
            clip == AudioClips.Ricochet)
        {
            AudioSource sfx = sfxSource.GetComponent<AudioSource>();

            sfx.clip = GetClip(clip);

            sfx.Play();
        }

        // UI: 
        else if (clip == AudioClips.Button)
        {
            AudioSource UI = UISource.GetComponent<AudioSource>();

            UI.clip = GetClip(clip);

            UI.Play();
        }

        // Ambiance
        else if (clip == AudioClips.Rain || clip == AudioClips.Wind)
        {
            AudioSource ambiance = AmbianceSource.GetComponent<AudioSource>();

            ambiance.clip = GetClip(clip);

            ambiance.Play();
        }
    }

    public AudioClip GetClip(AudioClips clipName)
    {
        SoundManagerChannel smc = null;

        if (clipName == AudioClips.GameMusic1 || clipName == AudioClips.GameMusic2 ||
            clipName == AudioClips.GameMusic3 || clipName == AudioClips.GameMusic4 ||
            clipName == AudioClips.GameMusic5 || clipName == AudioClips.GameMusic6 ||
            clipName == AudioClips.GameMusic7)
        {
            smc = MusicSource.GetComponent<SoundManagerChannel>();

            if (clipName == AudioClips.GameMusic1)
            {
                return smc.Clips[0];
            }
            else if (clipName == AudioClips.GameMusic2)
            {
                return smc.Clips[1];
            }
            else if (clipName == AudioClips.GameMusic3)
            {
                return smc.Clips[2];
            }
            else if (clipName == AudioClips.GameMusic4)
            {
                return smc.Clips[3];
            }
            else if (clipName == AudioClips.GameMusic5)
            {
                return smc.Clips[4];
            }
            else if (clipName == AudioClips.GameMusic6)
            {
                return smc.Clips[5];
            }
            else if (clipName == AudioClips.GameMusic7)
            {
                return smc.Clips[6];
            }
        }
        else if (clipName == AudioClips.Whoosh || clipName == AudioClips.inTangible || clipName == AudioClips.Solid)
        {
            smc = PlayerSource.GetComponent<SoundManagerChannel>();

            if (clipName == AudioClips.Whoosh)
            {
                return smc.Clips[0];
            }
            else if (clipName == AudioClips.inTangible)
            {
                return smc.Clips[1];
            }
            else if (clipName == AudioClips.Solid)
            {
                return smc.Clips[2];
            }
        }
        else if (clipName == AudioClips.GrimLaugh || clipName == AudioClips.Fireball)
        {
            smc = GrimSource.GetComponent<SoundManagerChannel>();

            if (clipName == AudioClips.GrimLaugh)
            {
                return smc.Clips[0];
            }
            else if (clipName == AudioClips.Fireball)
            {
                return smc.Clips[1];
            }
        }
        else if (clipName == AudioClips.LanternLit || clipName == AudioClips.LanternLaugh ||
            clipName == AudioClips.RopeSnap || clipName == AudioClips.Thunder ||
            clipName == AudioClips.MatchStrike || clipName == AudioClips.Switch ||
            clipName == AudioClips.Explosion || clipName == AudioClips.Clapping ||
            clipName == AudioClips.Ricochet)
        {
            smc = sfxSource.GetComponent<SoundManagerChannel>();

            if (clipName == AudioClips.LanternLit)
            {
                return smc.Clips[0];
            }
            else if (clipName == AudioClips.LanternLaugh)
            {
                return smc.Clips[1];
            }
            else if (clipName == AudioClips.RopeSnap)
            {
                return smc.Clips[2];
            }
            else if (clipName == AudioClips.Thunder)
            {
                return smc.Clips[3];
            }
            else if (clipName == AudioClips.MatchStrike)
            {
                return smc.Clips[4];
            }
            else if (clipName == AudioClips.Switch)
            {
                return smc.Clips[5];
            }
            else if (clipName == AudioClips.Explosion)
            {
                return smc.Clips[6];
            }
            else if (clipName == AudioClips.Clapping)
            {
                return smc.Clips[7];
            }
            else if (clipName == AudioClips.Ricochet)
            {
                return smc.Clips[8];
            }
        }
        else if (clipName == AudioClips.Button)
        {
            smc = UISource.GetComponent<SoundManagerChannel>();

            if (clipName == AudioClips.Button)
            {
                return smc.Clips[0];
            }
        }
        else if (clipName == AudioClips.Rain || clipName == AudioClips.Wind)
        {
            smc = AmbianceSource.GetComponent<SoundManagerChannel>();

            if (clipName == AudioClips.Rain)
            {
                return smc.Clips[0];
            }
            else if (clipName == AudioClips.Wind)
            {
                return smc.Clips[1];
            }
        }


        return null;
    }

    /* DEBUGGING
    private void Update()
    {
        //Music Preview (#1-7)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Play(AudioClips.GameMusic1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Play(AudioClips.GameMusic2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Play(AudioClips.GameMusic3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Play(AudioClips.GameMusic4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Play(AudioClips.GameMusic5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Play(AudioClips.GameMusic6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Play(AudioClips.GameMusic7);
            }
    } */
}


/*  Sound Effects: (Cited)
 *  www.freesoundeffects.com
 *  www.soundbible.com
 *  www.freesounds.org
 *  www.AudioMicro.com
 *  
 *  
 *  Music: (Cited)
 *  www.purpleplanet.com
 */
