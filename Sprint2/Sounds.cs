using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
    private Song music;
    private Song gameOver;
    private Song ending;

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

        music = Content.Load<Song>("sounds/01-main-theme-overworld");
        gameOver = Content.Load<Song>("sounds/10-game-over-2");
        ending = Content.Load<Song>("sounds/12-ending");
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
        fireworks.Play();
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