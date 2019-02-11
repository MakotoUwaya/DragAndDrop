using Prism.Mvvm;
using Prism.Commands;
using DragAndDrop.Model;

namespace DragAndDrop.ViewModels
{
    /// <summary>
    /// 設定ダイアログ ViewModel
    /// </summary>
    public class SettingDialogViewModel : BindableBase
    {
        private readonly Settings settings;

        private bool isWindowClosed;
        /// <summary>
        /// ウィンドウを閉じる
        /// </summary>
        public bool IsWindowClosed
        {
            get => this.isWindowClosed;
            set => this.SetProperty(ref this.isWindowClosed, value);
        }

        /// <summary>
        /// 画像判定機のURLエンドポイント
        /// </summary>
        public string ImageDeterminationUrl
        {
            get => this.settings.ImageDeterminationUrl;
            set
            {
                this.settings.ImageDeterminationUrl = value;
                this.RaisePropertyChanged(nameof(this.ImageDeterminationUrl));
            }
        }

        /// <summary>
        /// 判定機の種類
        /// </summary>
        public DeterminatorType Determinator
        {
            get => this.settings.Determinator;
            set
            {
                this.settings.Determinator = value;
                this.RaisePropertyChanged(nameof(this.Determinator));
                this.RaisePropertyChanged(nameof(this.IsUpdateDeterminator));
            }
        }

        /// <summary>
        /// When unmatch determinator settings is true
        /// </summary>
        public bool IsUpdateDeterminator => this.settings.IsUpdateDeterminator;

        /// <summary>
        /// 設定データの保存処理
        /// </summary>
        public DelegateCommand SettingSaveCommand { get; set; }

        /// <summary>
        /// 設定データの適用処理
        /// </summary>
        public DelegateCommand SettingAcceptCommand { get; set; }

        /// <summary>
        /// 設定キャンセル処理
        /// </summary>
        public DelegateCommand SettingCancelCommand { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SettingDialogViewModel()
        {
            this.settings = new Settings();
            this.RaisePropertyChanged();

            this.SettingSaveCommand = new DelegateCommand(() => 
            {
                this.settings.Save();
                this.IsWindowClosed = true;
            });

            this.SettingAcceptCommand = new DelegateCommand(() => 
            {
                this.settings.Save();
            });

            this.SettingCancelCommand = new DelegateCommand(() =>
            {
                this.IsWindowClosed = true;
            });
        }
    }
}
