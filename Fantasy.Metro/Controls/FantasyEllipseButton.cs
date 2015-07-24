using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fantasy.Metro.Controls
{
    public class FantasyEllipseButton : Button
    {
        public FantasyEllipseButton()
        {
            this.DefaultStyleKey = typeof(FantasyEllipseButton);
        }

        public static readonly DependencyProperty EllipseDiameterProperty =
            DependencyProperty.Register("EllipseDiameter", 
                typeof(Double),
                typeof(FantasyEllipseButton), 
                new PropertyMetadata(18D));

        public static readonly DependencyProperty EllipseStrokeThicknessProperty =
            DependencyProperty.Register("EllipseStrokeThickness",
                typeof(Double),
                typeof(FantasyEllipseButton), 
                new PropertyMetadata(1D));

        public static readonly DependencyProperty IconDataProperty = 
            DependencyProperty.Register("IconData",
                typeof(Geometry), 
                typeof(FantasyEllipseButton));

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", 
                typeof(double),
                typeof(FantasyEllipseButton), 
                new PropertyMetadata(10D));

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth",    
                typeof(double),
                typeof(FantasyEllipseButton),
                new PropertyMetadata(10D));
        
        public Double EllipseDiameter
        {
            get { return (Double)GetValue(EllipseDiameterProperty); }
            set { SetValue(EllipseDiameterProperty, value); }
        }

        public Double EllipseStrokeThickness
        {
            get { return (Double)GetValue(EllipseStrokeThicknessProperty); }
            set { SetValue(EllipseStrokeThicknessProperty, value); }
        }

        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        public Double IconHeight
        {
            get { return (Double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public Double IconWidth
        {
            get { return (Double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
    }
}
