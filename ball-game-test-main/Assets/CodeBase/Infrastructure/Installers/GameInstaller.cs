using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Data;
using Infrastructure;
using Zenject;

namespace CodeBase.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StaticData>().FromScriptableObjectResource("StaticData/StaticData").AsSingle();
            Container.Bind<SceneHandler>().AsSingle();
            Container.Bind<GameSetup>().AsSingle();
        }
    }
}
