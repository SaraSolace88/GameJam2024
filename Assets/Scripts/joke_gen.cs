using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class joke_gen : MonoBehaviour
{

    const float glyphs_per_second = 4f;
    const int cashino_song_bpm = 88;

    // GameObjects.
    [SerializeField] GameObject joke_text_obj;
    TMP_Text joke_text;

    [SerializeField] GameObject audio_source_obj;
    AudioSource audio_source;
    float dsp_start_time;

    const String the_joke = "What's the deal with airplane food?";
    int joke_progress = 0; // How many glyphs are we displaying?
    int umm_progress = 0;
    String umm_str = "";

    int words_progress = 0;
    int word_glyph_progress = 0; // Be careful!
    string[] words = the_joke.Split(" ");
    String rendered_joke = "";
    bool put_space_next_update = false;
    // Start is called before the first frame update
    void Start()
    {
        // Gameobject things =========================
        // Joke things
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

        // Audio things.
        if (audio_source_obj == null)
        {
            Debug.LogError("Joke Manager : GameObject 'audio_source_obj' is not set!");
            return;
        }
        audio_source = audio_source_obj.GetComponent<AudioSource>();
        if (audio_source == null)
        {
            Debug.LogError("Joke Manager : GameObject 'audio_source' doesn't have the component 'AudioSource'!");
            return;
        }
        // Record the time in seconds when the song starts (based on the number of samples of the audio processor).
        dsp_start_time = (float)AudioSettings.dspTime;
        audio_source.Play();
        // ====================

        // Context init =================================

        // This is how you change the text.
        //text_obj.text = "Yo mama is so fat!";
        joke_text.text = "";

    }

    void Uhhh()
    {

        if (umm_str.Length > 0 || joke_progress == the_joke.Length)
        {
            return;
        }

        List<string> base_uhhs = new List<string>()
            {
                "uhh",
                "...",
                "aaa",
                "bruh",
                "your mom"
            };
        var rand = new System.Random();
        int selection = rand.Next(0,base_uhhs.Count);


        int uhh_min = base_uhhs[selection].Length;
        int uhh_length = 0;

        int uhh_words_progress = words_progress;

        float song_time_sec = ((float)AudioSettings.dspTime - dsp_start_time);

        float last_glyph_written_sec = joke_progress / glyphs_per_second;


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

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("a"))
        {
            //Debug.Log("yo mama");
            Uhhh();
        }

        if ( joke_progress == the_joke.Length)
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
        }
    }
}
