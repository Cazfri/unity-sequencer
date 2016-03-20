using UnityEngine;
using System.Collections;

public class Sequencer : MonoBehaviour {

	private enum TrackState {OFF, ON, ARMED};
	
	private struct Track {
		public AudioSource source;
		public TrackState state;
		public string name;
	}

	public Metro metro;
	private Track[] tracks;
	private int numberOfTracks = 4;

	// Hard-coding the 4 AudioSources and names isn't scalable, but I'm unsure
	// how I would create the AudioSources at runtime. I may need to look into
	// 3rd party plugins or assets.
	public AudioSource track0, track1, track2, track3;
	public string track0Name, track1Name, track2Name, track3Name;
	
	void Start () {
		// Group AudioSources and corresponding names in arrays
		AudioSource[] sources = new AudioSource[4] {track0, track1, track2, track3};
		string[] trackNames = new string[4] {track0Name, track1Name, track2Name, track3Name};
		// Instantiate member variables
		tracks = new Track[4];
		for (int i = 0; i < numberOfTracks; ++i) {
			tracks[i].source = sources[i];
			tracks[i].state = Sequencer.TrackState.OFF;
			tracks[i].name = trackNames[i];
		}
	}

	/*
	 * Update() changes track states from armed to either on or off at the beginning of a
	 * measure.
	 */ 
	void Update () {
		// Clicking a track button when no tracks are playing turns the track on rather than
		// arming it
		if (!metro.isRunning() && someTrackState(TrackState.ON)) {
			foreach (Track t in tracks) {
				t.source.Play();
				if (t.state != TrackState.ON) {
					t.source.mute = true;
				} else {
					t.source.mute = false;
				}
			}
			metro.go();
		}
		// Update track states if there is a new measure, turning armed tracks on or off.
		if (metro.isNewMeasure()) {
			for (int i = 0; i < numberOfTracks; ++i) {
				if (tracks[i].state == TrackState.ARMED) {
					if (tracks[i].source.mute) {
						tracks[i].state = TrackState.ON;
						tracks[i].source.mute = false;
					} else {
						tracks[i].state = TrackState.OFF;
						tracks[i].source.mute = true;
					}
				}
			}
			if (!someTrackState(TrackState.ON) && !someTrackState(TrackState.ARMED)) {
				metro.stop();
			}
		}
	}

	/*
	 * OnGUI() is used to draw the buttons and track state indicators. It uses the Unity GUI
	 * as the user view and controller, but should be rewritten for other uses, such as triggering
	 * animations when a track is running.
	 */
	void OnGUI() {
		// Draw buttons to trigger each track.
		int buttonWidth = 75;
		int buttonHeight = 20;
		for (int i = 0; i < this.numberOfTracks; ++i) {
			if (GUI.Button(new Rect(buttonWidth * i, Screen.height - buttonHeight, buttonWidth, buttonHeight), tracks[i].name)) {
				// The first track to be started turns right on instead of being armed.
				if (!metro.isRunning()) {
					tracks[i].state = TrackState.ON;
				} else {
					tracks[i].state = TrackState.ARMED;
				}
			}
		}
		// Draw state indicators for each track.
		for (int i = 0; i < this.numberOfTracks; ++i) {
			switch (tracks[i].state) {
			case TrackState.ON:
				GUI.backgroundColor = Color.green;
				break;
			case TrackState.OFF:
				GUI.backgroundColor = Color.red;
				break;
			case TrackState.ARMED:
				GUI.backgroundColor = Color.yellow;
				break;
			}
			GUI.Button(new Rect(buttonWidth * i, Screen.height - buttonHeight * 2, buttonWidth, buttonHeight), "Test");
		}
	}

	/*
	 * Returns true if all clips are loaded, used to make sure that the metronome doesn't
	 * start if the audio files aren't loaded. Not neccessarily needed if audio is loaded
	 * before the scene starts.
	 */
	private bool clipsAreLoaded() {
		foreach (Track t in tracks) {
			if (t.source.clip.loadState != AudioDataLoadState.Loaded) {
				return false;
			}
		}
		return true;
	}

	// Returns a true if there are no tracks currently running. If this is so,
	// then the sequencer should stop the metronome, essentially restarting the song.
	private bool someTrackState(TrackState state) {
		foreach (Track t in tracks) {
			if (t.state == state) {
				return true;
			}
		}
		return false;
	}
}
