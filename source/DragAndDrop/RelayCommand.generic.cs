using System;
using System.Windows.Input;

namespace DragAndDrop
{
    /// <summary>
    /// ICommand を実装するアクションクラス ジェネリック対応
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute">実行するアクション</param>
        /// <param name="canExecute">実行可否判定</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute">実行するアクション</param>
        public RelayCommand(Action<T> execute) : this(execute, _ => true)
        {
        }

        /// <summary>
        /// 処理実行
        /// </summary>
        /// <param name="parameter">実行に必要なパラメータを指定</param>
        public void Execute(T parameter)
        {
            this.execute(parameter);
        }

        /// <summary>
        /// 処理実行 明示的なインターフェース実装
        /// <see cref="object"/>型から特定の<typeparamref name="T"/>型に変換する
        /// </summary>
        /// <param name="parameter"></param>
        void ICommand.Execute(object parameter)
        {
            if (parameter == null)
            {
                this.Execute(default(T));
                return;
            }

            this.execute((T)parameter);
        }

        /// <summary>
        /// 実行可否判定
        /// </summary>
        /// <param name="parameter">実行に必要なパラメータを指定</param>
        /// <returns>実行可否判定結果</returns>
        public bool CanExecute(T parameter)
        {
            return this.canExecute(parameter);
        }

        /// <summary>
        /// 実行可否判定 明示的なインターフェース実装
        /// <see cref="object"/>型から特定の<typeparamref name="T"/>型に変換する
        /// </summary>
        /// <param name="parameter">実行に必要なパラメータを指定</param>
        /// <returns>実行可否判定結果</returns>
        bool ICommand.CanExecute(object parameter)
        {
            return parameter == null 
                ? this.CanExecute(default(T)) 
                : this.CanExecute((T)parameter);
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
