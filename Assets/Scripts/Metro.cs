﻿using UnityEngine;
using System.Collections;

public class Metro : MonoBehaviour {

	public double bpm;
	public int timeTop;
	public int timeBot;
	private int currentBeat = 0;
	private int prevBeat = 0;
	private double startTime = 0.0F;
	private bool running = false;
	private bool isBeatOne = false;

	public Metro(double bmp, int timeTop, int timeBot) {
		this.bpm = bmp;
		this.timeTop = timeTop;
		this.timeBot = timeBot;
	}

	public Metro() {
		this.bpm = 120.0F;
		this.timeTop = 4;
		this.timeBot = 4;
	}

	void Start() {
		running = true; // This line is temporary, for testing purposes only
	}

	/* On update(), update the current beat counter and indicate when the metronome
	 * has reached a new measure.
	 */
	void Update() {
		if (!running) {
			return;
		}
		// Seconds per beat = (seconds per minute) / bpm
		// beats elapsed = (seconds elapsed - start time / seconds per beat)
		// current beat = beats elapsed % timeTop
		// Note that this equation only works when a quarter note has the beat (timeBot == 4)
		prevBeat = currentBeat;
		currentBeat = (int)((AudioSettings.dspTime - startTime) / (60.0F / bpm)) % timeTop;
		isBeatOne = (prevBeat == timeTop - 1 && currentBeat == 0);
		// Uncomment below this line to print when reaching a new measure
		//if (isBeatOne) {
		//	print ("Metro:\tNew Measure");
		//}
	}

	// Starts the metronome
	public void go() {
		if (timeBot != 4) {
			Debug.LogWarning("Warning: Metro currently does not support time signatures other than X/4");
		}
		this.running = true;
		startTime = AudioSettings.dspTime;
	}

	// Stops the metronome
	public void stop() {
		this.running = false;
	}

	// Getter function to see when metronome has reached a new measure
	public bool isNewMeasure() {
		return isBeatOne;
	}

}
