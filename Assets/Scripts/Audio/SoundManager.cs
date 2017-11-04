using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Audio Clips in game
    public enum AudioClips {
        //MUSIC
        GameMusic1, GameMusic2, GameMusic3, GameMusic4, GameMusic5, GameMusic6, GameMusic7,
        //PLAYER
        Jump, Death, inTangible, Solid, Ricochet,
        //GRIM
        GrimLaugh, Fireball,
        //SFX
        LanternLit, LanternLaugh, RopeSnap, Thunder,
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
	void Awake () {

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
    public void Play (AudioClips clip)
    {
        // MUSIC: 
        if (clip == AudioClips.GameMusic1 || clip == AudioClips.GameMusic2 || clip == AudioClips.GameMusic3 || clip == AudioClips.GameMusic4 || clip == AudioClips.GameMusic5 || clip == AudioClips.GameMusic6 || clip == AudioClips.GameMusic7)
        {

            AudioSource music = MusicSource.GetComponent<AudioSource>();
            SoundManagerChannel smc = MusicSource.GetComponent<SoundManagerChannel>();

  
            if (clip == AudioClips.GameMusic1)
            {
                music.clip = smc.Clips[0];
            }
            else if (clip == AudioClips.GameMusic2)
            {
                music.clip = smc.Clips[1];
            }
            else if (clip == AudioClips.GameMusic3)
            {
                music.clip = smc.Clips[2];
            }
            else if (clip == AudioClips.GameMusic4)
            {
                music.clip = smc.Clips[3];
            }
            else if (clip == AudioClips.GameMusic5)
            {
                music.clip = smc.Clips[4];
            }
            else if (clip == AudioClips.GameMusic6)
            {
                music.clip = smc.Clips[5];
            }
            else if (clip == AudioClips.GameMusic7)
            {
                music.clip = smc.Clips[6];
            }
            music.Play();
        }

        // PLAYER: 
        if (clip == AudioClips.Jump || clip == AudioClips.Death)
        {
            AudioSource player = PlayerSource.GetComponent<AudioSource>();
            SoundManagerChannel smc = PlayerSource.GetComponent<SoundManagerChannel>();

            if (clip==AudioClips.Jump) {
                player.clip = smc.Clips[0];
            }
            if (clip == AudioClips.Death)
            {
                player.clip = smc.Clips[1];
            }
            player.Play();
        }


        // GRIM: 
        if (clip == AudioClips.GrimLaugh || clip == AudioClips.Fireball)
        {
            AudioSource grim = GrimSource.GetComponent<AudioSource>();
            SoundManagerChannel smc = GrimSource.GetComponent<SoundManagerChannel>();

            if (clip == AudioClips.GrimLaugh)
            {
                grim.clip = smc.Clips[0];
            }
            if (clip == AudioClips.Fireball)
            {
                grim.clip = smc.Clips[1];
            }
            grim.Play();
        }

        // SOUND EFFECTS: 
        if (clip == AudioClips.LanternLit || clip == AudioClips.LanternLaugh || clip == AudioClips.RopeSnap || clip == AudioClips.Thunder)
        {
            AudioSource sfx = sfxSource.GetComponent<AudioSource>();
            SoundManagerChannel smc = sfxSource.GetComponent<SoundManagerChannel>();

            if (clip==AudioClips.LanternLit) {
                sfx.clip = smc.Clips[0];
            }
            else if (clip == AudioClips.LanternLaugh)
            {
                sfx.clip = smc.Clips[1];
            }
            else if (clip == AudioClips.RopeSnap)
            {
                sfx.clip = smc.Clips[2];
            }
            else if (clip == AudioClips.Thunder)
            {
                sfx.clip = smc.Clips[3];
            }
            sfx.Play();
        }
        
        // UI: 
        if (clip == AudioClips.Button)
        {
            AudioSource UI = UISource.GetComponent<AudioSource>();
            SoundManagerChannel smc = UISource.GetComponent<SoundManagerChannel>();

            if (clip == AudioClips.Button)
            {
                UI.clip = smc.Clips[0];
            }
            UI.Play();
        }

        // Ambiance
        if (clip == AudioClips.Rain || clip == AudioClips.Wind)
        {
            AudioSource ambiance = AmbianceSource.GetComponent<AudioSource>();
            SoundManagerChannel smc = AmbianceSource.GetComponent<SoundManagerChannel>();

            if (clip == AudioClips.Rain)
            {
                ambiance.clip = smc.Clips[0];
            }
            else if (clip == AudioClips.Wind)
            {
                ambiance.clip = smc.Clips[1];
            }
            ambiance.Play();
        }
    }

    private void Update()
    {
        //Music Preview (#)
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
    }
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
