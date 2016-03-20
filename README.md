# Unity Music Sequencer
## What is this?
This is [music sequencer](https://en.wikipedia.org/wiki/Music_sequencer) in Unity, similar to that of [Ableton Live's Session View](https://www.ableton.com/en/help/article/getting-started-5-working-session-view/). You can play a song by starting and stopping different instruments with on-screen buttons. Pressing an instrument's button will not start playing it, rather the track will becoming "armed". Armed tracks will start or stop playing at the beginning of measure.

## Running the sequencer
To run the sequencer, fork or download this repo, and open the Assets/_Scenes/main.unity file in Unity. Though the scene should work if downloaded from GitHub, some of the settings could potentially be lost. If the scene throws errors, you may need to reattach the audio files and references. Follow these steps if the scene won't work.
 - Locate the SequencerObject GameObject (the sphere in the main scene). Drag the 4 audio files in the /Assets/audio director into the AudioSource components' AudoClip field.
 - Attached to the SequencerObject is a Metro script. Set the bpm to 120.0, and the TimeTop and TimeBottom values to 4.
 - Attached to the SequencerObject is a Sequencer script. Drag the SequencerObject's Metro component to the Seqencer's "Metro" field . Drag the AudioSource components onto the "Track <0-3>" fields. Then, name the tracks in the "Track <0-3> Name" fields according to the audio files loaded into the AudioSources.
 - If you have any other questions, feel free to contact me at nmartinr@umich.edu.

## Known issues:
None at the moment.

## Future goals:
- Make other instruments that can be played by pressing buttons, such as a drum pad or on-screen keyboard.
    - This allows users to feel engaged with the piece by playing along with it.
- Make it so pressing a track's button while armed de-arms the track.
    - This is so that if a user accidentally arms a track, they can de-arm it.
- Adapt Metronome current beat equation (see Metro.Update()) to account for time signatures other than X/4
- Change how Sequencer tracks' AudioSources are instantiated so that they can be created at runtime. May require 3rd party plugins.


## Contact
If you have any questions, feel free to email me at nmartinr@umich.edu.