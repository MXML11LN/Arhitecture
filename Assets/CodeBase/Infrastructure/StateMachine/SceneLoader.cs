using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        [Inject]
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string sceneName, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(Load(sceneName, onLoaded));

        private IEnumerator Load(string sceneName, Action onLoaded =null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);
            while (!waitNextScene.isDone)
                yield return null;
            onLoaded?.Invoke();
        }
        
        
    }
}