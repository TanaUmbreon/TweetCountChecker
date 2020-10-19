using System;
using Microsoft.Extensions.DependencyInjection;
using TweetCountChecker.Commands;
using TweetCountChecker.Common;

namespace TweetCountChecker
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                IServiceProvider services = ServiceProviderManager.GetDefaultServiceProvider();
                var command = services.GetService<CountCommand>();
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] 問題が発生したためアプリケーションは停止しました。\n{ex.Message}\n\n[スタック トレース]\n{ex.StackTrace}");
                ExitCode.Stopped.Apply();
                throw;
            }
        }
    }

    #region 終了コードの定義

    /// <summary>
    /// 終了コードを表します。
    /// </summary>
    internal class ExitCode : Enumeration
    {
        /// <summary>正常終了</summary>
        public static readonly ExitCode Completed = new ExitCode(id: 0, name: "正常終了");
        /// <summary>異常終了</summary>
        public static readonly ExitCode Stopped = new ExitCode(id: 1, name: "異常終了");

        #region 列挙型クラスのインスタンス メンバ

        /// <summary>
        /// <see cref="ExitCode"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="id">終了コードを一意に特定する為の識別子。終了コードそのもの。</param>
        /// <param name="name">終了コードの説明。</param>
        private ExitCode(int id, string name) : base(id, name) { }

        /// <summary>
        /// このインスタンスの終了コードをアプリケーションの戻り値として適用します。
        /// </summary>
        public void Apply() => Environment.ExitCode = Id;

        #endregion
    }

    #endregion
}
