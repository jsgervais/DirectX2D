# DirectX2D
Simple 2D app using C# and DirectX 

LineDrawer contains a simple C# app to draw lines. 

Universal Windows10App is a test to do the same under Windows 10 Universal app project. 

## Getting started 

Under visual studio, make sure your recover project's nuget packages : 
 - SharpDX 4.0.1
 - SharpDX.D3DCompiler 4.0.1
 - SharpDX.Desktop 4.0.1
 - SharpDX.Direct2D1 4.0.1
 - SharpDX.Direct3D1 4.0.1
 - SharpDX.DXGI 4.0.1
 - SharpDX.Mathematics 4.0.1
 
[SharpDX](http://sharpdx.org/) is an open source managed .Net wrapper of the main DirectX API (V9, v11 and v12).
 
## Creating your first app
Inspired by SharpDX demo samples, I refactored and extended a few classes to make it easier : 

Your should have your main class derive from a 2D or 3D class like below, and only have to Run() the main render loop  : 

```
class Program : SharpDx2D11Base
{
	private static void Main()
	{
		Program program = new Program();
		program.Run(new DisplayWindowConfiguration("LineDrawer", APP_WIDTH, APP_HEIGHT););
	}
}
```

Next, if you have specialize DirecX objects you want to use, for example, TextFormat and TextLayout, you can have them initialized before and outside the main loop : 
```
	protected override void Initialize(DisplayWindowConfiguration conf)
	{
		base.Initialize(conf);

		TextFormat = new TextFormat(FactoryDWrite, "Calibri", 20) { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Center };

		RenderTarget2D.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype;
		TextLayout = new TextLayout(FactoryDWrite, _mousePosition.GetMouseCoordinates(), TextFormat, conf.Width, conf.Height);

	}
```


Any rendering can then be done in the onRender() function. Hint: remember to clear the backbuffer before rewriting the next frame!

```
	protected override void OnRender()
	{
		//clear previous frame
		RenderTarget2D.Clear(null);
 
		//Displays mouse coords top left corner : (0,0)
		TextLayout.Dispose();
		TextLayout = new TextLayout(FactoryDWrite, _mousePosition.GetMouseCoordinates(), TextFormat, 400, 40);

		RenderTarget2D.DrawTextLayout(new Vector2(0, 0), TextLayout, SceneColorBrush, DrawTextOptions.None);

		//Render current line if not null "?." operator  
		((IRenderableItem) _currentLine)?.Render(RenderTarget2D);

		//and all added lines 
		foreach (IRenderableItem line in _lines )
		{
			line.Render(RenderTarget2D);
		}
	}
```


## Events in the render area
Events are generated from System.Windows.Forms.Form 

```
	protected override void MouseClick(MouseEventArgs e)
	{

	}

	protected override void MouseMove(MouseEventArgs e)
	{
 
	}

	protected override void MouseDown(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
		}
	}

	protected override void MouseUp(MouseEventArgs e)
	{
 
	}

	protected override void KeyDown(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
			Exit();
	}

	protected override void KeyUp(KeyEventArgs e)
	{

	}
```

