using System;
using System.Reflection;
using System.Windows.Threading;
using Prism.Commands;
using Prism.Mvvm;
using DragAndDrop.Views;

namespace DragAndDrop.ViewModels
{
    /// <summary>
    /// メインウィンドウ
    /// </summary>
    internal class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 現在時刻更新用のタイマー
        /// </summary>
        DispatcherTimer dispatcherTimer;

        private string statusTime;
        /// <summary>
        /// 現在時刻
        /// </summary>
        public string StatusTime
        {
            get => this.statusTime;
            set => this.SetProperty(ref this.statusTime, value);
        }

        /// <summary>
        /// クライアントバージョン
        /// </summary>
        public string ClientVersion
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var asmName = assembly.GetName();
                var version = asmName.Version;
                return $"Ver {version.Major}.{version.Minor}.{version.Build}";
            }
        }

        /// <summary>
        /// 設定コマンド
        /// </summary>
        public DelegateCommand SettingCommand { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.StartTimer();

            // 設定画面を開く処理
            this.SettingCommand = new DelegateCommand(() =>
            {
                var dialog = new SettingDialog
                {
                    Owner = System.Windows.Application.Current.MainWindow
                };
                dialog.ShowDialog();
            });
        }

        /// <summary>
        /// コマンドの実行可能状態を更新
        /// </summary>
        private void CommandCanExecuteChanged()
        {
            this.SettingCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 現在時刻表示用のタイマーを開始する
        /// </summary>
        private void StartTimer()
        {
            this.dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = new TimeSpan(0, 0, 1)
            };

            this.dispatcherTimer.Tick += (s, e) =>
            {
                this.StatusTime = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}";
            };
            this.dispatcherTimer.Start();
        }
    }
}
