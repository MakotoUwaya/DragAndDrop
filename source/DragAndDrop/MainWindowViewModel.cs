using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Prism.Mvvm;
using Prism.Commands;
using DragAndDrop.Model;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Linq;

namespace DragAndDrop
{
    /// <summary>
    /// メインウィンドウ
    /// </summary>
    public class MainWindowViewModel : BindableBase
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
        /// 画像イメージのコレクション
        /// </summary>
        public ObservableCollection<ImageCard> ImageCards { get; }

        /// <summary>
        /// 設定コマンド
        /// </summary>
        public DelegateCommand SettingCommand { get; set; }

        private bool isBusy;
        /// <summary>
        /// 待機状態
        /// </summary>
        public bool IsBusy
        {
            get => this.isBusy;
            set => this.SetProperty(ref this.isBusy, value);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.StartTimer();
            this.IsBusy = true;
            this.ImageCards = new ObservableCollection<ImageCard>();
            BindingOperations.EnableCollectionSynchronization(this.ImageCards, new object());

            // 設定画面を開く処理
            this.SettingCommand = new DelegateCommand(() =>
            {
                var dialog = new Functions.Setting.SettingDialog
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
        /// 画像カードを追加する
        /// </summary>
        /// <param name="imageFilePath">画像ファイルパス</param>
        public async Task AddImageCards(string[] imageFilePath)
        {
            this.IsBusy = true;
            foreach (var file in imageFilePath)
            {
                await Task.Run(() =>
                {
                    var imageCard = new ImageCard(file);
                    this.ImageCards.Add(imageCard);
                });
            }            
         
            this.IsBusy = false;
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
