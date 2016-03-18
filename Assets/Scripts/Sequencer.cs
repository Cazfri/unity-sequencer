using UnityEngine;
using System.Collections;

public class Sequencer : MonoBehaviour {

	private enum TrackState {OFF, ON, ARMED};
	
	private struct Track {
		public AudioSource source;
		public TrackState state;
		public string name;
	}

	private Metro metro;
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
		metro = new Metro(120.0F, 4, 4);
		tracks = new Track[4];
		for (int i = 0; i < numberOfTracks; ++i) {
			tracks[i].source = sources[i];
			tracks[i].state = Sequencer.TrackState.OFF;
			tracks[i].name = trackNames[i];
		}
	}

	void Update () {
		// Update button colors
		// Update track states on metronome's new measures
		if (metro.isNewMeasure()) {
			
		}
	}
	
	void OnGUI() {
		// Need to display buttons and play indicators
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
	private bool noTracksRunning() {
		foreach (Track t in tracks) {
			if (t.state != TrackState.OFF) {
				return true;
			}
		}
		return false;
	}
}
