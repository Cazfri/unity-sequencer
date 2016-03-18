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
	// 3rd party plugins or assets
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

	void Update () {
		if (!metro.isRunning() && someTrackState(TrackState.ARMED)) {
			print ("Metro is starting because I pressed a button");
			foreach (Track t in tracks) {
				t.source.Play();
			}
			metro.go();
		}
		// Update track states on metronome's new measures
		if (metro.isNewMeasure()) {
			print ("Sequencer sees new measure");
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
	
	void OnGUI() {
		// Buttons to trigger each track
		int buttonWidth = 75;
		int buttonHeight = 20;
		for (int i = 0; i < this.numberOfTracks; ++i) {
			if (GUI.Button(new Rect(buttonWidth * i, Screen.height - buttonHeight, buttonWidth, buttonHeight), tracks[i].name)) {
				tracks[i].state = TrackState.ARMED;
				print (tracks[i].name + " armed!");
			}
		}
		// State indicators for each track
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

//	private void play() {
//		if (!clipsAreLoaded()) {
//			Debug.LogWarning ("Warning: Sequencer cannot play clips because they are not all loaded");
//			return;
//		}
//		metro.go();
//	}

//	private void stop() {
//		metro.stop();
//		foreach (Track t in tracks) {
//			t.source.mute = true;
//			t.state = Sequencer.TrackState.OFF;
//			t.source.Play();
//		}
//	}
	
	private bool clipsAreLoaded() {
		foreach (Track t in tracks) {
			if (t.source.clip.loadState != AudioDataLoadState.Loaded) {
				return false;
			}
		}
		return true;
	}

	// Returns a true if there are no tracks currently running. If this is so,
	// then the sequencer should stop the metronome, essentially restarting the song
	private bool someTrackState(TrackState state) {
		foreach (Track t in tracks) {
			if (t.state == state) {
				return true;
			}
		}
		return false;
	}
}
