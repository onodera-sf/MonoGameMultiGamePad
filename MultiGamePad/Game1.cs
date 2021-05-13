using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MultiGamePad
{
	/// <summary>
	/// ゲームメインクラス
	/// </summary>
	public class Game1 : Game
	{
    /// <summary>
    /// グラフィックデバイス管理クラス
    /// </summary>
    private readonly GraphicsDeviceManager _graphics = null;

    /// <summary>
    /// スプライトのバッチ化クラス
    /// </summary>
    private SpriteBatch _spriteBatch = null;

    /// <summary>
    /// スプライトでテキストを描画するためのフォント
    /// </summary>
    private SpriteFont _font = null;

    /// <summary>
    /// ゲームパッドの状態(４プレイヤー分)
    /// </summary>
    private GamePadState[] _gamePadStates = new GamePadState[4];


    /// <summary>
    /// GameMain コンストラクタ
    /// </summary>
    public Game1()
    {
      // グラフィックデバイス管理クラスの作成
      _graphics = new GraphicsDeviceManager(this);

      // ゲームコンテンツのルートディレクトリを設定
      Content.RootDirectory = "Content";

      // マウスカーソルを表示
      IsMouseVisible = true;
    }

    /// <summary>
    /// ゲームが始まる前の初期化処理を行うメソッド
    /// グラフィック以外のデータの読み込み、コンポーネントの初期化を行う
    /// </summary>
    protected override void Initialize()
    {
      // TODO: ここに初期化ロジックを書いてください

      // コンポーネントの初期化などを行います
      base.Initialize();
    }

    /// <summary>
    /// ゲームが始まるときに一回だけ呼ばれ
    /// すべてのゲームコンテンツを読み込みます
    /// </summary>
    protected override void LoadContent()
    {
      // テクスチャーを描画するためのスプライトバッチクラスを作成します
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      // フォントをコンテンツパイプラインから読み込む
      _font = Content.Load<SpriteFont>("Font");
    }

    /// <summary>
    /// ゲームが終了するときに一回だけ呼ばれ
    /// すべてのゲームコンテンツをアンロードします
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: ContentManager で管理されていないコンテンツを
      //       ここでアンロードしてください
    }

    /// <summary>
    /// 描画以外のデータ更新等の処理を行うメソッド
    /// 主に入力処理、衝突判定などの物理計算、オーディオの再生など
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Update(GameTime gameTime)
    {
      // ゲームパッドの状態を取得する
      _gamePadStates[0] = GamePad.GetState(PlayerIndex.One);
      _gamePadStates[1] = GamePad.GetState(PlayerIndex.Two);
      _gamePadStates[2] = GamePad.GetState(PlayerIndex.Three);
      _gamePadStates[3] = GamePad.GetState(PlayerIndex.Four);

      // ゲームパッドの Back ボタン、またはキーボードの Esc キーを押したときにゲームを終了させます
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      // 登録された GameComponent を更新する
      base.Update(gameTime);
    }

    /// <summary>
    /// 描画処理を行うメソッド
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Draw(GameTime gameTime)
    {
      // 画面を指定した色でクリアします
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // スプライトの描画準備
      _spriteBatch.Begin();

      // ゲームパッドの入力情報を表示
      DrawInputGamePadInformation(PlayerIndex.One, new Vector2(20.0f, 20.0f));
      DrawInputGamePadInformation(PlayerIndex.Two, new Vector2(40.0f, 260.0f));
      DrawInputGamePadInformation(PlayerIndex.Three, new Vector2(400.0f, 20.0f));
      DrawInputGamePadInformation(PlayerIndex.Four, new Vector2(420.0f, 260.0f));

      // スプライトの一括描画
      _spriteBatch.End();

      // 登録された DrawableGameComponent を描画する
      base.Draw(gameTime);
    }

    /// <summary>
    /// ゲームパッドの入力情報を表示する
    /// </summary>
    /// <param name="playerIndex">プレイヤーのインデックス</param>
    /// <param name="textBasePosition">表示するときストの位置</param>
    private void DrawInputGamePadInformation(PlayerIndex playerIndex, Vector2 textBasePosition)
    {
      // ゲームパッドのインデックス
      int padIndex = (int)playerIndex;

      string stateMessage;

      _spriteBatch.DrawString(_font,
          "PlayerIndex : " + playerIndex.ToString(),
         new Vector2(0.0f, 0.0f) + textBasePosition, Color.White);

      // 接続状況
      _spriteBatch.DrawString(_font,
          "IsConnected : " + _gamePadStates[padIndex].IsConnected,
          new Vector2(0.0f, 30.0f) + textBasePosition, Color.White);

      // 左スティック
      _spriteBatch.DrawString(_font,
          "LeftStick : " +
              _gamePadStates[padIndex].ThumbSticks.Left.X.ToString("f8") + ", " +
              _gamePadStates[padIndex].ThumbSticks.Left.Y.ToString("f8"),
          new Vector2(0.0f, 60.0f) + textBasePosition, Color.White);

      // 右スティック
      _spriteBatch.DrawString(_font,
          "RightStick : " +
              _gamePadStates[padIndex].ThumbSticks.Right.X.ToString("f8") + ", " +
              _gamePadStates[padIndex].ThumbSticks.Right.Y.ToString("f8"),
          new Vector2(0.0f, 90.0f) + textBasePosition, Color.White);

      // 方向パッド
      stateMessage = "";
      if (_gamePadStates[padIndex].DPad.Up == ButtonState.Pressed)
      {
        stateMessage += "Up    ";
      }
      if (_gamePadStates[padIndex].DPad.Left == ButtonState.Pressed)
      {
        stateMessage += "Left  ";
      }
      if (_gamePadStates[padIndex].DPad.Down == ButtonState.Pressed)
      {
        stateMessage += "Down  ";
      }
      if (_gamePadStates[padIndex].DPad.Right == ButtonState.Pressed)
      {
        stateMessage += "Right ";
      }
      _spriteBatch.DrawString(_font, "DirectionalPad : " + stateMessage,
          new Vector2(0.0f, 120.0f) + textBasePosition, Color.White);

      stateMessage = "";
      // Aボタン
      if (_gamePadStates[padIndex].Buttons.A == ButtonState.Pressed)
      {
        stateMessage += "A ";
      }
      // Bボタン
      if (_gamePadStates[padIndex].Buttons.B == ButtonState.Pressed)
      {
        stateMessage += "B ";
      }
      // Xボタン
      if (_gamePadStates[padIndex].Buttons.X == ButtonState.Pressed)
      {
        stateMessage += "X ";
      }
      // Yボタン
      if (_gamePadStates[padIndex].Buttons.Y == ButtonState.Pressed)
      {
        stateMessage += "Y ";
      }
      // STARTボタン
      if (_gamePadStates[padIndex].Buttons.Start == ButtonState.Pressed)
      {
        stateMessage += "START ";
      }
      // BACKボタン
      if (_gamePadStates[padIndex].Buttons.Back == ButtonState.Pressed)
      {
        stateMessage += "BACK ";
      }
      // Lボタン
      if (_gamePadStates[padIndex].Buttons.LeftShoulder == ButtonState.Pressed)
      {
        stateMessage += "LB ";
      }
      // Rボタン
      if (_gamePadStates[padIndex].Buttons.RightShoulder == ButtonState.Pressed)
      {
        stateMessage += "RB ";
      }
      // LeftStickボタン
      if (_gamePadStates[padIndex].Buttons.LeftStick == ButtonState.Pressed)
      {
        stateMessage += "LeftStick ";
      }
      // RightStickボタン
      if (_gamePadStates[padIndex].Buttons.RightStick == ButtonState.Pressed)
      {
        stateMessage += "RightStick ";
      }
      _spriteBatch.DrawString(_font, "Buttons : " + stateMessage,
          new Vector2(0.0f, 150.0f) + textBasePosition, Color.White);

      // トリガー
      _spriteBatch.DrawString(_font,
          "Trigger : " +
              _gamePadStates[padIndex].Triggers.Left.ToString("f8") + ", " +
              _gamePadStates[padIndex].Triggers.Right.ToString("f8"),
          new Vector2(0.0f, 180.0f) + textBasePosition, Color.White);
    }
  }
}
