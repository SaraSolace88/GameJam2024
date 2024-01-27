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

    int joke_progress = 0; // How many glyphs are we displaying?
    String the_joke = "What's the deal with airplane food?";

    // Start is called before the first frame update
    void Start()
    {
        // Gameobject things =========================
        // Joke things
        if (joke_text_obj == null)
        {
            Console.Error.WriteLine("Joke Manager : GameObject 'joke_text_obj' is not set!");
            return;

        }
        joke_text = joke_text_obj.GetComponent<TMP_Text>();

        if (joke_text == null)
        {
            Console.Error.WriteLine("Joke Manager : GameObject 'joke_text' doesn't have the component 'TMP_Text'!");
            return;
        }
        // ============

        // Audio things.
        if (audio_source_obj == null)
        {
            Console.Error.WriteLine("Joke Manager : GameObject 'audio_source_obj' is not set!");
            return;
        }
        audio_source = audio_source_obj.GetComponent<AudioSource>();
        if (audio_source == null)
        {
            Console.Error.WriteLine("Joke Manager : GameObject 'audio_source' doesn't have the component 'AudioSource'!");
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

    // Update is called once per frame
    void Update()
    {
        if ( joke_progress == the_joke.Length)
        {
            return;
        }
        float song_time_sec = ((float)AudioSettings.dspTime - dsp_start_time);

        float last_glyph_written_sec = joke_progress / glyphs_per_second;

        // Amount of time (in seconds) since the last glyph has been written.
        float glyph_diff = song_time_sec - last_glyph_written_sec;
        if(glyph_diff >= (1/glyphs_per_second) ) {
            joke_progress += 1;
            joke_text.text = the_joke[..joke_progress];
        }
    }
}
