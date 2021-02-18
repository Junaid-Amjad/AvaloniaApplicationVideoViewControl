using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace AvaloniaApplicationWithControls.Controls
{
    class CustomSkiaControl:Control
    {
        private object lockObject = new object();
            public CustomSkiaControl()
            {
                ClipToBounds = true;
            }

            class CustomDrawOp : ICustomDrawOperation
            {
                private readonly FormattedText _noSkia;

                public CustomDrawOp(Rect bounds, FormattedText noSkia)
                {
                    _noSkia = noSkia;
                    Bounds = bounds;
                }

                public void Dispose()
                {
                    // No-op
                }

                public Rect Bounds { get; }
            [DllImport("user32.dll", SetLastError = true)]
            private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

            [DllImport("user32.dll")]
            private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

            public bool HitTest(Point p) => false;
                public bool Equals(ICustomDrawOperation other) => false;
                static Stopwatch St = Stopwatch.StartNew();
                public void Render(IDrawingContextImpl context)
                {
                
                    var canvas = (context as ISkiaDrawingContextImpl)?.SkCanvas;
                    if (canvas == null)
                        context.DrawText(Brushes.Black, new Point(), _noSkia.PlatformImpl);
                    else
                    {
                        canvas.Save();
                        // create the first shader
                        var colors = new SKColor[] {
                        new SKColor(0, 255, 255),
                        new SKColor(255, 0, 255),
                        new SKColor(255, 255, 0),
                        new SKColor(0, 255, 255)
                    };
                    

                    /*
                    var ffplay = new Process
                    {
                        StartInfo =
                            {
                                FileName = "ffplay",
                                Arguments = "http://81.149.56.38:8084/mjpg/video.mjpg",
                                // hides the command window
                                CreateNoWindow = true, 
                                // redirect input, output, and error streams..
                                RedirectStandardError = true,
                                RedirectStandardOutput = true,
                                UseShellExecute = false
                            }
                    };
                    ffplay.EnableRaisingEvents = true;
                    ffplay.OutputDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
                    ffplay.ErrorDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
                    ffplay.Exited += (o, e) => Debug.WriteLine("Exited", "ffplay");
                    ffplay.Start();
                    Thread.Sleep(200);
                   
                    

                    // child, new parent
                    // make 'this' the parent of ffmpeg (presuming you are in scope of a Form or Control)
                    SetParent(ffplay.MainWindowHandle, this);

                    // window, x, y, width, height, repaint
                    // move the ffplayer window to the top-left corner and set the size to 320x280
                    MoveWindow(ffplay.MainWindowHandle, 0, 0, 320, 280, true);
                    */

                        var sx = Animate(100, 2, 10);
                        var sy = Animate(1000, 5, 15);
                        var lightPosition = new SKPoint(
                            (float)(Bounds.Width / 2 + Math.Cos(St.Elapsed.TotalSeconds) * Bounds.Width / 4),
                            (float)(Bounds.Height / 2 + Math.Sin(St.Elapsed.TotalSeconds) * Bounds.Height / 4));
                        using (var sweep =
                            SKShader.CreateSweepGradient(new SKPoint((int)Bounds.Width / 2, (int)Bounds.Height / 2), colors,
                                null))
                        using (var turbulence = SKShader.CreatePerlinNoiseFractalNoise(0.05f, 0.05f, 4, 0))
                        using (var shader = SKShader.CreateCompose(sweep, turbulence, SKBlendMode.SrcATop))
                        using (var blur = SKImageFilter.CreateBlur(Animate(100, 2, 10), Animate(100, 5, 15)))
                        using (var paint = new SKPaint
                        {
                            Shader = shader,
                            ImageFilter = blur
                        })
                            canvas.DrawPaint(paint);
                    
                        using (var pseudoLight = SKShader.CreateRadialGradient(
                            lightPosition,
                            (float)(Bounds.Width / 3),
                            new[] {
                            new SKColor(255, 200, 200, 100),
                            SKColors.Transparent,
                            new SKColor(40,40,40, 220),
                            new SKColor(20,20,20, (byte)Animate(100, 200,220)) },
                            new float[] { 0.3f, 0.3f, 0.8f, 1 },
                            SKShaderTileMode.Clamp))
                        using (var paint = new SKPaint
                        {
                            Shader = pseudoLight
                        })
                            canvas.DrawPaint(paint);
                        canvas.Restore();
                }
            }
                static int Animate(int d, int from, int to)
                {
                    var ms = (int)(St.ElapsedMilliseconds / d);
                    var diff = to - from;
                    var range = diff * 2;
                    var v = ms % range;
                    if (v > diff)
                        v = range - v;
                    var rv = v + from;
                    if (rv < from || rv > to)
                        throw new Exception("WTF");
                    return rv;
                }

        }



        public override void Render(DrawingContext context)
        {
            var noSkia = new FormattedText()
            {
                Text = "Current rendering API is not Skia"
            };
            lock (lockObject)
            {
                context.Custom(new CustomDrawOp(new Rect(0, 0, Bounds.Width, Bounds.Height), noSkia));
                Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
            }
        }
        
    }
}
