using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TweetCountChecker.Common
{
    /// <summary>
    /// 識別子と名前を持つ列挙型クラスを提供します。
    /// </summary>
    public abstract class Enumeration
    {
        /// <summary>
        /// インスタンスを一意に特定する為の識別子を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// インスタンスの名前を取得します。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// <see cref="Enumeration"/> のコンストラクタ。
        /// </summary>
        /// <param name="id">インスタンスを一意に特定する為の識別子。</param>
        /// <param name="name">インスタンスの名前。</param>
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// この列挙型クラスに含まれている定数の配列を取得します。
        /// </summary>
        /// <typeparam name="T">列挙型クラス。</typeparam>
        /// <returns><typeparamref name="T"/> に含まれている定数を格納する配列。</returns>
        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        /// <summary>
        /// このインスタンスをそれと等価な文字列の値に変換します。
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

        #region インスタンスの等価性を識別子の値で判断する為の実装

        /// <summary>
        /// このインスタンスと、指定したオブジェクトが同一かどうかを判断します。
        /// </summary>
        /// <param name="obj">このインスタンスと比較するオブジェクト。</param>
        /// <returns>指定したオブジェクトがこのインスタンスと等しい場合は true。それ以外の場合は false。</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }

            if (!(obj is Enumeration otherValue)) { return false; }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);
            return typeMatches && valueMatches;
        }

        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}

// [参考記事] enum 型の代わりに Enumeration クラスを使用する
// https://docs.microsoft.com/ja-jp/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
