using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DesktopClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GradientPage : Page
    {
        private readonly CompositionLinearGradientBrush gradientBrush1, gradientBrush2;
        private readonly SpriteVisual backgroundVisual;
        private static readonly Color GradientStop1StartColor = Color.FromArgb(255, 43, 210, 255);
        private static readonly Color GradientStop2StartColor = Color.FromArgb(255, 43, 255, 136);
        private static readonly Color GradientStop3StartColor = Colors.Red;
        private static readonly Color GradientStop4StartColor = Colors.Black;

        public GradientPage()
        {
            this.InitializeComponent();

            var compositor = Window.Current.Compositor;

            gradientBrush1 = compositor.CreateLinearGradientBrush();
            gradientBrush1.StartPoint = new Vector2(1.0f);
            gradientBrush1.EndPoint = Vector2.Zero;

            var gradientStop1 = compositor.CreateColorGradientStop(0.5f, GradientStop1StartColor);
            var gradientStop2 = compositor.CreateColorGradientStop(0.5f, GradientStop2StartColor);
            gradientBrush1.ColorStops.Add(gradientStop1);
            gradientBrush1.ColorStops.Add(gradientStop2);

            gradientBrush2 = compositor.CreateLinearGradientBrush();
            gradientBrush2.StartPoint = new Vector2(1.0f, 0);
            gradientBrush2.EndPoint = new Vector2(0, 1.0f);

            var gradientStop3 = compositor.CreateColorGradientStop(0.25f, GradientStop3StartColor);
            var gradientStop4 = compositor.CreateColorGradientStop(1.0f, GradientStop4StartColor);
            gradientBrush2.ColorStops.Add(gradientStop3);
            gradientBrush2.ColorStops.Add(gradientStop4);

            var graphicsEffect = new BlendEffect()
            {
                Mode = BlendEffectMode.Screen,
                Foreground = new CompositionEffectSourceParameter("Main"),
                Background = new CompositionEffectSourceParameter("Tint"),
            };

            var effectFactory = compositor.CreateEffectFactory(graphicsEffect);
            var brush = effectFactory.CreateBrush();
            brush.SetSourceParameter("Main", gradientBrush1);
            brush.SetSourceParameter("Tint", gradientBrush2);

            backgroundVisual = compositor.CreateSpriteVisual();
            backgroundVisual.Brush = brush;
            ElementCompositionPreview.SetElementChildVisual(Gradient, backgroundVisual);




            var offsetAnimation1 = compositor.CreateScalarKeyFrameAnimation();
            offsetAnimation1.Duration = TimeSpan.FromSeconds(1);
            offsetAnimation1.DelayTime = TimeSpan.FromSeconds(2);
            offsetAnimation1.InsertKeyFrame(1.0f, 0.25f);
            gradientStop1.StartAnimation(nameof(gradientStop1.Offset), offsetAnimation1);

            var offsetAnimation2 = compositor.CreateScalarKeyFrameAnimation();
            offsetAnimation2.Duration = TimeSpan.FromSeconds(1);
            offsetAnimation2.DelayTime = TimeSpan.FromSeconds(2);
            offsetAnimation2.InsertKeyFrame(1.0f, 1.0f);
            gradientStop2.StartAnimation(nameof(gradientStop1.Offset), offsetAnimation2);

            var gradientStop3Animation = compositor.CreateColorKeyFrameAnimation();
            gradientStop3Animation.Duration = TimeSpan.FromSeconds(2);
            gradientStop3Animation.DelayTime = TimeSpan.FromSeconds(2);
            gradientStop3Animation.Direction = AnimationDirection.Alternate;
            gradientStop3Animation.InsertKeyFrame(1.0f, GradientStop3StartColor);
            gradientStop3.StartAnimation(nameof(gradientStop1.Color), gradientStop3Animation);

            Loaded += async (s, e) =>
            {
                await Task.Yield();

                LeftText.Visibility = Visibility.Collapsed;
                RightText.Visibility = Visibility.Collapsed;
                MiddleText.Visibility = Visibility.Visible;

                await Task.Delay(3600);
                await ((App)Application.Current).ActivationService.TurnOfSplash();
            };

            Gradient.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;

                backgroundVisual.Size = e.NewSize.ToVector2();
                gradientBrush1.CenterPoint = backgroundVisual.Size / 2;
                gradientBrush2.CenterPoint = backgroundVisual.Size / 2;
            };
        }
    }
}