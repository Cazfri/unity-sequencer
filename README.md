# unity-sequencer
## What is this?
This is [music sequencer](https://en.wikipedia.org/wiki/Music_sequencer) in Unity, similar to that of [Ableton Live's Session View](https://www.ableton.com/en/help/article/getting-started-5-working-session-view/). Load in a song's audio tracks to AudioSource components. Pressing one of the buttons will arm a track, and the track will begin playing on the first beat of a measure. To stop playing a track, arm it and it will stop playing on the first beat of a measure.

## Known issues:
None at the moment :)

## Future goals:
- Adapt Metronome current beat equation (see Metro.Update()) to account for time signatures other than X/4
- Change how Sequencer tracks' AudioSources are instantiated so that they can be created at runtime. May require 3rd party plugins.
