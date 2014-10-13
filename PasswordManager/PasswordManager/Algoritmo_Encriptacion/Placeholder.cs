using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace PasswordManager.Algoritmo_Encriptacion
{
    public class Placeholder : Adorner
    {


        private readonly TextChangedEventHandler _textChangedHandler;
        private readonly RoutedEventHandler _passwordChangedHandler;
        private bool _isPlaceholderVisible;

        #region Dependency Property

        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(Placeholder),
            new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnTextChanged)));
        #endregion

        #region Constructors


        protected Placeholder(Control adornedElement) : base(adornedElement)
        {
            this.IsHitTestVisible = false;
            _textChangedHandler = new TextChangedEventHandler(AdornedElement_ContentChanged);
            _passwordChangedHandler = new RoutedEventHandler(AdornedElement_ContentChanged);

            adornedElement.GotFocus += new RoutedEventHandler(AdornedElement_GotFocus);
            adornedElement.LostFocus += new RoutedEventHandler(AdornedElement_LostFocus);
        }

        public Placeholder(PasswordBox adornedElement) : this((Control)adornedElement)
        {
            if (!adornedElement.IsFocused)
                adornedElement.PasswordChanged += _passwordChangedHandler;
        }

        public Placeholder(TextBox adornedElement): this((Control)adornedElement)
        {
            if (!adornedElement.IsFocused)
                adornedElement.TextChanged += _textChangedHandler;
        }


        public Placeholder(RichTextBox adornedElement) : this((Control)adornedElement)
        {
            if (!adornedElement.IsFocused)
                adornedElement.TextChanged += _textChangedHandler;
        }
        #endregion

        #region Property Changed Callbacks

        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Control adornedElement = sender as Control;
           if (adornedElement.IsLoaded)
                AddAdorner(adornedElement);
            else
                adornedElement.Loaded += new RoutedEventHandler(AdornedElement_Loaded);
        }
        #endregion

        #region Event Handlers

        private static void AdornedElement_Loaded(object sender, RoutedEventArgs e)
        {
            Control adornedElement = (Control)sender;
            adornedElement.Loaded -= AdornedElement_Loaded;
            AddAdorner(adornedElement);
        }

        private void AdornedElement_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBase textBoxBase = AdornedElement as TextBoxBase;
            if (textBoxBase != null)
                textBoxBase.TextChanged -= AdornedElement_ContentChanged;
            else
            {
                PasswordBox passwordBox = AdornedElement as PasswordBox;
                if (passwordBox != null)
                    passwordBox.PasswordChanged -= AdornedElement_ContentChanged;
            }

            if (_isPlaceholderVisible)
                this.InvalidateVisual();
        }


        public void AdornedElement_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBase textBoxBase = AdornedElement as TextBoxBase;
            if (textBoxBase != null)
                textBoxBase.TextChanged += _textChangedHandler;
            else
            {
                PasswordBox passwordBox = AdornedElement as PasswordBox;
                if (passwordBox != null)
                    passwordBox.PasswordChanged += _passwordChangedHandler;
            }
            if (!_isPlaceholderVisible && IsElementEmpty())
                this.InvalidateVisual();
        }

        private void AdornedElement_ContentChanged(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholderVisible ^ IsElementEmpty())
                this.InvalidateVisual();
        }
        #endregion

        #region Attached Property Getters and Setters

        public static string GetText(Control adornedElement)
        {
            if (adornedElement == null)
                throw new ArgumentNullException("adornedElement");
            return (string)adornedElement.GetValue(TextProperty);
        }

        public static void SetText(Control adornedElement, string placeholderText)
        {
            if (adornedElement == null)
                throw new ArgumentNullException("adornedElement");

            if (!(adornedElement is TextBox || adornedElement is RichTextBox || adornedElement is PasswordBox))
                throw new InvalidOperationException();
            adornedElement.SetValue(TextProperty, placeholderText);
        }
        #endregion

        private static void AddAdorner(Control adornedElement)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            if (adornerLayer == null)
                return;
            Adorner[] adorners = adornerLayer.GetAdorners(adornedElement);
            if (adorners != null)
                foreach (Adorner adorner in adorners)
                    if (adorner is Placeholder)
                        return;

            TextBox textBox = adornedElement as TextBox;
            if (textBox != null)
            {
                adornerLayer.Add(new Placeholder(textBox));
               return;
            }



            RichTextBox richTextBox = adornedElement as RichTextBox;
            if (richTextBox != null)
            {
                adornerLayer.Add(new Placeholder(richTextBox));
                return;
            }

            PasswordBox passwordBox = adornedElement as PasswordBox;
            if (passwordBox != null)
            {
                adornerLayer.Add(new Placeholder(passwordBox));
                return;
            }
        }


        private bool IsElementEmpty()
        {
            UIElement adornedElement = AdornedElement;
            TextBox textBox = adornedElement as TextBox;
            if (textBox != null)
                return string.IsNullOrEmpty(textBox.Text);
            PasswordBox passwordBox = adornedElement as PasswordBox;
            if (passwordBox != null)
                return string.IsNullOrEmpty(passwordBox.Password);
            RichTextBox richTextBox = adornedElement as RichTextBox;
            if (richTextBox != null)
            {
                BlockCollection blocks = richTextBox.Document.Blocks;
                if (blocks.Count == 0)
                    return true;
                if (blocks.Count == 1)
                {
                    Paragraph paragraph = blocks.FirstBlock as Paragraph;
                    if (paragraph == null)
                        return false;
                   if (paragraph.Inlines.Count == 0)
                        return true;
                    if (paragraph.Inlines.Count == 1)
                    {
                        Run run = paragraph.Inlines.FirstInline as Run;
                        return (run != null && string.IsNullOrEmpty(run.Text));
                    }
                }
                return false;
            }
            return false;
        }

        private TextAlignment ComputedTextAlignment()
        {
            Control adornedElement = AdornedElement as Control;
            TextBox textBox = adornedElement as TextBox;
            if (textBox != null)
            {
                if (DependencyPropertyHelper.GetValueSource(textBox, TextBox.HorizontalContentAlignmentProperty)
                    .BaseValueSource != BaseValueSource.Local ||
                    DependencyPropertyHelper.GetValueSource(textBox, TextBox.TextAlignmentProperty)
                    .BaseValueSource == BaseValueSource.Local)

                    // TextAlignment dominates
                    return textBox.TextAlignment;
            }

            RichTextBox richTextBox = adornedElement as RichTextBox;
            if (richTextBox != null)
            {
                BlockCollection blocks = richTextBox.Document.Blocks;
                TextAlignment textAlignment = richTextBox.Document.TextAlignment;
                if (blocks.Count == 0)
                    return textAlignment;

                if (blocks.Count == 1)
                {
                    Paragraph paragraph = blocks.FirstBlock as Paragraph;
                    if (paragraph == null)
                        return textAlignment;
                    return paragraph.TextAlignment;
                }
                return textAlignment;
            }

            switch (adornedElement.HorizontalContentAlignment)
            {
                case HorizontalAlignment.Left:
                    return TextAlignment.Left;
                case HorizontalAlignment.Right:
                    return TextAlignment.Right;
                case HorizontalAlignment.Center:
                    return TextAlignment.Center;
                case HorizontalAlignment.Stretch:
                    return TextAlignment.Justify;
            }
            return TextAlignment.Left;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Control adornedElement = this.AdornedElement as Control;
            string placeholderText;

            if (adornedElement == null ||
                adornedElement.IsFocused ||
                !IsElementEmpty() ||
                string.IsNullOrEmpty(placeholderText = (string)adornedElement.GetValue(TextProperty)))
                _isPlaceholderVisible = false;
            else
            {
                _isPlaceholderVisible = true;
                Size size = adornedElement.RenderSize;
                TextAlignment computedTextAlignment = ComputedTextAlignment();
                // foreground brush does not need to be dynamic. OnRender called when SystemColors changes.
                Brush foreground = SystemColors.GrayTextBrush.Clone();
                foreground.Opacity = adornedElement.Foreground.Opacity;
                Typeface typeface = new Typeface(adornedElement.FontFamily, FontStyles.Italic, adornedElement.FontWeight, adornedElement.FontStretch);
                FormattedText formattedText = new FormattedText(placeholderText,
                                                  CultureInfo.CurrentCulture,
                                                  adornedElement.FlowDirection,
                                                  typeface,
                                                  adornedElement.FontSize,
                                                  foreground);
                formattedText.TextAlignment = computedTextAlignment;
                formattedText.MaxTextHeight = size.Height - adornedElement.BorderThickness.Top - adornedElement.BorderThickness.Bottom - adornedElement.Padding.Top - adornedElement.Padding.Bottom;
                formattedText.MaxTextWidth = size.Width - adornedElement.BorderThickness.Left - adornedElement.BorderThickness.Right - adornedElement.Padding.Left - adornedElement.Padding.Right - 4.0;

                double left;
                double top = 0.0;
                if (adornedElement.FlowDirection == FlowDirection.RightToLeft)
                    left = adornedElement.BorderThickness.Right + adornedElement.Padding.Right + 2.0;
                else
                    left = adornedElement.BorderThickness.Left + adornedElement.Padding.Left + 2.0;
                switch (adornedElement.VerticalContentAlignment)
                {
                    case VerticalAlignment.Top:
                    case VerticalAlignment.Stretch:
                        top = adornedElement.BorderThickness.Top + adornedElement.Padding.Top;
                        break;
                    case VerticalAlignment.Bottom:

                        top = size.Height - adornedElement.BorderThickness.Bottom - adornedElement.Padding.Bottom - formattedText.Height;
                        break;
                    case VerticalAlignment.Center:
                        top = (size.Height + adornedElement.BorderThickness.Top - adornedElement.BorderThickness.Bottom + adornedElement.Padding.Top - adornedElement.Padding.Bottom - formattedText.Height) / 2.0;
                        break;
                }

                if (adornedElement.FlowDirection == FlowDirection.RightToLeft)
                {
                    // Somehow everything got drawn reflected. Add a transform to correct.
                    drawingContext.PushTransform(new ScaleTransform(-1.0, 1.0, RenderSize.Width / 2.0, 0.0));
                    drawingContext.DrawText(formattedText, new Point(left, top));
                    drawingContext.Pop();
                }
                else
                    drawingContext.DrawText(formattedText, new Point(left, top));
            }
        }
    }
}
