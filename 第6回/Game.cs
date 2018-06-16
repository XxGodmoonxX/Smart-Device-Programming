
using Sequence = System.Collections.IEnumerator;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
	float player_x = 360;
	float player_y = 640;
	float player_speed = 20.0f;

	const int BLOCK_NUM = 10;
	int[] block_x = new int[BLOCK_NUM];
	int[] block_y = new int[BLOCK_NUM];
	bool[] block_alive_flag = new bool[BLOCK_NUM];
	int time = 0;
	int next_block_num = 0;
	bool isComplete = false;

	/// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.SetResolution(720, 1280);

		for(int i = 0 ; i < BLOCK_NUM ; i++ )
		{
			block_x[i] = gc.Random(0, 720 - 40);
			block_y[i] = gc.Random(0, 1280 - 40);
			block_alive_flag [i] = true;
		}
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
		player_x -= gc.AccelerationLastX * player_speed;
		player_y += gc.AccelerationLastY * player_speed;

		if(isComplete == false) {
			time++;
		}
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        // 画面を白で塗りつぶします
        gc.ClearScreen();

        // 黒の文字を描画します
        gc.SetColor(0, 0, 0);
        gc.SetFontSize(36);

		gc.DrawString("AcceX:"+gc.AccelerationLastX,0,0);
		gc.DrawString("AcceY:"+gc.AccelerationLastY,0,40);
		gc.DrawString("AcceZ:"+gc.AccelerationLastZ,0,80);

		gc.DrawImage (1, (int)player_x, (int)player_y);
    }
}
