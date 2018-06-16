
using Sequence = System.Collections.IEnumerator;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
	// 変数の宣言
	int ball_x;
	int ball_y;
	int ball_speed_x;
	int ball_speed_y;

	int player_x;
	int player_y;
	int player_w;
	int player_h;

	//ブロック
	const int BLOCK_NUM = 50;
	int[] block_x = new int [BLOCK_NUM];
	int[] block_y = new int [BLOCK_NUM];
	bool[] block_alive_flag = new bool [BLOCK_NUM];
	int block_w = 64;
	int block_h = 20;
	int time;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public override void InitGame()
	{
		// キャンバスの大きさを設定します
		gc.SetResolution(640, 480);

		//ボールの位置、速さの初期値
		ball_x = 0;
		ball_y = 0;
		ball_speed_x = 3;
		ball_speed_y = 3;

		//ラケットの位置、大きさの初期値
		player_x = 270;
		player_y = 460;
		player_w = 100;
		player_h = 20;

		//ブロック
		for (int i = 0; i < BLOCK_NUM; i++) {
			block_x [i] = (i % 10) * block_w;
			block_y [i] = (i / 10) * block_h;
			block_alive_flag [i] = true;
		}
		time = 0;

	}

	/// <summary>
	/// 動きなどの更新処理
	/// </summary>
	public override void UpdateGame()
	{
		//ボールの位置をスピードに合わせて変化
		ball_x = ball_x + ball_speed_x;
		ball_y = ball_y + ball_speed_y;

		//ボールが画面外出たら跳ね返る
		if (ball_x < 0) {
			ball_x = 0;
			ball_speed_x = -ball_speed_x;
		}
		if (ball_y < 0) {
			ball_y = 0;
			ball_speed_y = -ball_speed_y;
		}
		if (ball_x > 616) {
			ball_x = 616;
			ball_speed_x = -ball_speed_x;
		}
//		if (ball_y > 456) {
//			ball_y = 456;
//			ball_speed_y = -ball_speed_y;
//		}

		//画面を1/60秒押してたら ラケットの位置を変化 押したとこがラケットの真ん中に
		if (gc.GetPointerFrameCount(0) > 0) {
			player_x = gc.GetPointerX(0) - player_w / 2;
			//バーが上下にも動くように
			player_y = gc.GetPointerY(0) - player_h / 2;
		}

		//跳ね返りの当たり判定
		//ボールがプレイヤーに当たったら
		if (gc.CheckHitRect(ball_x, ball_y, 24, 24, player_x, player_y, player_w, player_h)) {
			//ボール下向きに動いていたら
			if (ball_speed_y > 0) {
				ball_speed_y = -ball_speed_y;
			}
		}

		//ブロックが全部消えていなければ
		if (countBlock () != 0) {
			time++;
			for (int i = 0; i < BLOCK_NUM; i++) {
				//ボールがブロックに当たったら
				if (gc.CheckHitRect (ball_x, ball_y, 24, 24, block_x [i], block_y [i], block_w, block_h)) {
					block_alive_flag [i] = false;
				}
			}
		}

	}

	/// <summary>
	/// 描画の処理
	/// </summary>
	public override void DrawGame()
	{
		// 画面を白で塗りつぶします
		gc.ClearScreen();

		// 0番の画像を描画します 青空
		gc.DrawImage(0, 0, 0);
		// 1番の画像 ボール
		gc.DrawImage(1, ball_x, ball_y);

		//ラケットの色青
		gc.SetColor(0, 0, 255);
		gc.FillRect(player_x, player_y, player_w, player_h);

		//ブロックを表示
		for (int i = 0; i < BLOCK_NUM; i++) {
			if (block_alive_flag [i] == true) {
				gc.FillRect (block_x[i], block_y[i], block_w, block_h);
			}
		}

		//timeを表示 ブロックが全部消えていれば
		if (countBlock () == 0) {
			gc.DrawString ("Clear!" + time, 0, 300);
		} else {
			gc.DrawString ("Time:" + time, 0, 300);
		}

	}

	int countBlock() {
		int num = 0;
		for (int i = 0; i < BLOCK_NUM; i++) {
			if (block_alive_flag [i]) {
				num++;
			}
		}
		return num;
	}
}
