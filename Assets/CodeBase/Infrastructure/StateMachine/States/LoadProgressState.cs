using CodeBase.Data.Progress;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadProgressState :  IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        [Inject]
        public LoadProgressState(IGameStateMachine stateMachine,IPersistentProgressService progressService,ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState,string>(_progressService.Progress.WorldData.PositionOnLevel.LevelName);
        }

        public void Exit()
        {
          
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress =
                _saveLoadService.LoadProgress() ??  NewProgress();

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress("Main");

            progress.HeroState.MaxHp = 100f;
            progress.HeroState.ResetHP();
            progress.HeroStats.Damage = 25f;
            progress.HeroStats.AttackRadius = .7f;
            return progress;
        }
    }
}