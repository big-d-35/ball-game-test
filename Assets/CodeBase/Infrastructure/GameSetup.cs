using CodeBase.Infrastructure.Data;
using Infrastructure;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameSetup
    {
        private readonly StaticData _staticData;
        private readonly SceneHandler _sceneHandler;

        public GameSetup(StaticData staticData, SceneHandler sceneHandler)
        {
            _staticData = staticData;
            _sceneHandler = sceneHandler;
        }

        public void Initialize()
        {
            Debug.Log($"Loading Start Scene: {_staticData.StartScene}");
            _sceneHandler.LoadLevel(_staticData.StartScene);
        }
    }

}
