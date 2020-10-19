using System;
using Microsoft.Extensions.DependencyInjection;

namespace TweetCountChecker
{
    /// <summary>
    /// DI コンテナとして使用するサービス プロバイダーを生成し、管理します。
    /// </summary>
    public static class ServiceProviderManager
    {
        /// <summary>既定のサービス プロバイダー</summary>
        private static IServiceProvider? defaultServiceProvider;

        /// <summary>
        /// 既定のサービス プロバイダーを取得します。初回呼び出し時には生成を行います。
        /// </summary>
        /// <returns>既定のサービス プロバイダー。</returns>
        public static IServiceProvider GetDefaultServiceProvider() =>
            defaultServiceProvider ??= CreateDefaultServiceProvider();

        /// <summary>
        /// 既定のサービス プロバイダーを生成します。
        /// </summary>
        /// <returns>既定のサービス プロバイダー。</returns>
        private static IServiceProvider CreateDefaultServiceProvider()
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.Configure(services);

            return services.BuildServiceProvider();
        }
    }
}
