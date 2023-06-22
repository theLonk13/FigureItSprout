using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotAudio : MonoBehaviour
{
    //selection of random pot falling sounds
    [SerializeField] int numFallSounds;
    [SerializeField] AudioSource fall1;
    [SerializeField] AudioSource fall2;
    //Audio for unpotted plant planting
    [SerializeField] AudioSource unpottedPlant;

    //Audio for shy plant shrivel
    [SerializeField] AudioSource shyPlantShrivel;

    //Audio for blossom
    [SerializeField] AudioSource blossomPlantSound;

    //flag for playing
    bool fallSoundPlayed = false;

    public void playFallSound()
    {
        if (!fallSoundPlayed)
        {
            fallSoundPlayed = true;

            playRandFallSound();
        }
    }

    void playRandFallSound()
    {
        int check = Random.Range(0, numFallSounds);
        if(check == 0)
        {
            fall1.Play();
        }else if(check == 1)
        {
            fall2.Play();
        }
    }

    public void playShyPlantSound()
    {
        shyPlantShrivel.Play();
    }

    public void PlayBlossom()
    {
        blossomPlantSound.Play();
    }

    public void PlayPlantUnpotted()
    {
        unpottedPlant.Play();
    }
}
