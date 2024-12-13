using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameInitializer : MonoBehaviour
    {
        [Inject] private GameSetup _gameSetup;

        private void Start() => 
            _gameSetup.Initialize();
    }
}