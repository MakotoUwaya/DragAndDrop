using System;
using System.Windows.Input;

namespace DragAndDrop
{
    /// <summary>
    /// ICommand を実装するアクションクラス ジェネリック対応
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute">実行するアクション</param>
        /// <param name="canExecute">実行可否判定</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute">実行するアクション</param>
        public RelayCommand(Action execute) : this(execute, () => true)
        {
        }

        /// <summary>
        /// 処理実行 明示的なインターフェース実装
        /// </summary>
        /// <param name="parameter"></param>
        void ICommand.Execute(object parameter)
        {
            this.execute();
        }

        /// <summary>
        /// 実行可否判定 明示的なインターフェース実装
        /// </summary>
        /// <param name="parameter">実行に必要なパラメータを指定</param>
        /// <returns>実行可否判定結果</returns>
        bool ICommand.CanExecute(object parameter)
        {
            return this.canExecute();
        }

        /// <summary>
        /// 実行可能状況変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


    }
}
