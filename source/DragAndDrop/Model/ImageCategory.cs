using System.Collections.Generic;

namespace DragAndDrop.Model
{
    /// <summary>
    /// 画像カテゴリ
    /// </summary>
    public class ImageCategory
    {
        /// <summary>
        /// 画像カテゴリの一覧
        /// </summary>
        public static IEnumerable<string> List { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static ImageCategory()
        {
            List = new[]
            {
                "エントランス",
                "キッチン",
                "トイレ",
                "ベッドルーム",
                "ベランダ",
                "ポスト",
                "リビング",
                "ロビー",
                "収納",
                "外観",
                "庭",
                "洗面所",
                "玄関",
                "眺望",
                "間取り",
                "風呂画像",
                "駐車場",
            };
        }
    }
}
