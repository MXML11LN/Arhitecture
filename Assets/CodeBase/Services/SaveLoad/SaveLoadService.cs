using CodeBase.Data.Progress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
   public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        public SaveLoadService()
        {
        }

        public void SaveProgress()
        {
            
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}