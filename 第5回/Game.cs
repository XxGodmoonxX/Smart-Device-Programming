
using Sequence = System.Collections.IEnumerator;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
    // 変数の宣言
//    int sec = 0;

	int time = 600;
	int score = 0;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.SetResolution(720, 1280);
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
		time = time - 1;
		for (int i = 0; i < gc.PointerCount; i++) {
			if (gc.GetPointerFrameCount (i) == 1) {
				if (time >= 0) {
					score = score + 1;
				}
			}
		}

		if (gc.GetPointerDuration (0) >= 2.0f) {
			time = 600;
			score = 0;
		}
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        // 画面を白で塗りつぶします
        gc.ClearScreen();

        // 0番の画像を描画します
        gc.DrawImage(0, 0, 0);

        // 黒の文字を描画します
        gc.SetColor(0, 0, 0);
        gc.SetFontSize(36);
		if (time >= 0) {
			gc.DrawString ("time:" + time, 60, 160);
		} else {
			gc.DrawString ("finished!!", 60, 160);
		}
		gc.DrawString ("Score:" + score, 60, 200);
    }
}
