using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2.Block;
using System;
using System.Diagnostics;


public class Sounds
{
    private SoundEffect coin;
    private SoundEffect fireball;
    private SoundEffect fireworks;
    private SoundEffect jump;
    private SoundEffect die;
    private SoundEffect pause;
    private SoundEffect pipe;
    private SoundEffect big;
    private SoundEffect stomp;
    private SoundEffect kick;
    private SoundEffect power;
    private Song music;
    private Song gameOver;
    private Song ending;
    private Stopwatch stopwatchKick, stopwatchPower, stopwatchFire;
    private const int ThrottleInterval = 100;

    public Sounds(ContentManager Content)
    {
        coin = Content.Load<SoundEffect>("sounds/smb_coin");
        fireball = Content.Load<SoundEffect>("sounds/smb_fireball");
        fireworks = Content.Load<SoundEffect>("sounds/smb_fireworks");
        jump = Content.Load<SoundEffect>("sounds/smb_jumpsmall");
        die = Content.Load<SoundEffect>("sounds/smb_mariodie");
        pause = Content.Load<SoundEffect>("sounds/smb_pause");
        pipe = Content.Load<SoundEffect>("sounds/smb_pipe");
        big = Content.Load<SoundEffect>("sounds/smb_vine");
        stomp = Content.Load<SoundEffect>("sounds/smb_stomp");
        kick = Content.Load<SoundEffect>("sounds/smb_kick");
        power = Content.Load<SoundEffect>("sounds/smb_powerup_appears");

        music = Content.Load<Song>("sounds/01-main-theme-overworld");
        gameOver = Content.Load<Song>("sounds/10-game-over-2");
        ending = Content.Load<Song>("sounds/12-ending");

        stopwatchKick = new Stopwatch();
        stopwatchPower = new Stopwatch();
        stopwatchFire = new Stopwatch();
        stopwatchKick.Start();
        stopwatchPower.Start();
        stopwatchFire.Start();
    }

    public void startMusic()
    {
        MediaPlayer.Play(music);
        MediaPlayer.IsRepeating = true;
    }

    public void stopMusic()
    {
        MediaPlayer.Stop();
    }

    public void startEnd()
    {
        MediaPlayer.Play(ending);
        MediaPlayer.IsRepeating = true;
    }

    public void playKick()
    {
        if (stopwatchKick.ElapsedMilliseconds >= ThrottleInterval)
        {
            kick.Play();
            stopwatchKick.Restart();
        }
    }

    public void playPower()
    {
        if (stopwatchPower.ElapsedMilliseconds >= ThrottleInterval)
        {
            power.Play();
            stopwatchPower.Restart();
        }
    }

    public void playBig()
    {
        big.Play();
    }

    public void playStomp()
    {
        stomp.Play();
    }

    public void playPipe()
    {
        pipe.Play();
    }

    public void playCoin()
    {
        coin.Play();
    }

    public void playFireball()
    {
        fireball.Play();
    }

    public void playFireworks()
    {
        if (stopwatchFire.ElapsedMilliseconds >= ThrottleInterval)
        {
            fireworks.Play();
            stopwatchFire.Restart();
        }
    }

    public void playDie()
    {
        die.Play();
    }

    public void playJump()
    {
        jump.Play();
    }

    public void playPause()
    {
        pause.Play();
    }

    public void playGameOver()
    {
        MediaPlayer.Play(gameOver);
    }
}