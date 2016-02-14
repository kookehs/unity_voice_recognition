using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;

namespace Completed
{
    public class SpeechManager : MonoBehaviour {
        private string _command;
        private DateTime current_command_time;
        private DateTime last_command_time;
        private Player player;
        private Process _speech;
        private string working_directory;

        public string command {
            get {return this._command;}
            set {this._command = value;}
        }

        public Process speech {
            get {return this._speech;}
            set {this._speech = value;}
        }

        private void
        Awake() {
            current_command_time = DateTime.Now;
            last_command_time = DateTime.Now;
            working_directory = Directory.GetCurrentDirectory();

            if (Process.GetProcessesByName("speech_recognition").Length == 0) {
                _speech = new Process();
                _speech.StartInfo.CreateNoWindow = true;
                _speech.StartInfo.FileName = working_directory + "/SpeechRecognition/bin/speech_recognition.exe";
                _speech.StartInfo.RedirectStandardOutput = true;
                _speech.StartInfo.UseShellExecute = false;
                _speech.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                _speech.OutputDataReceived += (sender, args) => {
                        if (args.Data.Contains("Voce") == false) {
                                _command = args.Data;
                                _command = _command.Replace("\n", "");
                                _command = _command.Replace("\r", "");
                                current_command_time = DateTime.Now;
                        }
                };

                _speech.Start();
                _speech.BeginOutputReadLine();
                UnityEngine.Debug.Log("Starting speech recognition");
            }

            player = GameObject.Find("Player").GetComponent<Player>();
        }

        private void
        Execute() {
            UnityEngine.Debug.Log(_command);

            if (_command == "up") {
                player.AttemptMove(0, 1);
            } else if (_command == "down") {
                player.AttemptMove(0, -1);
            } else if (_command == "left") {
                player.AttemptMove(-1, 0);
            } else if (_command == "right") {
                player.AttemptMove(1, 0);
            } else {
                UnityEngine.Debug.Log("Unknown command: " + _command);
            }
        }

        private void
        OnDestroy() {
            _speech.CancelOutputRead();
            _speech.Kill();
        }

        private void
        Update() {
            if (current_command_time != last_command_time) {
                Execute();
                last_command_time = current_command_time;
            }
        }
    }
}
