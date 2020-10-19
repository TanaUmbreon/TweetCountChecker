namespace TweetCountChecker.Commands
{
    /// <summary>
    /// コマンドの基底クラスを表します。
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// コマンドが起動される際に呼び出すメソッドを定義します。
        /// </summary>
        void Execute();
    }
}
