using Ability.Service;
using Wallet.Service;

namespace Core
{
    public class MainSceneInstaller : Zenject.MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainSceneInstaller>().FromInstance(this).AsSingle();
            Container.Bind<WalletService>().AsSingle();
            Container.Bind<AbilityService>().AsSingle();
            Container.Bind<AbilitySelectService>().AsSingle();
        }
    }
}
