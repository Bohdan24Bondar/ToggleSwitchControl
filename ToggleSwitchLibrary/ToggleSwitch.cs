using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToggleSwitchLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ToggleSwitchLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ToggleSwitchLibrary;assembly=ToggleSwitchLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class ToggleSwitch : Control
    {
        #region Constants

        public const string FULL_TRACK_NAME = "borderTemplate";

        public const string CHECKED_TRACK_NAME = "PART_CheckedTrack";

        public const string THUMB_NAME = "thumbTemplate";

        public const string TEXT_BLOCK_NAME = "contentBlock";

        public const string CONTAINER_NAME = "PART_Container";

        #endregion

        #region Private date

        private Border _fullTrack;

        private Border _checkedTrack;

        private Thumb _switchThumb;

        private Rectangle _knob;

        private TextBlock _contentBlock;

        private Grid _container;

        #endregion

        public double Offset { get; set; }

        public double MiddlePosition { get => (_fullTrack.ActualWidth - _switchThumb.ActualWidth) / 2.0; }

        public double TextBlockWidth { get => ControlWidth - ToggleWidth; }

        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        #region CheckedProperties

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(ToggleSwitch), new FrameworkPropertyMetadata(OnIsCheckedChanged)); 

        public Brush ChechedBackground
        {
            get { return (Brush)GetValue(ChechedBackgroundProperty); }
            set { SetValue(ChechedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ChechedBackgroundProperty =
            DependencyProperty.Register(nameof(ChechedBackground), typeof(Brush), typeof(ToggleSwitch));

        public string ContentChecked
        {
            get { return (string)GetValue(ContentCheckedProperty); }
            set { SetValue(ContentCheckedProperty, value); }
        }

        public static readonly DependencyProperty ContentCheckedProperty =
            DependencyProperty.Register(nameof(ContentChecked), typeof(string), typeof(ToggleSwitch));

        public Brush ThumbBackgroundChecked
        {
            get { return (Brush)GetValue(ThumbBackgroundCheckedProperty); }
            set { SetValue(ThumbBackgroundCheckedProperty, value); }
        }

        public static readonly DependencyProperty ThumbBackgroundCheckedProperty =
            DependencyProperty.Register(nameof(ThumbBackgroundChecked), typeof(Brush), typeof(ToggleSwitch));

        #endregion

        #region UncheckedProperties

        public Brush UncheckedBackground
        {
            get { return (Brush)GetValue(UncheckedBackgroundProperty); }
            set { SetValue(UncheckedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty UncheckedBackgroundProperty =
            DependencyProperty.Register(nameof(UncheckedBackground), typeof(Brush), typeof(ToggleSwitch));

        public string ContentUnchecked
        {
            get { return (string)GetValue(ContentUncheckedProperty); }
            set { SetValue(ContentUncheckedProperty, value); }
        }

        public static readonly DependencyProperty ContentUncheckedProperty =
            DependencyProperty.Register(nameof(ContentUnchecked), typeof(string), typeof(ToggleSwitch));

        public Brush ThumbBackgroundUnchecked
        {
            get { return (Brush)GetValue(ThumbBackgroundUncheckedProperty); }
            set { SetValue(ThumbBackgroundUncheckedProperty, value); }
        }

        public static readonly DependencyProperty ThumbBackgroundUncheckedProperty =
            DependencyProperty.Register(nameof(ThumbBackgroundUnchecked), typeof(Brush), typeof(ToggleSwitch));

        #endregion

        #region Events

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ToggleSwitch)d;

            if (e.NewValue != e.OldValue)
            {
                if ((bool)e.NewValue)
                {
                    control.InvokeChecked(new RoutedEventArgs());
                }
                else
                {
                    control.InvokeUnchecked(new RoutedEventArgs());
                }
            }
        }

        public event RoutedEventHandler Unchecked;

        protected void InvokeUnchecked(RoutedEventArgs e)
        {
            Unchecked?.Invoke(this, e);
        }

        public event RoutedEventHandler Checked;

        protected void InvokeChecked(RoutedEventArgs e)
        {
            Checked?.Invoke(this, e);
        }

        #endregion

        #region Size properties

        public double ToggleWidth
        {
            get { return (double)GetValue(ToggleWidthProperty); }
            set { SetValue(ToggleWidthProperty, value); }
        }

        public static readonly DependencyProperty ToggleWidthProperty =
            DependencyProperty.Register(nameof(ToggleWidth), typeof(double), typeof(ToggleSwitch));

        public double ContentBlockWidth
        {
            get { return (double)GetValue(ContentBlockWidthProperty); }
            set { SetValue(ContentBlockWidthProperty, value); }
        }

        public static readonly DependencyProperty ContentBlockWidthProperty =
            DependencyProperty.Register(nameof(ContentBlockWidth), typeof(double), typeof(ToggleSwitch));

        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), typeof(ToggleSwitch));

        public double ControlWidth
        {
            get { return (double)GetValue(ControlWidthProperty); }
            set { SetValue(ControlWidthProperty, value); }
        }

        public static readonly DependencyProperty ControlWidthProperty =
            DependencyProperty.Register(nameof(ControlWidth), typeof(double), typeof(ToggleSwitch));

        #endregion

        #region DisabledProperties

        public Brush IsDisabledBackround
        {
            get { return (Brush)GetValue(IsDisabledBackroundProperty); }
            set { SetValue(IsDisabledBackroundProperty, value); }
        }

        public static readonly DependencyProperty IsDisabledBackroundProperty =
            DependencyProperty.Register(nameof(IsDisabledBackround), typeof(Brush), typeof(ToggleSwitch));

        public Brush IsDisabledThumbBackground { get; set; } 

        #endregion

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        public static readonly DependencyProperty IsDraggingProperty =
            DependencyProperty.Register(nameof(IsDragging), typeof(bool), typeof(ToggleSwitch));

        #region Dragging methods

        public void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            IsDragging = false;
            _fullTrack = Template.FindName(FULL_TRACK_NAME, this) as Border;
            _switchThumb = Template.FindName(THUMB_NAME, this) as Thumb;

            if (e.HorizontalChange > 0)
            {
                bool isOnLeft = IsOnLeft();

                MoveThumb(isOnLeft);
            }

            if (e.HorizontalChange < 0)
            {
                bool isOnLeft = IsOnLeft();

                MoveThumb(isOnLeft);
            }


            if (e.HorizontalChange == 0)
            {
                if (Offset == 0)
                {
                    Canvas.SetLeft(_switchThumb, _fullTrack.ActualWidth - _switchThumb.ActualWidth);
                    Offset = _fullTrack.ActualWidth; 
                    IsChecked = true;
                }
                else
                {
                    Canvas.SetLeft(_switchThumb, 0);
                    Offset = 0;
                    IsChecked = false;
                }
            }

            _checkedTrack.Width = 0;
            ChangeThumbBackgound();
        }

        public void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Offset + e.HorizontalChange < 0)
            {
                Offset = 0;
                Canvas.SetLeft(_switchThumb, Offset);
                _checkedTrack.Width = 0;
                return;
            }

            if (Offset + e.HorizontalChange + _switchThumb.ActualWidth > _fullTrack.ActualWidth + 1)//todo sum
            {
                Offset = _fullTrack.ActualWidth - _switchThumb.ActualWidth;
                Canvas.SetLeft(_switchThumb, Offset);
                _checkedTrack.Width = _fullTrack.ActualWidth;
                return;
            }

            Offset += e.HorizontalChange;
            Canvas.SetLeft(_switchThumb, Offset);
            _checkedTrack = Template.FindName(CHECKED_TRACK_NAME, this) as Border;
            _knob = VisualTreeHelper.GetChild(_switchThumb, 0) as Rectangle;

            if (e.HorizontalChange < 0)
            {
                _checkedTrack.Width = Offset + _knob.ActualWidth + e.HorizontalChange;
                IsDragging = true;
            }
            else
            {
                _checkedTrack.Width = Offset + _knob.ActualWidth;
            }
        }

        #endregion

        protected virtual void AddEventHandlers()
        {
            if (_switchThumb != null)
            {
                _switchThumb.DragDelta += OnDragDelta;
                _switchThumb.DragCompleted += OnDragCompleted;
            }
        }

        private void MoveThumb(bool isOnLeft)
        {
            if (isOnLeft)
            {
                Canvas.SetLeft(_switchThumb, 0);
                Offset = 0;
                IsChecked = false;
            }
            else
            {
                Canvas.SetLeft(_switchThumb, _fullTrack.ActualWidth - _switchThumb.ActualWidth);
                Offset = _fullTrack.ActualWidth;
                IsChecked = true;
            }
        }

        private bool IsOnLeft()
        {
            return Offset < MiddlePosition;
        }

        public void ChangeThumbBackgound()
        {
            if (IsChecked)
            {
                _knob = VisualTreeHelper.GetChild(_switchThumb, 0) as Rectangle;
                _knob.Fill = ThumbBackgroundChecked;
            }
            else
            {
                _knob = VisualTreeHelper.GetChild(_switchThumb, 0) as Rectangle;
                _knob.Fill = ThumbBackgroundUnchecked;
            }
        }

        public override void OnApplyTemplate()
        {
            _fullTrack = Template.FindName(FULL_TRACK_NAME, this) as Border;
            _checkedTrack = Template.FindName(CHECKED_TRACK_NAME, this) as Border;
            _switchThumb = Template.FindName(THUMB_NAME, this) as Thumb;
            _container = Template.FindName(CONTAINER_NAME, this) as Grid;
            _contentBlock = Template.FindName(TEXT_BLOCK_NAME, this) as TextBlock;
            //_contentBlock.Width = TextBlockWidth;
            AddEventHandlers();
            base.OnApplyTemplate();
        }
    }
}
