using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler self;

    [SerializeField] private AudioClip bubblePlop, collideEnemy,startGame, newHighscore, gameOver;
    [SerializeField] private AudioSource sfxSource, musicSource;
    [SerializeField] private Button muteButton;
    [SerializeField] private Sprite muteSprite, defaultSprite;
    /*[SerializeField] [Range(0f, 1f)]*/  private  float sfxFullVolume,musicFullVolume;

    private bool isMute;

    private void OnEnable()
    {
        Bubble.CollectBubble += PlopBubble;
        Enemy.CollideEnenmy += CollideEnemy;
    }
    private void OnDisable()
    {
        Bubble.CollectBubble -= PlopBubble;
        Enemy.CollideEnenmy -= CollideEnemy;
    }

    private void Awake()
    {
        if (self == null)
            self = this;
        else if (self != this)
            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        sfxFullVolume = sfxSource.volume;
        musicFullVolume = musicSource.volume;
        CheckMute();
    }

    private void PlopBubble(GameObject gameObject)
    {
        sfxSource.clip = bubblePlop;
        sfxSource.Play();
    }

    private void CollideEnemy(GameObject gameObject)
    {
        sfxSource.clip = collideEnemy;
        sfxSource.Play();
    }

    public void StartGame()
    {
        sfxSource.clip = startGame;
        sfxSource.Play();
    }

    public void NewHighscore()
    {
        sfxSource.clip = newHighscore;
        sfxSource.Play();
    }

    public void GameOver()
    {
        sfxSource.clip = gameOver;
        sfxSource.Play();
    }


    public void Mute()
    {
        isMute = !isMute;
        int mute = isMute ? 0 : 1;
        PlayerPrefs.SetInt("Mute", mute);
        CheckMute();
    }

    private void CheckMute()
    {
        if (PlayerPrefs.HasKey("Mute"))
            isMute = PlayerPrefs.GetInt("Mute") == 0 ? true : false;
        else
            isMute = false;

        if (isMute)
        {
            muteButton.GetComponent<Image>().sprite = muteSprite;
            musicSource.volume = 0;
            sfxSource.volume = 0;
        }
        else
        {
            muteButton.GetComponent<Image>().sprite = defaultSprite;
            musicSource.volume = musicFullVolume;
            sfxSource.volume = sfxFullVolume;
        }
    }
}
