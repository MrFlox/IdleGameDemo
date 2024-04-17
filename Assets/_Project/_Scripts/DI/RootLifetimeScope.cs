using Modules.Entities;
using Modules.Entities.Collector.ScriptableObject;
using Modules.Entities.Collector.Services;
using Modules.Entities.Generator.ScriptableObjects;
using Modules.Entities.Generator.Services;
using Modules.Entities.ScriptableObjects;
using Modules.Infrastructure.Services;
using Modules.ResourceSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Modules.Entities.Converter.ScriptableObjects;
using Modules.Entities.Deliver;
using Modules.Entities.Services;
using Modules.UI;
using Modules.UI.Windows;
using Modules.UI.Windows.UnitShop;
using UnityEngine.Serialization;

namespace DI
{
    public class RootLifetimeScope : LifetimeScope
    {
        [FormerlySerializedAs("Settings")] [SerializeField]
        private CollectorSettingsSo _settings;

        [FormerlySerializedAs("GetSettings")] [SerializeField]
        private GeneratorsSettingsSo _getSettings;

        [FormerlySerializedAs("FactorySettings")] [SerializeField]
        private FactorySettingsSo _factorySettings;

        [SerializeField] private DeliverModelSettingsSo _deliverModelSettings;
        [SerializeField] private ConverterSettingsSo _converterSettingsSo;

        private SignalInstaller _signalInstaller = new();
        protected override void Configure(IContainerBuilder builder)
        {
            InstallServices(builder);
            InstallScriptableObjects(builder);
            InstallEntryPoint(builder);
            InstallMessagePipe(builder);
            InstallEntryPoints(builder);
        }

        private void InstallMessagePipe(IContainerBuilder builder)
        {
            _signalInstaller.Configure(builder);
        }

        private void InstallEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Game>();
        }

        private void InstallScriptableObjects(IContainerBuilder builder)
        {
            builder.RegisterInstance(_settings).As<CollectorSettingsSo>();
            builder.RegisterInstance(_getSettings).As<GeneratorsSettingsSo>();
            builder.RegisterInstance(_factorySettings).As<FactorySettingsSo>();
            builder.RegisterInstance(_deliverModelSettings).As<DeliverModelSettingsSo>();
            builder.RegisterInstance(_converterSettingsSo).As<ConverterSettingsSo>();
        }

        private void InstallServices(IContainerBuilder builder)
        {
            builder.Register<UnitShopPresenter>(Lifetime.Singleton).As<IUnitShopPresenter>();
            builder.Register<WindowsFactory>(Lifetime.Singleton).As<IWindowsFactory>();
            builder.Register<AssetLoaderService>(Lifetime.Singleton);
            builder.Register<IResourcesService, ResourcesService>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IFactory, Factory>(Lifetime.Singleton);
            builder.Register<IAbstractFactory<FlyingText>, AbstractFactory<FlyingText>>(Lifetime.Singleton);
            builder.Register<IGeneratorsService, GeneratorsService>(Lifetime.Singleton);
            builder.Register<ICollectorsService, CollectorsService>(Lifetime.Singleton);
        }

        private static void InstallEntryPoints(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<FlyingLabelsManager>();
            builder.RegisterEntryPoint<WindowManager>();
        }
    }
}