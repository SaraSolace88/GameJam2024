using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
//using Unity.VisualScripting.Dependencies.Sqlite; // commented out to make a build put back if needed
using UnityEngine;
using UnityEngine.UI;
using static jokes;

public class joke_gen : MonoBehaviour
{

    float glyphs_per_second;

    // GameObjects.
    [SerializeField] GameObject joke_text_obj;
    [SerializeField] private GameObject TextBubble;
    [SerializeField] private AudioSource Speech;
    [SerializeField] private new List<AudioClip> VoiceAudioClips;
    TMP_Text joke_text;

    private PauseMenu pause_sys;

    float dsp_start_time;

    String the_joke = "";
    int joke_progress = 0; // How many glyphs are we displaying?
    int umm_progress = 0;
    String umm_str = "";

    int words_progress = 0;
    int word_glyph_progress = 0;
    string[] words;
    String rendered_joke = "";
    bool put_space_next_update = false;

    bool running_joke = false;
    bool[] used = new bool[jokes.so_many_jokes.Length];

    // Start is called before the first frame update
    void Start()
    {
        // Gameobject things =========================
        if (joke_text_obj == null)
        {
            Debug.LogError("Joke Manager : GameObject 'joke_text_obj' is not set!");
            return;

        }
        joke_text = joke_text_obj.GetComponent<TMP_Text>();

        if (joke_text == null)
        {
            Debug.LogError("Joke Manager : GameObject 'joke_text' doesn't have the component 'TMP_Text'!");
            return;
        }
        // ============

        pause_sys = GameObject.FindWithTag("SceneLoader").GetComponent<PauseMenu>();
        // Context init =================================

        // This is how you change the text.
        joke_text.text = "";

        Array.Fill(used, false);
    }
    private void set_dsp_time(float dsp_song_start) { 
            dsp_start_time = dsp_song_start;

    }
    private void OnEnable() {
        // Subscribing to rhythm system things.
        RhythmSystem.OnSongStart += set_dsp_time;
        RhythmSystem.OnJokeStart += start_new_joke;
        EndHit.HitMiss += missed_note;
    }

    private void OnDisable() {
        RhythmSystem.OnSongStart -= set_dsp_time;
        RhythmSystem.OnJokeStart -= start_new_joke;
        EndHit.HitMiss -= missed_note;
    }

    void missed_note() {
        Uhhh();
    }

    void Uhhh()
    {

        if (umm_str.Length > 0 || joke_progress == the_joke.Length || the_joke.Length == 0)
        {
            return;
        }

        List<string> base_uhhs = new List<string>()
            {
                "uhh",
                "umm",
                "...",
                "*sigh*",
                "aaah",
                "bruh",
                "your mom"
            };
        var rand = new System.Random();
        int selection = rand.Next(0,base_uhhs.Count);


        int uhh_min = base_uhhs[selection].Length;
        int uhh_length = 0;

        int uhh_words_progress = words_progress;

        if (the_joke.Length - joke_progress < uhh_min+1)
        {
            return;
        }
        while(uhh_length < uhh_min)
        {
            int glyphs_left = words[uhh_words_progress][word_glyph_progress..].Length;
            uhh_length += glyphs_left;
            word_glyph_progress = 0; // always starting at the beginning of the words.
            uhh_words_progress += 1; // look at next word.
            words_progress += 1;

            // Good luck.
            // Only add 1 to the uhh_length when this while loop *will* run again at least once.
            if(uhh_length < uhh_min-1)
            {
                uhh_length += 1; // Accounts for the spaces between words.
            }
        }
        
        if(uhh_length < uhh_min)
        {
            Debug.LogError("HALT AND CATCH FIRE!");
            return;
        }

        
        umm_str = base_uhhs[selection] + new string('.', uhh_length - uhh_min);
    }

    IEnumerator JokeGoAway() {
        yield return new WaitForSeconds(2f);

        // Only erase the joke if another joke hasn't started already.
        if(running_joke == false) {
            joke_text.text = "";
            TextBubble.SetActive(false);
        }
    }


    void start_new_joke(float joke_length_sec)
    {
        if(running_joke == true)
        {
            Debug.Log("Starting a new joke while one is already running? Are you sure you want to do that?");
        }
        TextBubble.SetActive(true);
        int I = UnityEngine.Random.Range(0, VoiceAudioClips.Count);
        Speech.PlayOneShot(VoiceAudioClips[I]);

        // Record the time in seconds when the song starts (based on the number of samples of the audio processor).
        dsp_start_time = (float)AudioSettings.dspTime;

        int num_of_jokes = jokes.so_many_jokes.Length;

        float ideal_length = 4 * joke_length_sec; //Ideal Joke String Length
        List<string> good_jokes = new List<string>();
        int tolerance = 6;
        
        while (good_jokes.Count < 4)
        {
            for (int i = 0; i < jokes.so_many_jokes.Length; i++)
            {
                if (used[i] == false && jokes.so_many_jokes[i].Length >= ideal_length - tolerance && jokes.so_many_jokes[i].Length <= ideal_length + tolerance)
                {
                    good_jokes.Add(jokes.so_many_jokes[i]);
                    used[i] = true;
                }
            }
            tolerance += 1;
        }

        var rand = new System.Random();
        int joke_idx = rand.Next(0, good_jokes.Count);

        // pick the joke to use.
        the_joke = good_jokes[joke_idx];
        
        // Init other variables.
        glyphs_per_second = the_joke.Length / joke_length_sec ;
        words = the_joke.Split(" ");
        joke_progress = 0;
        umm_progress = 0;
        words_progress = 0;
        word_glyph_progress = 0;
        put_space_next_update = false;
        rendered_joke = "";

        running_joke = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (words != null && words_progress == words.Length) {
            running_joke = false;
        }

        // If we are finished, be done.
        if ( running_joke == false || pause_sys.IsPaused == true )
        {
            return;
        }

        float song_time_sec = ((float)AudioSettings.dspTime - dsp_start_time);

        float last_glyph_written_sec = joke_progress / glyphs_per_second;

        // Amount of time (in seconds) since the last glyph has been written.
        float glyph_diff = song_time_sec - last_glyph_written_sec;
        if(glyph_diff >= (1/glyphs_per_second) ) {

            if(put_space_next_update)
            {
                put_space_next_update = false;
                rendered_joke += " ";
            }
            else if (umm_str.Length > 0)
            {
                rendered_joke += umm_str[umm_progress];
                umm_progress += 1;

                if ( umm_progress == umm_str.Length)
                {
                    umm_str = "";
                    umm_progress = 0;
                    put_space_next_update = true;
                }
            }
            else
            {
                // Continue the joke.
                rendered_joke += words[words_progress][word_glyph_progress];
                word_glyph_progress += 1;
                if(word_glyph_progress == words[words_progress].Length)
                {
                    words_progress += 1;
                    word_glyph_progress = 0;
                    put_space_next_update = true;
                }
            }

            joke_progress += 1;

            joke_text.text = rendered_joke;

            // check if we have finished. If we have, don't do anymore.
            if (joke_progress == the_joke.Length)
            {
                running_joke = false;
                StartCoroutine(JokeGoAway());
            }
        }
    }
}