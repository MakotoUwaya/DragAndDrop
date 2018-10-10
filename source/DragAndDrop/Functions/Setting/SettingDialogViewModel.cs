using Prism.Mvvm;
using Prism.Commands;

namespace DragAndDrop.Functions.Setting
{
    /// <summary>
    /// 設定ダイアログ ViewModel
    /// </summary>
    public class SettingDialogViewModel : BindableBase
    {
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
            this.SettingSaveCommand = new DelegateCommand(() => 
            {
                this.IsWindowClosed = true;
            });

            this.SettingAcceptCommand = new DelegateCommand(() => { });

            this.SettingCancelCommand = new DelegateCommand(() =>
            {
                this.IsWindowClosed = true;
            });
        }
    }
}
