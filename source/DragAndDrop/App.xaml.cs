using System.Windows;
using Prism.Ioc;
using Prism.Unity;

namespace DragAndDrop
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class Application : PrismApplication
    {
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
        }
    }
}
