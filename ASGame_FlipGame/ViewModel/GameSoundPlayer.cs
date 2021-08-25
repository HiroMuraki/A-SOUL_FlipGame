using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace ASGame_FlipGame.ViewModel {
    public class GameSoundPlayer {
        private static GameSoundPlayer _singletonObject;
        private static readonly object _singletonLocker = new object();
        private readonly Random _rnd;
        private readonly List<Uri> _flipFXSound; // 点击音效
        private readonly List<Uri> _gameMusic; // 背景音乐
        private readonly MediaPlayer _flipSoundPlayer; // 点击技能音播放器
        private readonly MediaPlayer _musicSoundPlayer; // 背景音乐播放器
        private string _gameSoundDirectory;

        public string GameSoundDirectory {
            get {
                return _gameSoundDirectory;
            }
            set {
                _gameSoundDirectory = value;
            }
        }
        private GameSoundPlayer() {
            _rnd = new Random();
            _gameSoundDirectory = "ASSounds";

            // 音效播放器初始化
            _flipSoundPlayer = new MediaPlayer();
            _musicSoundPlayer = new MediaPlayer();
            _musicSoundPlayer.MediaEnded += MusicSoundPlayer_MediaEnded; // 背景音乐重复播放

            // 音效列表初始化
            _flipFXSound = new List<Uri>();
            _gameMusic = new List<Uri>();
            //_avaSkillSounds = new List<Uri>();
            //_bellaSkillSounds = new List<Uri>();
            //_carolSkillSounds = new List<Uri>();
            //_dianaSkillSounds = new List<Uri>();
            //_eileenSkillSounds = new List<Uri>();
        }
        public static GameSoundPlayer GetInstance() {
            if (_singletonObject == null) {
                lock (_singletonLocker) {
                    if (_singletonObject == null) {
                        _singletonObject = new GameSoundPlayer();
                    }
                }
            }
            return _singletonObject;
        }

        public void LoadSounds() {
            if (!Directory.Exists(_gameSoundDirectory)) {
                return;
            }
            foreach (var file in Directory.GetFiles(_gameSoundDirectory)) {
                string fileName = Path.GetFileName(file).ToUpper();
                Uri soundUri = new Uri($@"{_gameSoundDirectory}\{fileName}", UriKind.Relative);
                // 跳过非MP3
                if (!(Path.GetExtension(fileName) == ".MP3")) {
                    continue;
                }
                if (fileName.StartsWith("FLIP")) {
                    _flipFXSound.Add(soundUri);
                }
                else if (fileName.StartsWith("MUSIC")) {
                    _gameMusic.Add(soundUri);
                }
                //else if (fileName.StartsWith("COMPLETED")) {
                //    _flagFXSound.Add(soundUri);
                //}
                //else if (fileName.StartsWith("#AVA")) {
                //    _avaSkillSounds.Add(soundUri);
                //}
                //else if (fileName.StartsWith("#BELLA")) {
                //    _bellaSkillSounds.Add(soundUri);
                //}
                //else if (fileName.StartsWith("#CAROL")) {
                //    _carolSkillSounds.Add(soundUri);
                //}
                //else if (fileName.StartsWith("#DIANA")) {
                //    _dianaSkillSounds.Add(soundUri);
                //}
                //else if (fileName.StartsWith("#EILEEN")) {
                //    _eileenSkillSounds.Add(soundUri);
                //}
            }
        }
        public void PlayFlipFXSound() {
            RandomPlay(_flipSoundPlayer, _flipFXSound);
        }
        public void PlayMusic() {
            RandomPlay(_musicSoundPlayer, _gameMusic);
        }

        private void RandomPlay(MediaPlayer player, List<Uri> resources) {
            // 音频列表资源为0则跳过播放
            if (resources.Count <= 0) {
                return;
            }
            // 否则随机打开一个并播放
            player.Open(resources[_rnd.Next(0, resources.Count)]);
            player.Play();
        }
        private void MusicSoundPlayer_MediaEnded(object sender, EventArgs e) {
            PlayMusic();
        }

        //private readonly List<Uri> _avaSkillSounds; // 向晚技能音效
        //private readonly List<Uri> _bellaSkillSounds; // 贝拉技能音效
        //private readonly List<Uri> _carolSkillSounds; // 珈乐技能音效
        //private readonly List<Uri> _dianaSkillSounds; // 嘉然技能音效
        //private readonly List<Uri> _eileenSkillSounds; // 乃琳技能音效
        //private readonly MediaPlayer _gameCompletedSoundPlayer; // 结算音播放器
        //private readonly MediaPlayer _skillActivedSoundPlayer; // 技能启动音播放器
        //public void PlayGameCompletedSound() {
        //    RandomPlay(_gameCompletedSoundPlayer, _flagFXSound);
        //}
        //public void PlaySkillActivedSound(LLKSkill skill) {
        //    switch (skill) {
        //        case LLKSkill.None:
        //            break;
        //        case LLKSkill.AvaPower:
        //            RandomPlay(_skillActivedSoundPlayer, _avaSkillSounds);
        //            break;
        //        case LLKSkill.BellaPower:
        //            RandomPlay(_skillActivedSoundPlayer, _bellaSkillSounds);
        //            break;
        //        case LLKSkill.CarolPower:
        //            RandomPlay(_skillActivedSoundPlayer, _carolSkillSounds);
        //            break;
        //        case LLKSkill.DianaPower:
        //            RandomPlay(_skillActivedSoundPlayer, _dianaSkillSounds);
        //            break;
        //        case LLKSkill.EileenPower:
        //            RandomPlay(_skillActivedSoundPlayer, _eileenSkillSounds);
        //            break;
        //        default:
        //            break;
        //    }
        //    _skillActivedSoundPlayer.Play();
        //}
    }
}
