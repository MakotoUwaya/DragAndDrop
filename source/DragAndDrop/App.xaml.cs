using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using DragAndDrop.Model;
using DragAndDrop.Views;
using Interfaces;
using aws = AWSDriver;
using google = GoogleVisionDriver;

namespace DragAndDrop
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class Application : PrismApplication
    {
        /// <summary>
        /// Main content region
        /// </summary>
        public static readonly string MainContent = "MainContent";

        /// <summary>
        /// Startup window
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// Resist type to container
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            switch ((DeterminatorType)DragAndDrop.Properties.Settings.Default.Determinator)
            {
                case DeterminatorType.AWS:
                    containerRegistry.Register<IDeterminator, aws.Determinator>();
                    break;
                case DeterminatorType.Google:
                    containerRegistry.Register<IDeterminator, google.Determinator>();
                    break;
            }
        }
    }
}
