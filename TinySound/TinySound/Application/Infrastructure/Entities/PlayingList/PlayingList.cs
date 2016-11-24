using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using NAudio.Wave;
using TinySound.Application.Infrastructure.Utils.AudioGenerator;
using TinySound.Application.Infrastructure.Utils.FileHelper;
using TinySound.Application.Models.Storage;
using PlaybackState = CSCore.SoundOut.PlaybackState;
using Timer = System.Timers.Timer;

namespace TinySound.Application.Infrastructure.Entities.PlayingList
{
	public class PlayingList
	{
		public Int32 AudioIndex;
		
		public String Title { get; set; }
		public List<Audio.Audio> Audios { get; set; }
		public ISoundOut SoundOut { get; set; }
		public Timer Timer { get; set; }
		public Thread ControllingThread { get; set; }
		public bool NeedToStopAudio { get; set; }
		public bool NeedToStartAudio { get; set; }
		public bool NeedToDeleteAudio { get; set; }
	

		public PlayingList(PlayList.PlayList PlayList)
		{
			this.NeedToStopAudio = false;
			this.NeedToStartAudio = false;
			this.NeedToDeleteAudio = false;
			this.AudioIndex = 0;
			this.Title = PlayList.PlayListInfo.Title;
			this.Audios = new List<Audio.Audio>(PlayList.Audios.Select(x => x.Value).ToArray());
			

			this.SoundOut = AudioGenerator.GetSoundOut();
			this.SoundOut.Stopped += SoundOut_Stopped;
			ControllingThread = new Thread(ControllingThreadEntryPoint);
			ControllingThread.Start();
			Start();
			







		}

		private void ControllingThreadEntryPoint()
		{
			while (true)
			{
				Thread.Sleep(100);
				if (this.NeedToStartAudio)
				{
					this.NeedToStartAudio = false;
					this.Start();
				}
				if (this.NeedToStopAudio == true)
				{
					this.SoundOut.Stop();  
					this.NeedToStopAudio = false;
				}
				if (this.NeedToDeleteAudio)
				{ 
					this.DeleteFromStorage();
					this.NeedToDeleteAudio = false;
					this.ControllingThread.Abort();

				}
			}
		}


		private void SoundOut_Stopped(object sender, PlaybackStoppedEventArgs e)
		{
			if (AudioIndex < Audios.Count - 1)
			{
				this.AudioIndex++;
				this.NeedToStartAudio = true;
			}
			else
			{
				this.NeedToStopAudio = true;
				this.NeedToDeleteAudio = true;

			}
		}

		public void DeleteFromStorage()
		{
			Storage.PlayingLists.Remove(this);

		}
		public void Delete()
		{
			this.AudioIndex = this.Audios.Count;
			this.SoundOut.Stop();
		}
		public void NextAudio()
		{
			this.SoundOut.Stop();
		}
		public void Start()
		{
			
			if (Audios.Count >= 1)
			{

				PutMusicToPlayerWithAudioIndex();
				Console.WriteLine("instartmethod");
				SoundOut.Play();
			}
			
		}

		public void PutMusicToPlayerWithAudioIndex()
		{
			this.SoundOut.Initialize(GetSoundSource(this.Audios[this.AudioIndex].FileHelper.FilePath));
			
		}

		public void Pause()
		{
			if(this.SoundOut.PlaybackState == PlaybackState.Playing)
				this.SoundOut.Pause();
		}

		public void Resume()
		{
			if(this.SoundOut.PlaybackState == PlaybackState.Paused)
				this.SoundOut.Resume();
		}

		public void TriggerResumePause()
		{
			if (this.SoundOut.PlaybackState == PlaybackState.Playing)
				Pause();
			else if(this.SoundOut.PlaybackState == PlaybackState.Paused)
				Resume();
		}

		private static IWaveSource GetSoundSource(String filename)
		{
			//return any source ... in this example, we'll just play a mp3 file
			return CodecFactory.Instance.GetCodec(Path.Combine(FileHelper.FolderPath, filename));
		}
	}
}
