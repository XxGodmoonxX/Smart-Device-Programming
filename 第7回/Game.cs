﻿
using Sequence = System.Collections.IEnumerator;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
    // 変数の宣言
	int score = 0;
	string pname = "t15986mw";
	string url = "";
	string str = "";

	const int BOX_NUM = 10;
	int[] box_x = new int[BOX_NUM];
	int[] box_y = new int[BOX_NUM];
	int[] box_speed = new int[BOX_NUM];
	int box_w = 24;
	int box_h = 24;

	int player_x = 304;
	int player_y = 400;
	int player_dir = 1;
	int player_speed = 5;

//	int score = 0;
	int count = 0;
//	string str = "";

	int gameState = 0;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.SetResolution(640, 480);

//		score = gc.Random(0, 100);
		resetValue();
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
//		if(gc.GetPointerFrameCount(0) ==1 ){
//			url = "http://web.sfc.keio.ac.jp/~wadari/sdp/k07_web/score.cgi?score="
//				+ score + "&name=" + pname;
//			gc.GetOnlineTextAsync(url, out str);
//		}

		if (gameState == 0) {
			if (gc.GetPointerFrameCount (0) == 1) {
				gameState = 1;
			}

		} else if (gameState == 1) {
			count++;
			score = count / 60;
			box_w = 24 + count / 300;
			box_h = 24 + count / 300;

			if(gc.GetPointerFrameCount(0) == 1 ){
				player_dir = -player_dir;
			}

			player_x += player_dir * player_speed;

			for(int i = 0 ; i < BOX_NUM ; i++){
				//箱を動かす処理
				box_y[i] = box_y[i] + box_speed[i];

				if(box_y[i]> 480){
					box_x[i] = gc.Random(0,616);
					box_y[i] = -gc.Random(100,480);
					box_speed[i] = gc.Random(3,6);
				}

				//playerと箱の当り判定
				if (gc.CheckHitRect(player_x, player_y, 32, 32,
					box_x[i], box_y[i], box_w, box_h)) {
					//当たった時の処理
					//gameStateを２にする
					gameState = 2;
				}
			}

			//playerが画面左右に着いてもgameOverになるように
			if(player_x < 0 || player_x > 608){
				//gameStateを２にする
				gameState = 2;
			}

		} else if (gameState == 2) {
			if(gc.GetPointerFrameCount(0) == 1 ){
				url = "http://web.sfc.keio.ac.jp/~wadari/sdp/k07_web/score.cgi?score="
					+ score + "&name=" + pname;
				gc.GetOnlineTextAsync(url, out str);
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

//		gc.DrawOnlineImage("http://web.sfc.keio.ac.jp/~wadari/sdp/k07_web/Player.png",320,240);

		string str2 = "";

		if (gameState == 0) {
			gc.DrawString ("Title", 0, 400);
		} else if (gameState == 1) {
			//ゲーム画面の表示
			gc.DrawOnlineImage("http://web.sfc.keio.ac.jp/~wadari/sdp/k07_web/Player.png",
				player_x, player_y);

			for (int i = 0; i < BOX_NUM; i++) {
				gc.FillRect (box_x [i], box_y [i], box_w, box_h);
			}
		} else if (gameState == 2) {
			gc.DrawString ("Game Over", 0, 200);
			gc.DrawString(str, 0, 400);
		}
	}

	void resetValue() {
		for (int i = 0; i < BOX_NUM; i++) {
			box_x [i] = gc.Random (0, 616);
			box_y [i] = -gc.Random (100, 480);
			box_speed [i] = gc.Random (3, 6);
		}
	}
}
